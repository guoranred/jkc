using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Members.EntityFrameworkCore
{
    public class MembersHttpApiHostMigrationsDbContext : AbpDbContext<MembersHttpApiHostMigrationsDbContext>
    {
        public MembersHttpApiHostMigrationsDbContext(DbContextOptions<MembersHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureMembers();
        }
    }
}
