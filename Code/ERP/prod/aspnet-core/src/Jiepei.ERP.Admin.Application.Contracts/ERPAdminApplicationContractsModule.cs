using Volo.Abp.Account;
using Volo.Abp.AuditLogging;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.LanguageManagement;
using Volo.Abp.LeptonTheme.Management;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TextTemplateManagement;
using EasyAbp.Abp.DataDictionary;
using Jiepei.ERP.Orders.Admin;
using Jiepei.ERP.Members.Admin;
using Jiepei.ERP.Suppliers;

namespace Jiepei.ERP
{
    [DependsOn(
        typeof(ERPDomainSharedModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpSettingManagementApplicationContractsModule),
        typeof(AbpAuditLoggingApplicationContractsModule),
        typeof(AbpIdentityServerApplicationContractsModule),
        typeof(AbpAccountPublicApplicationContractsModule),
        typeof(AbpAccountAdminApplicationContractsModule),
        typeof(LanguageManagementApplicationContractsModule),
        typeof(LeptonThemeManagementApplicationContractsModule),
        typeof(TextTemplateManagementApplicationContractsModule)
    )]
    [DependsOn(
        typeof(OrdersAdminApplicationContractsModule),
        typeof(MembersAdminApplicationContractsModule),
        typeof(SuppliersApplicationContractsModule)
        )]
    [DependsOn(typeof(AbpDataDictionaryApplicationContractsModule))]
    public class ERPAdminApplicationContractsModule : AbpModule
    {

    }
}
