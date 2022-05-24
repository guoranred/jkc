using Volo.Abp.Modularity;

namespace Jiepei.ERP.Members
{
    [DependsOn(
        typeof(MembersApplicationModule),
        typeof(MembersDomainTestModule)
        )]
    public class MembersApplicationTestModule : AbpModule
    {

    }
}
