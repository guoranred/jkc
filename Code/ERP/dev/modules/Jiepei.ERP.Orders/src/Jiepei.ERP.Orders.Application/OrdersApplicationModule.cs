using Jiepei.ERP.DeliverCentersClient;
using Jiepei.ERP.Members;
using Jiepei.ERP.Orders.Application.External;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Orders
{
    [DependsOn(
        typeof(OrdersDomainModule),
        typeof(OrdersApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(BeetleExternalServiceModule),
        typeof(DeliverCentersClientModule),
        typeof(MembersApplicationModule)
        )]
    public class OrdersApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<OrdersApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<OrdersApplicationModule>();
            });
        }
    }
}
