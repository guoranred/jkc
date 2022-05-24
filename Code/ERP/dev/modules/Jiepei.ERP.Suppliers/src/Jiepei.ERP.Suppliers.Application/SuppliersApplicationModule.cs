using Jiepei.ERP.Suppliers.Unionfab;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Suppliers
{
    [DependsOn(
        typeof(SuppliersDomainModule),
        typeof(SuppliersApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(SupplierUnionfabModule)
        )]
    public class SuppliersApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<SuppliersApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<SuppliersApplicationModule>(validate: true);
            });
        }
    }
}
