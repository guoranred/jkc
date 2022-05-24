using Volo.Abp.Modularity;

namespace Jiepei.ERP.Suppliers
{
    [DependsOn(
        typeof(SuppliersApplicationModule),
        typeof(SuppliersDomainTestModule)
        )]
    public class SuppliersApplicationTestModule : AbpModule
    {

    }
}
