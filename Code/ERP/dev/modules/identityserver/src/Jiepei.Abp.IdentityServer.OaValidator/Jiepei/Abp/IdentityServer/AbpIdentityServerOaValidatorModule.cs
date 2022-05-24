using Jiepei.Abp.IdentityServer.OaValidator;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.IdentityServer;
using Volo.Abp.IdentityServer.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Jiepei.Abp.IdentityServer
{
    [DependsOn(typeof(AbpIdentityServerDomainModule))]
    public class AbpIdentityServerOaValidatorModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IIdentityServerBuilder>(builder =>
            {
                builder.AddExtensionGrantValidator<OaTokenGrantValidator>();
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpIdentityServerOaValidatorModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<AbpIdentityServerResource>()
                    .AddVirtualJson("/Jiepei/Abp/IdentityServer/Localization/OaValidator");
            });
        }
    }
}
