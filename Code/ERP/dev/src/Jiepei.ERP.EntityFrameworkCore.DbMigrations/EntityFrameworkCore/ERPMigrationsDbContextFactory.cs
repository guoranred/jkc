using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Jiepei.ERP.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class ERPMigrationsDbContextFactory : IDesignTimeDbContextFactory<ERPMigrationsDbContext>
    {
        public ERPMigrationsDbContext CreateDbContext(string[] args)
        {
            ERPEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<ERPMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new ERPMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Jiepei.ERP.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
