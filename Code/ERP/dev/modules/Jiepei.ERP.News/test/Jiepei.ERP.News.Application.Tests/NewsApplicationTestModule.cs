using Volo.Abp.Modularity;

namespace Jiepei.ERP.News
{
    [DependsOn(
        typeof(NewsApplicationModule),
        typeof(NewsDomainTestModule)
        )]
    public class NewsApplicationTestModule : AbpModule
    {

    }
}
