using Jiepei.ERP.Suppliers.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Suppliers
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(SuppliersEntityFrameworkCoreTestModule)
        )]
    public class SuppliersDomainTestModule : AbpModule
    {
        
    }
}
