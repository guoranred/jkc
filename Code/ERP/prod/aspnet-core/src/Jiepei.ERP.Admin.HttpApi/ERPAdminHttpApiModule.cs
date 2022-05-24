using EasyAbp.Abp.DataDictionary.HttpApi;
using Jiepei.ERP.Localization;
using Jiepei.ERP.Members.Admin;
using Jiepei.ERP.Orders.Admin;
using Jiepei.ERP.Suppliers;
using Localization.Resources.AbpUi;
using Volo.Abp.Account;
using Volo.Abp.AuditLogging;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.LanguageManagement;
using Volo.Abp.LeptonTheme;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TextTemplateManagement;

namespace Jiepei.ERP
{
    [DependsOn(
        typeof(ERPAdminApplicationContractsModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpFeatureManagementHttpApiModule),
        typeof(AbpSettingManagementHttpApiModule),
        typeof(AbpAuditLoggingHttpApiModule),
        typeof(AbpIdentityServerHttpApiModule),
        typeof(AbpAccountAdminHttpApiModule),
        typeof(AbpAccountPublicHttpApiModule),
        typeof(LanguageManagementHttpApiModule),
        typeof(LeptonThemeManagementHttpApiModule),
        typeof(TextTemplateManagementHttpApiModule)
        )]
    [DependsOn(
        typeof(OrdersAdminHttpApiModule),
        typeof(MembersAdminHttpApiModule),
        typeof(SuppliersHttpApiModule)
        )]
    [DependsOn(typeof(AbpDataDictionaryHttpApiModule))]
    public class ERPAdminHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureLocalization();
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<ERPResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );
            });
        }
    }
}
