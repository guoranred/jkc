using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Suppliers
{
    [DependsOn(
        typeof(SuppliersHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class SuppliersConsoleApiClientModule : AbpModule
    {
        
    }
}
