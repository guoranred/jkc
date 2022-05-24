using Jiepei.ERP.Members.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Members
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(MembersEntityFrameworkCoreTestModule)
        )]
    public class MembersDomainTestModule : AbpModule
    {
        
    }
}
