using Jiepei.InTradeConsumer;
using Jiepei.InTradeConsumer.EntityFrameworkCore;
using Jiepi.InTradeConsumer.Service;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;

namespace Jiepi.InTradeConsumer
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(InTradeConsumerEntityFrameworkCoreModule),
        typeof(InTradeConsumerDomainModule),
        typeof(AbpEventBusRabbitMqModule)
    )]
    public class InTradeRabbitMQConsumerModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHostedService<InTradeHostedService>();
        }
    }
}