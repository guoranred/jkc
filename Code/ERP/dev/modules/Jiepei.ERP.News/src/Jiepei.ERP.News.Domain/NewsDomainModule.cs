using Volo.Abp.Domain;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.News
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(NewsDomainSharedModule)
    )]
    public class NewsDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("en", "en", "English", "gb"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文", "cn"));
            });
        }

    }
}
