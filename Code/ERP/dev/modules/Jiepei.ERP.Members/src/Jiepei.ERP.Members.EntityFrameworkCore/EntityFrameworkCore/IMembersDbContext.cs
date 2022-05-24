using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Members.EntityFrameworkCore
{
    [ConnectionStringName(MembersDbProperties.ConnectionStringName)]
    public interface IMembersDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<CustomerService> CustomerServices { get; set; }

        DbSet<MemberAddress> MemberAddress { get; set; }

        DbSet<MemberInformation> MemberInformation { get; set; }
    }
}