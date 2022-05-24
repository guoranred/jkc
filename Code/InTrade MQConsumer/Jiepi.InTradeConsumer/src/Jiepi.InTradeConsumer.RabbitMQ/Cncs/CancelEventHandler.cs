using Jiepei.ERP.EventBus.Shared.Cncs;
using Jiepei.ERP.Cncs;
using Jiepei.InTradeConsumer.Domain.OrderMains;
using Jiepei.InTradeConsumer.Domain.Shareds;
using Jiepei.InTradeConsumer.OrderDetails;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;
using Jiepei.InTradeConsumer.Domain.CncOrders;

namespace Jiepi.InTradeConsumer.Service.Cncs
{
    public class CancelEventHandler : IDistributedEventHandler<CancelCncEto>, ITransientDependency
    {
        private readonly IRepository<CncOrder, int> _orderRepository;
        private readonly IRepository<OrderDetail, int> _orderDetailRepository;
        private readonly IRepository<OrderMain, int> _orderMainRepository;
        private readonly ILogger<CancelEventHandler> _logger;

        public CancelEventHandler(
            IRepository<CncOrder, int> orderRepository,
            IRepository<OrderDetail, int> orderDetailRepository,
            IRepository<OrderMain, int> orderMainRepository,
            ILogger<CancelEventHandler> logger)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderMainRepository = orderMainRepository;
            _logger = logger;
        }

        public async Task HandleEventAsync(CancelCncEto eventData)
        {
            if (!await ValidAsync(eventData))
            {
                return;
            }

            await CancelOrderAsync(eventData);
            await CancelOrderMainAsync(await CanceleOrderDetailAsync(eventData));
        }

        [UnitOfWork]
        protected virtual async Task<bool> ValidAsync(CancelCncEto eventData)
        {         
            if (eventData == null)
            {
                _logger.LogError("系统异常");
                return default;
            }

            var order = await _orderRepository.FindAsync(t => t.OrderNo == eventData.OrderNo);
            if (order == null)
            {
                _logger.LogError($"未查询到当前CNC订单{eventData.OrderNo}信息");
                return default;
            }

            if (order.Status >= (int)EnumCncOrderStatus.SureOrder)
            {
                _logger.LogError($"订单{eventData.OrderNo}不符合取消条件");
                return default;
            }

            return true;
        }

        [UnitOfWork]
        protected virtual async Task CancelOrderAsync(CancelCncEto eventData)
        {
            var entity = await _orderRepository.FindAsync(t => t.OrderNo == eventData.OrderNo);
            entity.SetStaus((int)EnumCncOrderStatus.Cancel);
            await _orderRepository.UpdateAsync(entity);
        }

        [UnitOfWork]
        protected virtual async Task<int> CanceleOrderDetailAsync(CancelCncEto eventData)
        {
            var entity = await _orderDetailRepository.FindAsync(t => t.ProName == eventData.OrderNo);
            entity.SetStatus((int)EnumOrderDetailStatus.Cancel);
            await _orderDetailRepository.UpdateAsync(entity);

            return entity?.MainId ?? 0;
        }

        [UnitOfWork]
        protected virtual async Task CancelOrderMainAsync(int id)
        {
            var entity = await _orderMainRepository.FindAsync(t => t.Id == id);
            entity.SetStatus((int)EnumOrderMainStatus.CancelOrder);
            await _orderMainRepository.UpdateAsync(entity);
        }
    }
}
