using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Members
{
    [DependsOn(
        typeof(MembersApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class MembersHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Members";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(MembersApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
