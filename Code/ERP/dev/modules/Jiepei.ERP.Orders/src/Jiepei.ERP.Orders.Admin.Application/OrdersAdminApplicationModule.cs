using Jiepei.ERP.DeliverCentersClient;
using Jiepei.ERP.Members.Admin;
using Jiepei.ERP.Orders.Application.External;
using Jiepei.ERP.Suppliers;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Settings;

namespace Jiepei.ERP.Orders.Admin
{
    [DependsOn(
        typeof(OrdersDomainModule),
        typeof(OrdersAdminApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(BeetleExternalServiceModule),
        typeof(DeliverCentersClientModule))]
    [DependsOn(typeof(SuppliersApplicationModule), typeof(MembersAdminApplicationModule))]
    public class OrdersAdminApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<OrdersAdminApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<OrdersAdminApplicationAutoMapperProfile>();
            });

            Configure<AbpSettingOptions>(options =>
            {
                options.ValueProviders.Add<ConfigurationSettingValueProvider>();
            });
        }
    }
}
