using EasyAbp.Abp.DataDictionary;
using Jiepei.ERP.Members;
using Jiepei.ERP.News;
using Jiepei.ERP.Orders;
using Jiepei.ERP.Suppliers;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.SettingManagement;

namespace Jiepei.ERP
{
    [DependsOn(
        typeof(ERPDomainSharedModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpSettingManagementApplicationContractsModule),
        typeof(AbpObjectExtendingModule)
    )]
    [DependsOn(
        typeof(OrdersApplicationContractsModule),
        typeof(SuppliersApplicationContractsModule),
        typeof(MembersApplicationContractsModule),
        typeof(NewsApplicationContractsModule),
        typeof(AbpDataDictionaryApplicationContractsModule),
        typeof(AbpDataDictionaryApplicationContractsSharedModule)
        )]
    public class ERPApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            ERPDtoExtensions.Configure();
        }
    }
}
