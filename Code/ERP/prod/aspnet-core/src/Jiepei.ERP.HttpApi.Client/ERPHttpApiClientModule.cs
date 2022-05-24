using Jiepei.ERP.Orders;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Jiepei.ERP
{
    [DependsOn(
        typeof(ERPApplicationContractsModule)
    )]
    [DependsOn(typeof(OrdersHttpApiClientModule))]
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
