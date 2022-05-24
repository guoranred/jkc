using Jiepei.ERP.Suppliers.Suppliers;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Suppliers.EntityFrameworkCore
{
    [ConnectionStringName(SuppliersDbProperties.ConnectionStringName)]
    public interface ISuppliersDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<Supplier> Suppliers { get; set; }
    }
}