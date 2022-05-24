using Alipay.EasySDK.Factory;
using Alipay.EasySDK.Kernel;
using Jiepei.ERP.EntityFrameworkCore;
using Jiepei.ERP.EventBus.Shared;
using Jiepei.ERP.Members;
using Jiepei.ERP.News;
using Jiepei.ERP.Orders;
using Jiepei.ERP.Orders.Application.External.Configuration;
using Jiepei.ERP.Suppliers;
using Jiepei.ERP.Suppliers.Unionfab;
using Jiepei.ERP.Utilities;
using Jiepei.ERP.Utilities.Pays;
using Jiepei.ERP.Utilities.Ship;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.VirtualFileSystem;

namespace Jiepei.ERP
{
    [DependsOn(
        typeof(ERPHttpApiModule),
        typeof(AbpAutofacModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(ERPApplicationModule),
        typeof(ERPEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule)
    )]
    [DependsOn(typeof(OrdersApplicationModule),
        typeof(AbpBlobStoringFileSystemModule))]
    public class ERPHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            Configure<AliyunOSSOptions>(configuration.GetSection(
                                       AliyunOSSOptions.AliyunOSS));

            Configure<SupplierUnionfabOptions>(configuration.GetSection(SupplierUnionfabOptions.Unionfab));

            ConfigureAHAliPay(configuration);
            context.Services.AddWeChatPay();
            context.Services.AddOptions().Configure<WeChatPayAHOption>(configuration.GetSection("AHWeChatPay"));
            context.Services.AddOptions().Configure<AliPayAHOption>(configuration.GetSection("AHAliPay"));
            context.Services.AddOptions().Configure<JwtConfig>(configuration.GetSection("Jwt"));
            context.Services.AddOptions().Configure<NeiMao>(configuration.GetSection("NeiMao"));


            //context.Services.AddAlwaysAllowAuthorization();

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
        }

        private void ConfigureCache(IConfiguration configuration)
        {
            Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "ERP:"; });
        }
        private void ConfigureRemoteApi(IConfiguration configuration, IServiceCollection service)
        {
            var sheetMetalApiConfiguration = configuration.GetSection(nameof(SheetMetalApiConfiguration))
                .Get<SheetMetalApiConfiguration>();
            service.AddSingleton(sheetMetalApiConfiguration);
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
                    options.FileSets.ReplaceEmbeddedByPhysical<ERPApplicationContractsModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}Jiepei.ERP.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<ERPApplicationModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}Jiepei.ERP.Application"));
                });
            }
        }

        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                //options.ConventionalControllers.Create(typeof(ERPApplicationModule).Assembly);
                //options.ConventionalControllers.Create(typeof(OrdersApplicationModule).Assembly);
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            //context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.Authority = configuration["AuthServer:Authority"];
            //        options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
            //        options.Audience = "ERP";
            //    });

            var jwtconfig = configuration.GetSection("Jwt").Get<JwtConfig>();
            context.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtconfig.Issuer,
                    ValidAudience = jwtconfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtconfig.SigningKey)),
                    ClockSkew = TimeSpan.FromSeconds(3), // 缓冲过期时间，总的有效时间等于这个时间加上jwt的过期时间，如果不配置，默认是5分钟
                    ValidateIssuer = true,//是否验证发行者
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否调用对签名securityToken的SecurityKey进行验证
                };
            });
        }

        private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAbpSwaggerGen(
                //configuration["AuthServer:Authority"],
                //new Dictionary<string, string>
                //{
                //    {"ERP", "ERP API"}
                //},
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ERP API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);

                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                    });


                    //options.UseAllOfForInheritance();
                    options.UseInlineDefinitionsForEnums();
                    options.UseAllOfToExtendReferenceSchemas();

                    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Jiepei.ERP.Orders.Application.xml"));
                    if (File.Exists(Path.Combine(AppContext.BaseDirectory, "Jiepei.ERP.EventBus.Shared.xml")))
                        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Jiepei.ERP.EventBus.Shared.xml"));
                    if (File.Exists(Path.Combine(AppContext.BaseDirectory, "Jiepei.ERP.Shared.xml")))
                        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Jiepei.ERP.Shared.xml"));

                });
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            });
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

            //if (MultiTenancyConsts.IsEnabled)
            //{
            //    app.UseMultiTenancy();
            //}

            app.UseAuthorization();

            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ERP API");

                //var configuration = context.GetConfiguration();
                //options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
                //options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
                //options.OAuthScopes("ERP");



            });

            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseUnitOfWork();
            app.UseConfiguredEndpoints();
        }

        private void ConfigureAHAliPay(IConfiguration configuration)
        {
            Factory.SetOptions(new Config
            {
                AppId = configuration["AHAliPay:AppId"],
                AlipayPublicKey = configuration["AHAliPay:AlipayPublicKey"],
                SignType = configuration["AHAliPay:SignType"],
                MerchantPrivateKey = configuration["AHAliPay:PrivateKey"],
                Protocol = "https",
                GatewayHost = configuration["AHAliPay:Gatewayurl"],
                //可设置异步通知接收服务地址（可选）
                NotifyUrl = configuration["AHAliPay:NotifyUrl"],
                //可设置AES密钥，调用AES加解密相关接口时需要（可选）
                //EncryptKey = "<-- 请填写您的AES密钥，例如：aa4BtZ4tspm2wnXLb1ThQA== -->"
            });
        }


        private static void ConfigureXmlComments(SwaggerGenOptions options)
        {
            ConfigureXmlComments<OrdersHttpApiModule>(options);
            ConfigureXmlComments<OrdersApplicationContractsModule>(options);

            ConfigureXmlComments<SuppliersApplicationContractsModule>(options);
            ConfigureXmlComments<SuppliersHttpApiModule>(options);

            ConfigureXmlComments<MembersHttpApiModule>(options);
            ConfigureXmlComments<MembersApplicationContractsModule>(options);

            ConfigureXmlComments<MembersApplicationContractsModule>(options);
            ConfigureXmlComments<MembersApplicationContractsModule>(options);

            ConfigureXmlComments<NewsHttpApiModule>(options);
            ConfigureXmlComments<NewsApplicationContractsModule>(options);
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
