using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Suppliers
{
    [DependsOn(
        typeof(SuppliersApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class SuppliersHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Suppliers";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(SuppliersApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
