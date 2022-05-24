using Jiepei.ERP.Members.Localization;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Members.Admin
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
    typeof(MembersAdminApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
    public class MembersAdminHttpApiModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(MembersAdminHttpApiModule).Assembly);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<MembersResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
