using EasyAbp.Abp.DataDictionary.HttpApi;
using Jiepei.ERP.Localization;
using Jiepei.ERP.Members;
using Jiepei.ERP.Orders;
using Jiepei.ERP.Suppliers;
using Localization.Resources.AbpUi;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Jiepei.ERP
{
    [DependsOn(
        typeof(ERPApplicationContractsModule)
        )]
    [DependsOn(
        typeof(OrdersHttpApiModule),
        typeof(SuppliersHttpApiModule),
        typeof(MembersHttpApiModule)
        )]
    [DependsOn(typeof(AbpDataDictionaryHttpApiModule))]
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
