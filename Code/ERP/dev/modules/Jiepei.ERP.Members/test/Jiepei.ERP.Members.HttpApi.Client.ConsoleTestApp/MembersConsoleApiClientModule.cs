using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Members
{
    [DependsOn(
        typeof(MembersHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class MembersConsoleApiClientModule : AbpModule
    {
        
    }
}
