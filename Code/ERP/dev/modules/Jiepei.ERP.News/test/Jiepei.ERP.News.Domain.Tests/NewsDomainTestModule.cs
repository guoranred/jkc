using Jiepei.ERP.News.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.News
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(NewsEntityFrameworkCoreTestModule)
        )]
    public class NewsDomainTestModule : AbpModule
    {
        
    }
}
