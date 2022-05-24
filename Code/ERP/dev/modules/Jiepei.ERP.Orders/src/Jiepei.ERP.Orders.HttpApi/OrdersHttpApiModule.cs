using Jiepei.ERP.Orders.Localization;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Orders
{
    [DependsOn(
        typeof(OrdersApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class OrdersHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(OrdersHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<OrdersResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
