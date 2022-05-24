using Jiepei.ERP.News;
using Jiepei.ERP.Orders;
using Jiepei.ERP.Suppliers;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace Jiepei.ERP
{
    [DependsOn(
        typeof(ERPApplicationContractsModule),
        typeof(AbpFeatureManagementHttpApiClientModule),
        typeof(AbpSettingManagementHttpApiClientModule)
    )]
    [DependsOn(
        typeof(OrdersHttpApiClientModule),
        typeof(NewsHttpApiClientModule),
        typeof(SuppliersHttpApiClientModule)
    )]
    public class ERPHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(ERPApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
