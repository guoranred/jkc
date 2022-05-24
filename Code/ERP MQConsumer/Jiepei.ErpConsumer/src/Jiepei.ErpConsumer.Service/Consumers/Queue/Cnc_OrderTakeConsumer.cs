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
    public class Cnc_OrderTakeConsumer : ITransientInject
    {
        readonly ILogger _logger;
        readonly RabbitMQConsumer _consumer;
        readonly IConfiguration _configuration;
        readonly IOrderDataService _orderDataService;

        public Cnc_OrderTakeConsumer(ILogger<Mold_OrderTakeConsumer> logger
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
            _consumer.Start<MQ_Cnc_OrderTaskDto>(RabbitMQConstant.Cnc.Exchange_OrderTake
            , RabbitMQConstant.Cnc.Queue_OrderTake
            , RabbitMQConstant.Cnc.Route_OrderTake
            , OnMessageReceived);
        }

        public void Stop()
        {
            _consumer.Stop();
        }

        private async Task OnMessageReceived(object sender, MessageReceivedEventArgs<MQ_Cnc_OrderTaskDto> args)
        {
            var message = args.Data;
            var url = _configuration["Erp:Cnc:TaskUrl"];
            var result = await _orderDataService.OrderTaskAsync(message, url, EnumHttpClientType.Post);
            if (!result)
            {
                _logger.LogError("Cnc 订单同步成功执行失败,失败对象：" + JsonConvert.SerializeObject(message));
                throw new Exception();
            }
        }
    }
}
