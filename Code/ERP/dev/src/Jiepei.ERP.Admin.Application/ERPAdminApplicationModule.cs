using EasyAbp.Abp.DataDictionary;
using Jiepei.ERP.Members.Admin;
using Jiepei.ERP.News;
using Jiepei.ERP.Orders.Admin;
using Jiepei.ERP.Suppliers;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace Jiepei.ERP.Admin
{
    [DependsOn(
        typeof(ERPDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(ERPAdminApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpSettingManagementApplicationModule))]
    [DependsOn(
        typeof(OrdersAdminApplicationModule),
        typeof(MembersAdminApplicationModule),
        typeof(SuppliersApplicationModule),
        typeof(NewsAdminApplicationModule),
        typeof(AbpDataDictionaryApplicationModule)
        )]
    public class ERPAdminApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<ERPAdminApplicationModule>();
            });
        }
    }
}
