using Jiepei.ERP.Members;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Orders
{
    [DependsOn(
        typeof(OrdersDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule),
        typeof(MembersApplicationContractsModule)
        )]
    public class OrdersApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            OrdersDtoExtensions.Configure();
        }
    }
}
