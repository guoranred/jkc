using EasyAbp.Abp.DataDictionary;
using Jiepei.ERP.Members;
using Jiepei.ERP.News;
using Jiepei.ERP.Orders;
using Jiepei.ERP.Suppliers;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace Jiepei.ERP
{
    [DependsOn(
        typeof(ERPDomainModule),
        typeof(ERPApplicationContractsModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpSettingManagementApplicationModule)
        )]
    [DependsOn(
        typeof(OrdersApplicationModule),
        typeof(SuppliersApplicationModule),
        typeof(MembersApplicationModule),
        typeof(NewsApplicationModule),
        typeof(AbpDataDictionaryApplicationModule))]
    //[DependsOn(typeof(AbpEventBusRabbitMqModule))]
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
