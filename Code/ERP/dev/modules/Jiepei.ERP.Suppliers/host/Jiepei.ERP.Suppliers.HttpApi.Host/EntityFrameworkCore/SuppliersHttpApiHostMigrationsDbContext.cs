using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Suppliers.EntityFrameworkCore
{
    public class SuppliersHttpApiHostMigrationsDbContext : AbpDbContext<SuppliersHttpApiHostMigrationsDbContext>
    {
        public SuppliersHttpApiHostMigrationsDbContext(DbContextOptions<SuppliersHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureSuppliers();
        }
    }
}
