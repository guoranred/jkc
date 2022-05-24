using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Orders
{
    [DependsOn(
        typeof(OrdersApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class OrdersHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Orders";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(OrdersApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }


    }
}
