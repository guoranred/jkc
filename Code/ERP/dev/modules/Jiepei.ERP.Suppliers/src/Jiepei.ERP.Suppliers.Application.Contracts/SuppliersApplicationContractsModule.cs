using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Suppliers
{
    [DependsOn(
        typeof(SuppliersDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class SuppliersApplicationContractsModule : AbpModule
    {

    }
}
