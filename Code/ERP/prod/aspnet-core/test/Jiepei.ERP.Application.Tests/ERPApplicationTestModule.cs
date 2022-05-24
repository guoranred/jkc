using Volo.Abp.Modularity;

namespace Jiepei.ERP
{
    [DependsOn(
        typeof(ERPApplicationModule),
        typeof(ERPDomainTestModule)
        )]
    public class ERPApplicationTestModule : AbpModule
    {

    }
}