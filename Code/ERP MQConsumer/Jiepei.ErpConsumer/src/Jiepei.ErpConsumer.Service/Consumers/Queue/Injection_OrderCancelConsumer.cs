using Jiepei.ERP.Shared.Consumers;
using Jiepei.ERP.Shared.Consumers.Orders;
using Jiepei.ErpConsumer.Business;
using Jiepei.ErpConsumer.Business.Contracts;
using Jiepei.ErpConsumer.Service.Consumers.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Jiepei.ErpConsumer.Service.Consumers
{
    /// <summary>
    /// 注塑订单取消
    /// </summary>
    public class Injection_OrderCancelConsumer : ITransientInject
    {
        readonly ILogger _logger;
        readonly RabbitMQConsumer _consumer;
        readonly IConfiguration _configuration;
        readonly IOrderDataService _orderDataService;

        public Injection_OrderCancelConsumer(ILogger<Injection_OrderCancelConsumer> logger
           , RabbitMQConsumer consumer
           , IConfiguration configuration
            , IOrderDataService orderDataService
          )
        {
            _logger = logger;
            _consumer = consumer;
            _configuration = configuration;
            _orderDataService = orderDataService;
        }

        public void Start()
        {
            //这里要确认队列信息，目前队列是根据不同的类型来加载的
            _consumer.Start<MQ_Injection_OrderCancelDto>(RabbitMQConstant.Injection.Exchange_OrderCancel
            , RabbitMQConstant.Injection.Queue_OrderCancel
            , RabbitMQConstant.Injection.Route_OrderCancel
            , OnMessageReceived);
        }

        public void Stop()
        {
            _consumer.Stop();
        }

        private async Task OnMessageReceived(object sender, MessageReceivedEventArgs<MQ_Injection_OrderCancelDto> args)
        {
            var message = args.Data;
            var url = _configuration["Erp:Injection:CancelUrl"];
            var result = await _orderDataService.OrderTaskAsync(message, url, EnumHttpClientType.Put);
            if (!result)
            {
                _logger.LogError("注塑订单取消执行失败,失败对象：" + JsonConvert.SerializeObject(message));
                throw new Exception();
            }
        }
    }
}
