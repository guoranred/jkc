﻿using System;
using System.Net.Http;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Jiepei.ERP.Members.Blazor.WebAssembly;
using Volo.Abp.Account.Blazor;
using Volo.Abp.AspNetCore.Components.Web.BasicTheme.Themes.Basic;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AspNetCore.Components.WebAssembly.BasicTheme;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity.Blazor.WebAssembly;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Jiepei.ERP.Members.Blazor.Host
{
    [DependsOn(
        typeof(AbpAutofacWebAssemblyModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyBasicThemeModule),
        typeof(AbpIdentityBlazorWebAssemblyModule),
        typeof(AbpAccountBlazorModule),
        typeof(MembersBlazorWebAssemblyModule)
    )]
    public class MembersBlazorHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var environment = context.Services.GetSingletonInstance<IWebAssemblyHostEnvironment>();
            var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();

            ConfigureAuthentication(builder);
            ConfigureHttpClient(context, environment);
            ConfigureBlazorise(context);
            ConfigureRouter(context);
            ConfigureUI(builder);
            ConfigureMenu(context);
            ConfigureAutoMapper(context);
        }

        private void ConfigureRouter(ServiceConfigurationContext context)
        {
            Configure<AbpRouterOptions>(options =>
            {
                options.AppAssembly = typeof(MembersBlazorHostModule).Assembly;
            });
        }

        private void ConfigureMenu(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new MembersHostMenuContributor());
            });
        }

        private void ConfigureBlazorise(ServiceConfigurationContext context)
        {
            context.Services
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();
        }

        private static void ConfigureAuthentication(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("AuthServer", options.ProviderOptions);
                options.ProviderOptions.DefaultScopes.Add("Members");
            });
        }

        private static void ConfigureUI(WebAssemblyHostBuilder builder)
        {
            builder.RootComponents.Add<App>("#ApplicationContainer");
        }

        private static void ConfigureHttpClient(ServiceConfigurationContext context, IWebAssemblyHostEnvironment environment)
        {
            context.Services.AddTransient(sp => new HttpClient
            {
                BaseAddress = new Uri(environment.BaseAddress)
            });
        }

        private void ConfigureAutoMapper(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<MembersBlazorHostModule>();
            });
        }
    }
}
