using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Jiepei.ERP.Suppliers.EntityFrameworkCore
{
    public class SuppliersHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<SuppliersHttpApiHostMigrationsDbContext>
    {
        public SuppliersHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<SuppliersHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Suppliers"));

            return new SuppliersHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
