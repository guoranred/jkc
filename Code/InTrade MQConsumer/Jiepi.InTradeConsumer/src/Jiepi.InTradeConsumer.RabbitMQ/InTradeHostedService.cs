using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;

namespace Jiepi.InTradeConsumer.Service
{
    public class InTradeHostedService : BackgroundService
    {
        private readonly IAbpApplicationWithExternalServiceProvider _application;
        private readonly IServiceProvider _serviceProvider;

        public InTradeHostedService(
            IAbpApplicationWithExternalServiceProvider application,
            IServiceProvider serviceProvider)
        {
            _application = application;
            _serviceProvider = serviceProvider;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _application.Initialize(_serviceProvider);

            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _application.Shutdown();

            await base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //throw new NotImplementedException();
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    await Task.Delay(1000, stoppingToken);
            //}
            //_application.Initialize(_serviceProvider);
            await Task.CompletedTask;
        }
    }
}