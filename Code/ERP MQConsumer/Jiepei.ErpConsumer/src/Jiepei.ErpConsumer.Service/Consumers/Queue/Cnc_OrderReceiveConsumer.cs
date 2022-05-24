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

namespace Jiepei.ErpConsumer.Service.Consumers.Queue
{
    public class Cnc_OrderReceiveConsumer : ITransientInject
    {
        readonly ILogger _logger;
        readonly RabbitMQConsumer _consumer;
        readonly IConfiguration _configuration;
        readonly IOrderDataService _orderDataService;

        public Cnc_OrderReceiveConsumer(ILogger<Cnc_OrderTakeConsumer> logger
        , RabbitMQConsumer consumer
        , IConfiguration configuration
        , IOrderDataService orderDataService)
        {
            _logger = logger;
            _consumer = consumer;
            _configuration = configuration;
            _orderDataService = orderDataService;
        }

        public void Start() =>
    //这里要确认队列信息，目前队列是根据不同的类型来加载的
    _consumer.Start<MQ_Cnc_OrderReceiveDto>(RabbitMQConstant.Cnc.Exchange_OrderReceive
                                            , RabbitMQConstant.Cnc.Queue_OrderReceive
                                            , RabbitMQConstant.Cnc.Route_OrderReceive
                                            , OnMessageReceived);

        public void Stop()
        {
            _consumer.Stop();
        }

        private async Task OnMessageReceived(object sender, MessageReceivedEventArgs<MQ_Cnc_OrderReceiveDto> args)
        {
            var message = args.Data;
            var url = _configuration["Erp:Cnc:ReceiveUrl"];
            var result = await _orderDataService.OrderTaskAsync(message, url, EnumHttpClientType.Put);
            if (!result)
            {
                _logger.LogError("Cnc 订单收货失败,失败对象：" + JsonConvert.SerializeObject(message));
                throw new Exception();
            }
        }
    }
}
