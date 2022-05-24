using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Orders.Admin
{
    [DependsOn(
        typeof(OrdersAdminApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class OrdersAdminHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Orders";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(OrdersAdminApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
