using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Jiepei.InTradeConsumer.InjectionOrders;
using Jiepei.InTradeConsumer.Domain.OrderMains;
using Jiepei.InTradeConsumer.OrderDetails;
using Volo.Abp.Uow;
using Jiepei.ERP.Injections;
using Jiepei.InTradeConsumer.Domain.Shareds;
using Jiepei.ERP.EventBus.Shared.Injections;
using Microsoft.Extensions.Logging;
using Jiepei.InTradeConsumer.Domain.InjectionOrders;

namespace Jiepi.InTradeConsumer.Service.Injections
{
    /// <summary>
    /// 订单发货
    /// </summary>
    public class DeliveryDaysEventHandler : IDistributedEventHandler<DeliveryDaysInjectionEto>, ITransientDependency
    {
        private readonly IRepository<InjectionOrder, int> _orderRepository;
        private readonly ILogger<DeliveryDaysEventHandler> _logger;

        public DeliveryDaysEventHandler(IRepository<InjectionOrder, int> orderRepository
            , ILogger<DeliveryDaysEventHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task HandleEventAsync(DeliveryDaysInjectionEto eventData)
        {
            if (!await ValidAsync(eventData))
            {
                return;
            }

            await ChangeDeliveryDaysAsync(eventData);
        }

        [UnitOfWork]
        protected virtual async Task<bool> ValidAsync(DeliveryDaysInjectionEto eventData)
        {
            if (eventData == null)
            {
                _logger.LogError("系统异常");
                return default;
            }

            var order = await _orderRepository.FindAsync(t => t.OrderNo == eventData.OrderNo);
            if (order == null)
            {
                _logger.LogError($"未查询到当前注塑订单{eventData.OrderNo}信息");
                return default;
            }
            if (eventData.DeliveryDays<3) {

                _logger.LogError($"交期不得小于3");
                return default;
            }
            return true;
        }

        [UnitOfWork]
        protected virtual async Task ChangeDeliveryDaysAsync(DeliveryDaysInjectionEto eventData)
        {
            var entity = await _orderRepository.FindAsync(t => t.OrderNo == eventData.OrderNo);
            entity.SetDeliveryDays(eventData.DeliveryDays);
            await _orderRepository.UpdateAsync(entity);
        }
    }
}
