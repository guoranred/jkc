
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.News
{
    [DependsOn(
        typeof(NewsAdminApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class NewsAdminHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "News";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(NewsAdminApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
