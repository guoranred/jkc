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
    /// <summary>
    /// 订单审批
    /// </summary>
    public class CheckEventHandler : IDistributedEventHandler<CheckCncEto>, ITransientDependency
    {
        private readonly IRepository<CncOrder, int> _orderRepository;
        private readonly IRepository<OrderDetail, int> _orderDetailRepository;
        private readonly IRepository<OrderMain, int> _orderMainRepository;
        private readonly ILogger<CheckEventHandler> _logger;

        public CheckEventHandler(IRepository<CncOrder, int> orderRepository
            , IRepository<OrderDetail, int> orderDetailRepository
            , IRepository<OrderMain, int> orderMainRepository
            , ILogger<CheckEventHandler> logger)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderMainRepository = orderMainRepository;
            _logger = logger;
        }

        public async Task HandleEventAsync(CheckCncEto eventData)
        {

            if (!await ValidAsync(eventData))
            {
                return;
            }

            await ChangeOrderStatusAsync(eventData);
            var mainId = await ChangeOrderDetailStatusAsync(eventData);
            await ChangeOrderMainStatusAsync(mainId);
        }

        [UnitOfWork]
        protected virtual async Task<bool> ValidAsync(CheckCncEto eventData)
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

            if (order.Status != (int)EnumCncOrderStatus.WaitCheck
                && order.Status != (int)EnumCncOrderStatus.CheckedNoPass
                && order.Status != (int)EnumCncOrderStatus.CheckedPass)
            {
                _logger.LogError($"当前CNC订单{eventData.OrderNo}不符合订单审核状态");
                return default;
            }
            return true;
        }

        [UnitOfWork]
        protected virtual async Task ChangeOrderStatusAsync(CheckCncEto eventData)
        {
            var order = await _orderRepository.FindAsync(t => t.OrderNo == eventData.OrderNo);
            order.Check((int)eventData.Status, eventData.Remark);
            await _orderRepository.UpdateAsync(order);
        }

        [UnitOfWork]
        protected virtual async Task<int> ChangeOrderDetailStatusAsync(CheckCncEto eventData)
        {
            var orderDetail = await _orderDetailRepository.FindAsync(t => t.ProName == eventData.OrderNo);
            //orderDetail.SetStatus((int)EnumOrderDetailStatus.WaitCheck);
            if (eventData.Status == EnumCncOrderStatus.CheckedPass)
            {
                orderDetail.SetStatus((int)EnumOrderDetailStatus.CheckedPass);
            }
            if (eventData.Status == EnumCncOrderStatus.CheckedNoPass)
            {
                orderDetail.SetStatus((int)EnumOrderDetailStatus.CheckedNoPass);
            }
            await _orderDetailRepository.UpdateAsync(orderDetail);
            return orderDetail?.MainId ?? 0;
        }

        [UnitOfWork]
        protected virtual async Task ChangeOrderMainStatusAsync(int id)
        {
            var entity = await _orderMainRepository.FindAsync(t => t.Id == id);
            entity.SetStatus((int)EnumOrderMainStatus.WaitSure);
            await _orderMainRepository.UpdateAsync(entity);
        }
    }
}