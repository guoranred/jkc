using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Members.EntityFrameworkCore
{
    [ConnectionStringName(MembersDbProperties.ConnectionStringName)]
    public class MembersDbContext : AbpDbContext<MembersDbContext>, IMembersDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<CustomerService> CustomerServices { get; set; }

        public DbSet<MemberAddress> MemberAddress { get; set; }

        public DbSet<MemberInformation> MemberInformation { get; set; }

        public DbSet<AdministrativeDivision> AdministrativeDivisions { get; set; }


        public MembersDbContext(DbContextOptions<MembersDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureMembers();
        }
    }
}