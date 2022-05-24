using Jiepei.ErpConsumer.Service.Consumers;
using Jiepei.ErpConsumer.Service.Consumers.Queue;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jiepei.ErpConsumer.Service
{
    //public class HostedService : IHostedService
    public class HostedService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        #region Molds
        private List<Mold_OrderTakeConsumer> _moldOrderTakeConsumers = new List<Mold_OrderTakeConsumer>();
        private List<Mold_OrderCancelConsumer> _moldOrderCancelConsumers = new List<Mold_OrderCancelConsumer>();
        private List<Mold_OrderPaymentConsumer> _moldOrderPaymentConsumers = new List<Mold_OrderPaymentConsumer>();
        //private List<Mold_OrderStageConsumer> _moldOrderStageConsumers = new List<Mold_OrderStageConsumer>();
        private List<Mold_OrderReceiveConsumer> _moldOrderReceiveConsumers = new List<Mold_OrderReceiveConsumer>();
        #endregion

        #region Injections
        private List<Injection_OrderTakeConsumer> _injectionOrderTakeConsumers = new List<Injection_OrderTakeConsumer>();
        private List<Injection_OrderCancelConsumer> _injectionOrderCancelConsumers = new List<Injection_OrderCancelConsumer>();
        private List<Injection_OrderPaymentConsumer> _injectionOrderPaymentConsumers = new List<Injection_OrderPaymentConsumer>();
        //private List<Injection_OrderStageConsumer> _injectionOrderStageConsumers = new List<Injection_OrderStageConsumer>();
        private List<Injection_OrderReceiveConsumer> _injectionOrderReceiveConsumers = new List<Injection_OrderReceiveConsumer>();
        #endregion

        #region Cnc
        private List<Cnc_OrderTakeConsumer> _CncOrderTakeConsumers = new List<Cnc_OrderTakeConsumer>();
        private List<Cnc_OrderCancelConsumer> _CncOrderCancelConsumers = new List<Cnc_OrderCancelConsumer>();
        private List<Cnc_OrderPaymentConsumer> _CncOrderPaymentConsumers = new List<Cnc_OrderPaymentConsumer>();
        private List<Cnc_OrderReceiveConsumer> _CncOrderReceiveConsumers = new List<Cnc_OrderReceiveConsumer>();
        #endregion

        public HostedService(ILogger<HostedService> logger
            , IServiceProvider serviceProvider
            , IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration;

            #region Molds
            for (var i = 0; i < 2; i++)
            {
                _moldOrderTakeConsumers.Add(_serviceProvider.GetService<Mold_OrderTakeConsumer>());
            }
            for (var i = 0; i < 2; i++)
            {
                _moldOrderCancelConsumers.Add(_serviceProvider.GetService<Mold_OrderCancelConsumer>());
            }

            for (var i = 0; i < 2; i++)
            {
                _moldOrderPaymentConsumers.Add(_serviceProvider.GetService<Mold_OrderPaymentConsumer>());
            }

            //for (var i = 0; i < 2; i++)
            //{
            //    _moldOrderStageConsumers.Add(_serviceProvider.GetService<Mold_OrderStageConsumer>());
            //}
            for (var i = 0; i < 2; i++)
            {
                _moldOrderReceiveConsumers.Add(_serviceProvider.GetService<Mold_OrderReceiveConsumer>());
            }
            #endregion

            #region Injections
            for (var i = 0; i < 2; i++)
            {
                _injectionOrderTakeConsumers.Add(_serviceProvider.GetService<Injection_OrderTakeConsumer>());
            }
            for (var i = 0; i < 2; i++)
            {
                _injectionOrderCancelConsumers.Add(_serviceProvider.GetService<Injection_OrderCancelConsumer>());
            }

            for (var i = 0; i < 2; i++)
            {
                _injectionOrderPaymentConsumers.Add(_serviceProvider.GetService<Injection_OrderPaymentConsumer>());
            }

            //for (var i = 0; i < 2; i++)
            //{
            //    _injectionOrderStageConsumers.Add(_serviceProvider.GetService<Injection_OrderStageConsumer>());
            //}
            for (var i = 0; i < 2; i++)
            {
                _injectionOrderReceiveConsumers.Add(_serviceProvider.GetService<Injection_OrderReceiveConsumer>());
            }
            #endregion

            #region Cnc
            for (var i = 0; i < 2; i++)
            {
                _CncOrderTakeConsumers.Add(_serviceProvider.GetService<Cnc_OrderTakeConsumer>());
            }
            for (var i = 0; i < 2; i++)
            {
                _CncOrderCancelConsumers.Add(_serviceProvider.GetService<Cnc_OrderCancelConsumer>());
            }

            for (var i = 0; i < 2; i++)
            {
                _CncOrderPaymentConsumers.Add(_serviceProvider.GetService<Cnc_OrderPaymentConsumer>());
            }
            for (var i = 0; i < 2; i++)
            {
                _CncOrderReceiveConsumers.Add(_serviceProvider.GetService<Cnc_OrderReceiveConsumer>());
            }
            #endregion
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            //var result = _configuration["RabbitMQConnection:rabbitmq"];
            _logger.LogInformation("Jiepei.ErpConsumer.Service 开始");
            return Task.Factory.StartNew(() =>
            {
                #region Molds
                foreach (var item in _moldOrderTakeConsumers)
                {
                    item.Start();
                }
                foreach (var item in _moldOrderCancelConsumers)
                {
                    item.Start();
                }

                foreach (var item in _moldOrderPaymentConsumers)
                {
                    item.Start();
                }
                //foreach (var item in _moldOrderStageConsumers)
                //{
                //    item.Start();
                //}
                foreach (var item in _moldOrderReceiveConsumers)
                {
                    item.Start();
                }
                #endregion

                #region Injections
                foreach (var item in _injectionOrderTakeConsumers)
                {
                    item.Start();
                }
                foreach (var item in _injectionOrderCancelConsumers)
                {
                    item.Start();
                }

                foreach (var item in _injectionOrderPaymentConsumers)
                {
                    item.Start();
                }
                //foreach (var item in _injectionOrderStageConsumers)
                //{
                //    item.Start();
                //}
                foreach (var item in _injectionOrderReceiveConsumers)
                {
                    item.Start();
                }
                #endregion

                #region Cnc
                foreach (var item in _CncOrderTakeConsumers)
                {
                    item.Start();
                }
                foreach (var item in _CncOrderCancelConsumers)
                {
                    item.Start();
                }
                foreach (var item in _CncOrderPaymentConsumers)
                {
                    item.Start();
                }
                foreach (var item in _CncOrderReceiveConsumers)
                {
                    item.Start();
                }
                #endregion

            }, cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("JiePei.ErpConsumer.Service 停止");
            return Task.Factory.StartNew(() =>
            {
                #region Molds
                foreach (var item in _moldOrderTakeConsumers)
                {
                    item.Stop();
                }
                foreach (var item in _moldOrderCancelConsumers)
                {
                    item.Stop();
                }
                foreach (var item in _moldOrderPaymentConsumers)
                {
                    item.Stop();
                }
                //foreach (var item in _moldOrderStageConsumers)
                //{
                //    item.Stop();
                //}
                foreach (var item in _moldOrderReceiveConsumers)
                {
                    item.Stop();
                }
                #endregion

                #region Injections
                foreach (var item in _injectionOrderTakeConsumers)
                {
                    item.Stop();
                }
                foreach (var item in _injectionOrderCancelConsumers)
                {
                    item.Stop();
                }

                foreach (var item in _injectionOrderPaymentConsumers)
                {
                    item.Stop();
                }
                //foreach (var item in _injectionOrderStageConsumers)
                //{
                //    item.Stop();
                //}
                foreach (var item in _injectionOrderReceiveConsumers)
                {
                    item.Stop();
                }
                #endregion

                #region Cnc
                foreach (var item in _CncOrderTakeConsumers)
                {
                    item.Stop();
                }
                foreach (var item in _CncOrderCancelConsumers)
                {
                    item.Stop();
                }
                foreach (var item in _CncOrderPaymentConsumers)
                {
                    item.Stop();
                }
                foreach (var item in _CncOrderReceiveConsumers)
                {
                    item.Stop();
                }
                #endregion

            }, cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.CompletedTask;
        }
    }
}
