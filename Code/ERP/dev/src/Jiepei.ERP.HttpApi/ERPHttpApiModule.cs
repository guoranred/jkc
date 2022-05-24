using EasyAbp.Abp.DataDictionary.HttpApi;
using Jiepei.ERP.Localization;
using Jiepei.ERP.Members;
using Jiepei.ERP.News;
using Jiepei.ERP.Orders;
using Jiepei.ERP.Suppliers;
using Localization.Resources.AbpUi;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace Jiepei.ERP
{
    [DependsOn(
        typeof(ERPApplicationContractsModule),
        typeof(AbpFeatureManagementHttpApiModule),
        typeof(AbpSettingManagementHttpApiModule)
        )]
    [DependsOn(
        typeof(OrdersHttpApiModule),
        typeof(SuppliersHttpApiModule),
        typeof(MembersHttpApiModule),
        typeof(NewsHttpApiModule),
        typeof(AbpDataDictionaryHttpApiModule)
        )]
    public class ERPHttpApiModule : AbpModule
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
