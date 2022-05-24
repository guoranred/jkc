using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.News
{
    [DependsOn(
        typeof(NewsDomainModule),
        typeof(NewsApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class NewsApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<NewsApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<NewsApplicationAutoMapperProfile>();
            });
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<NewsApplicationModule>(validate: true);
            });
        }
    }
}
