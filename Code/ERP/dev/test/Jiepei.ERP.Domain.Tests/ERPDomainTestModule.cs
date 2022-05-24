using Jiepei.ERP.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Jiepei.ERP
{
    [DependsOn(
        typeof(ERPEntityFrameworkCoreTestModule)
        )]
    public class ERPDomainTestModule : AbpModule
    {

    }
}