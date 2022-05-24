using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Members.Admin
{
    [DependsOn(
        typeof(MembersDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class MembersAdminApplicationContractsModule : AbpModule
    {

    }
}
