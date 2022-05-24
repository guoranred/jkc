using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Jiepei.ERP.Members.EntityFrameworkCore
{
    public class MembersHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<MembersHttpApiHostMigrationsDbContext>
    {
        public MembersHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<MembersHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Members"));

            return new MembersHttpApiHostMigrationsDbContext(builder.Options);
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
