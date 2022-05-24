using Volo.Abp.Modularity;

namespace Jiepei.ERP.Orders
{
    [DependsOn(
        typeof(OrdersApplicationModule),
        typeof(OrdersDomainTestModule)
        )]
    public class OrdersApplicationTestModule : AbpModule
    {

    }
}
