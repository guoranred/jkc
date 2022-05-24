using Jiepei.ERP.News.Localization;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.News.Admin
{
    [DependsOn(
       typeof(NewsAdminApplicationContractsModule),
       typeof(AbpAspNetCoreMvcModule))]
    public class NewsAdminHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(NewsAdminHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<NewsResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
