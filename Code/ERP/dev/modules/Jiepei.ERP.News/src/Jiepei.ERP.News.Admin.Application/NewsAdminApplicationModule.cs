using Jiepei.ERP.News.Admin;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Settings;

namespace Jiepei.ERP.News
{
    [DependsOn(
        typeof(NewsDomainModule),
        typeof(NewsAdminApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class NewsAdminApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<NewsAdminApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<NewsAdminApplicationAutoMapperProfile>();
            });

            Configure<AbpSettingOptions>(options =>
            {
                options.ValueProviders.Add<ConfigurationSettingValueProvider>();
            });
            //Configure<AbpAutoMapperOptions>(options =>
            //{
            //    options.AddMaps<NewsAdminApplicationModule>(validate: true);
            //});
        }
    }
}
