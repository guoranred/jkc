using Jiepei.ERP.EntityFrameworkCore;
using Jiepei.ERP.Members.Admin;
using Jiepei.ERP.News;
using Jiepei.ERP.News.Admin;
using Jiepei.ERP.Orders.Admin;
using Jiepei.ERP.Orders.Application.External.Configuration;
using Jiepei.ERP.Suppliers;
using Jiepei.ERP.Suppliers.Unionfab;
using Jiepei.ERP.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.VirtualFileSystem;

namespace Jiepei.ERP.Admin
{
    [DependsOn(
        typeof(ERPAdminHttpApiModule),
        typeof(AbpAutofacModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(ERPAdminApplicationModule),
        typeof(ERPEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpSwashbuckleModule))]
    [DependsOn(
        typeof(OrdersAdminApplicationModule),
        typeof(NewsAdminApplicationModule),
        typeof(MembersAdminApplicationModule))]
    public class ERPAdminHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            ConfigureConventionalControllers();
            ConfigureAuthentication(context, configuration);
            ConfigureLocalization();

            ConfigureCache(configuration);
            ConfigureVirtualFileSystem(context);
            ConfigureRedis(context, configuration, hostingEnvironment);
            ConfigureCors(context, configuration);
            ConfigureSwaggerServices(context, configuration);
            ConfigureRemoteApi(configuration, context.Services);

            Configure<AbpBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = false; //Disables job execution
            });

            // 优联
            Configure<SupplierUnionfabOptions>(configuration.GetSection(SupplierUnionfabOptions.Unionfab));
            Configure<AliyunOSSOptions>(configuration.GetSection(AliyunOSSOptions.AliyunOSS));

        }

        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(ERPAdminApplicationModule).Assembly);
            });
        }
        private void ConfigureRemoteApi(IConfiguration configuration, IServiceCollection service)
        {
            var sheetMetalApiConfiguration = configuration.GetSection(nameof(SheetMetalApiConfiguration))
                .Get<SheetMetalApiConfiguration>();
            service.AddSingleton(sheetMetalApiConfiguration);
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                    options.Audience = "ERP";
                });
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });
        }

        private void ConfigureCache(IConfiguration configuration)
        {
            Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "ERP:"; });
        }

        private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<ERPDomainSharedModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}Jiepei.ERP.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<ERPDomainModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}Jiepei.ERP.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<ERPAdminApplicationContractsModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}Jiepei.ERP.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<ERPAdminApplicationModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}Jiepei.ERP.Application"));
                });
            }
        }

        private void ConfigureRedis(
            ServiceConfigurationContext context,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment)
        {
            if (!hostingEnvironment.IsDevelopment())
            {
                var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                context.Services
                    .AddDataProtection()
                    .PersistKeysToStackExchangeRedis(redis, "ERP-Protection-Keys");
            }
        }

        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAbpSwaggerGenWithOAuth(
                configuration["AuthServer:Authority"],
                new Dictionary<string, string>
                {
                    {"ERP", "ERP API"}
                },
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ERP API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);

                    ConfigureXmlComments(options);
                });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();
            ServiceProviderInstance.Instance = app.ApplicationServices;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbpRequestLocalization();

            if (!env.IsDevelopment())
            {
                app.UseErrorPage();
            }

            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ERP API");

                var configuration = context.GetConfiguration();
                options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
                options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
                options.OAuthScopes("ERP");
            });

            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseUnitOfWork();
            app.UseConfiguredEndpoints();
        }

        private static void ConfigureXmlComments(SwaggerGenOptions options)
        {
            ConfigureXmlComments<ERPAdminHttpApiModule>(options);
            ConfigureXmlComments<ERPAdminApplicationContractsModule>(options);

            ConfigureXmlComments<MembersAdminHttpApiModule>(options);
            ConfigureXmlComments<MembersAdminApplicationContractsModule>(options);

            ConfigureXmlComments<SuppliersHttpApiModule>(options);
            ConfigureXmlComments<SuppliersApplicationContractsModule>(options);

            ConfigureXmlComments<NewsAdminHttpApiModule>(options);
            ConfigureXmlComments<NewsAdminApplicationContractsModule>(options);
        }

        private static void ConfigureXmlComments<T>(SwaggerGenOptions options)
        {
            var documentPath = GetDocumentFilePath<T>();
            if (File.Exists(documentPath))
            {
                options.IncludeXmlComments(documentPath);
            }
        }

        private static string GetDocumentFilePath<T>()
        {
            var documentName = typeof(T).Assembly.FullName.Split(',').FirstOrDefault();
            if (documentName == null)
            {
                return string.Empty;
            }
            return Path.Combine(AppContext.BaseDirectory, documentName + ".xml");
        }
    }
}

