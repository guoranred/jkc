using Jiepei.ERP.Suppliers.Suppliers;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Suppliers.EntityFrameworkCore
{
    [ConnectionStringName(SuppliersDbProperties.ConnectionStringName)]
    public class SuppliersDbContext : AbpDbContext<SuppliersDbContext>, ISuppliersDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public DbSet<Supplier> Suppliers { get; set; }

        public SuppliersDbContext(DbContextOptions<SuppliersDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureSuppliers();
        }
    }
}