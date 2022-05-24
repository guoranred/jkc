using EasyAbp.Abp.DataDictionary.HttpApi;
using Jiepei.ERP.DeliverCentersClient;
using Jiepei.ERP.Localization;
using Jiepei.ERP.Members.Admin;
using Jiepei.ERP.News.Admin;
using Jiepei.ERP.Orders.Admin;
using Jiepei.ERP.Suppliers;
using Localization.Resources.AbpUi;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;

namespace Jiepei.ERP.Admin
{
    [DependsOn(
        typeof(ERPAdminApplicationContractsModule),
        typeof(AbpAccountHttpApiModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpFeatureManagementHttpApiModule),
        typeof(AbpSettingManagementHttpApiModule))]
    [DependsOn(
        typeof(OrdersAdminHttpApiModule),
        typeof(MembersAdminHttpApiModule),
        typeof(SuppliersHttpApiModule),
        typeof(NewsAdminHttpApiModule),
        typeof(AbpDataDictionaryHttpApiModule),
        typeof(DeliverCentersClientModule)
        )]
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
