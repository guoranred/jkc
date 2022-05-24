using EasyAbp.Abp.DataDictionary;
using Jiepei.ERP.Members;
using Jiepei.ERP.Orders;
using Jiepei.ERP.Suppliers;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Jiepei.ERP
{
    [DependsOn(
        typeof(ERPDomainModule),
        typeof(ERPApplicationContractsModule)
        )]
    [DependsOn(
        typeof(OrdersApplicationModule),
        typeof(SuppliersApplicationModule),
        typeof(MembersApplicationModule),
        typeof(AbpDataDictionaryApplicationModule)
        )]
    public class ERPApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<ERPApplicationModule>();
            });
        }
    }
}
