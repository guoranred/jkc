using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Members
{
    [DependsOn(
        typeof(MembersDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class MembersApplicationContractsModule : AbpModule
    {

    }
}
