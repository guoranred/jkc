using Jiepei.ERP.Orders.Application.External.Cache;
using Jiepei.ERP.Orders.Application.External.Configuration;
using Jiepei.ERP.Orders.Application.External.Order;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Orders.Application.External
{
    [DependsOn(typeof(AbpCachingModule))]
    public class BeetleExternalServiceModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            //拦截器注册
            context.Services.OnRegistred(ctx =>
            {
                var type = ctx.ImplementationType;
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            ConfigureRemoteApi(configuration, context.Services);

            context.Services.AddTransient<OrderExternalService>();
            context.Services.AddSingleton<SheetMetalServiceHelper>();
        }

        private void ConfigureRemoteApi(IConfiguration configuration, IServiceCollection service)
        {
            //将appSetting中钣金站远程配置设置为单例放入IOC容器中
            var sheetMetalApiConfiguration = configuration.GetSection(nameof(SheetMetalApiConfiguration))
                .Get<SheetMetalApiConfiguration>();
            service.AddSingleton(sheetMetalApiConfiguration);
        }
    }
}
