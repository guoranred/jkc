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
    /// Cnc 订单支付
    /// </summary>
    public class Cnc_OrderPaymentConsumer : ITransientInject
    {
        readonly ILogger _logger;
        readonly RabbitMQConsumer _consumer;
        readonly IConfiguration _configuration;
        readonly IOrderDataService _orderDataService;

        public Cnc_OrderPaymentConsumer(ILogger<Cnc_OrderPaymentConsumer> logger
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
            _consumer.Start<MQ_Cnc_OrderPaymentDto>(RabbitMQConstant.Cnc.Exchange_OrderPayment
            , RabbitMQConstant.Cnc.Queue_OrderPayment
            , RabbitMQConstant.Cnc.Route_OrderPayment
            , OnMessageReceived);
        }

        public void Stop()
        {
            _consumer.Stop();
        }

        private async Task OnMessageReceived(object sender, MessageReceivedEventArgs<MQ_Cnc_OrderPaymentDto> args)
        {
            var message = args.Data;
            var url = _configuration["Erp:Cnc:PaymentUrl"];
            var result = await _orderDataService.OrderTaskAsync(message, url, EnumHttpClientType.Put);
            if (!result)
            {
                _logger.LogError("Cnc 订单支付执行失败,失败对象：" + JsonConvert.SerializeObject(message));
                throw new Exception();
            }
        }
    }
}
