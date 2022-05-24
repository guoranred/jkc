using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Orders.Admin
{
    [DependsOn(
    typeof(OrdersDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
    public class OrdersAdminApplicationContractsModule : AbpModule
    {
    }
}
