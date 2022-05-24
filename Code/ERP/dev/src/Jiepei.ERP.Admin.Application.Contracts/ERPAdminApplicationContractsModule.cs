using EasyAbp.Abp.DataDictionary;
using Jiepei.ERP.Members.Admin;
using Jiepei.ERP.News;
using Jiepei.ERP.Orders.Admin;
using Jiepei.ERP.Suppliers;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace Jiepei.ERP.Admin
{
    [DependsOn(
        typeof(ERPDomainSharedModule),
        typeof(AbpAccountApplicationContractsModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpSettingManagementApplicationContractsModule),
        typeof(AbpObjectExtendingModule))]
    [DependsOn(
        typeof(OrdersAdminApplicationContractsModule),
        typeof(MembersAdminApplicationContractsModule),
        typeof(SuppliersApplicationContractsModule),
        typeof(NewsAdminApplicationContractsModule),
        typeof(AbpDataDictionaryApplicationContractsModule)
    )]
    public class ERPAdminApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            ERPAdminDtoExtensions.Configure();
        }
    }
}
