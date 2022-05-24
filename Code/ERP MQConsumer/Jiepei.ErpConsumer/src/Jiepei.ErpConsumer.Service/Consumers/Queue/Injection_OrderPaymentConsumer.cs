using System;
using System.Threading.Tasks;
using Jiepei.ERP.Shared.Consumers;
using Jiepei.ERP.Shared.Consumers.Orders;
using Jiepei.ErpConsumer.Business;
using Jiepei.ErpConsumer.Business.Contracts;
using Jiepei.ErpConsumer.Service.Consumers.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Jiepei.ErpConsumer.Service.Consumers
{
    /// <summary>
    /// 注塑 订单支付
    /// </summary>
    public class Injection_OrderPaymentConsumer : ITransientInject
    {
        readonly ILogger _logger;
        readonly RabbitMQConsumer _consumer;
        readonly IConfiguration _configuration;
        readonly IOrderDataService _orderDataService;

        public Injection_OrderPaymentConsumer(ILogger<Injection_OrderPaymentConsumer> logger
           , RabbitMQConsumer consumer
           , IConfiguration configuration
           , IOrderDataService orderDataService)
        {
            _logger = logger;
            _consumer = consumer;
            _configuration = configuration;
            _orderDataService = orderDataService;
        }

        public void Start()
        {
            _consumer.Start<MQ_Injection_OrderPaymentDto>(RabbitMQConstant.Injection.Exchange_OrderPayment
            , RabbitMQConstant.Injection.Queue_OrderPayment
            , RabbitMQConstant.Injection.Route_OrderPayment
            , OnMessageReceived);
        }

        public void Stop()
        {
            _consumer.Stop();
        }

        private async Task OnMessageReceived(object sender, MessageReceivedEventArgs<MQ_Injection_OrderPaymentDto> args)
        {
            var message = args.Data;
            var url = _configuration["Erp:Injection:PaymentUrl"];
            var result = await _orderDataService.OrderTaskAsync(message, url, EnumHttpClientType.Put);
            if (!result)
            {
                _logger.LogError("注塑订单支付执行失败,失败对象：" + JsonConvert.SerializeObject(message));
                throw new Exception();
            }
        }
    }
}
