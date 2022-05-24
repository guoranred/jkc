using Jiepei.ERP.Orders.Localization;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Orders.Admin
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
        typeof(OrdersAdminApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class OrdersAdminHttpApiModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(OrdersAdminHttpApiModule).Assembly);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
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
