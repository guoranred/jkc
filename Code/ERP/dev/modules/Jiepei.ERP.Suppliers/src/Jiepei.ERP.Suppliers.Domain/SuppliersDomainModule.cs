using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Suppliers
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(SuppliersDomainSharedModule)
    )]
    public class SuppliersDomainModule : AbpModule
    {

    }
}
