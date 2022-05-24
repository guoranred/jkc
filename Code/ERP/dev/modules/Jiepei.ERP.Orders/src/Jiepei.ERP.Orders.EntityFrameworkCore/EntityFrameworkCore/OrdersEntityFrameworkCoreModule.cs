
using Jiepei.ERP.Orders.Materials;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Orders.SubOrders;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Orders.EntityFrameworkCore
{
    [DependsOn(
        typeof(OrdersDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class OrdersEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<OrdersDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddDefaultRepositories(includeAllEntities: true);
                options.AddRepository<Order, OrderRepository>();
                //options.AddRepository<OrderLog, OrderLogRepository>();
                //options.AddRepository<OrderDelivery, OrderDeliveryRepository>();

                //options.AddRepository<MoldOrder, MoldOrderRepository>();
                //options.AddRepository<MoldOrderFlow, MoldOrderFlowRepository>();

                //options.AddRepository<CncOrder,CncOrderRepository>();
                //options.AddRepository<CncOrderFlow, CncOrderFlowRepository>();
                //options.AddRepository<CncOrderBom, CncOrderBomRepository>();
                //options.AddRepository<InjectionOrder, InjectionOrderRepository>();
                options.AddRepository<SubOrder, SubOrderRepository>();
                options.AddRepository<D3Material, D3MaterialRepository>();
                options.AddRepository<D3Material, D3MaterialRepository>();
            });

        }
    }
}