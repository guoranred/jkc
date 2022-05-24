using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.News
{
    [DependsOn(
        typeof(NewsHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class NewsConsoleApiClientModule : AbpModule
    {
        
    }
}
