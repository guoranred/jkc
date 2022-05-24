using Jiepei.ERP.EventBus.Shared.Injections;
using Jiepei.ERP.Injections;
using Jiepei.InTradeConsumer.Domain.InjectionOrders;
using Jiepei.InTradeConsumer.Domain.OrderMains;
using Jiepei.InTradeConsumer.Domain.Shareds;
using Jiepei.InTradeConsumer.OrderDetails;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace Jiepi.InTradeConsumer.Service.Injections
{
    /// <summary>
    /// 订单审批
    /// </summary>
    public class CheckEventHandler : IDistributedEventHandler<CheckInjectionEto>, ITransientDependency
    {
        private readonly IRepository<InjectionOrder, int> _orderRepository;
        private readonly IRepository<OrderDetail, int> _orderDetailRepository;
        private readonly IRepository<OrderMain, int> _orderMainRepository;
        private readonly ILogger<CheckEventHandler> _logger;

        public CheckEventHandler(IRepository<InjectionOrder, int> orderRepository
            , IRepository<OrderDetail, int> orderDetailRepository
            , IRepository<OrderMain, int> orderMainRepository
            , ILogger<CheckEventHandler> logger)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderMainRepository = orderMainRepository;
            _logger = logger;
        }

        public async Task HandleEventAsync(CheckInjectionEto eventData)
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
        protected virtual async Task<bool> ValidAsync(CheckInjectionEto eventData)
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

            if (order.Status != (int)EnumInjectionOrderStatus.WaitCheck
                && order.Status != (int)EnumInjectionOrderStatus.CheckedNoPass
                && order.Status != (int)EnumInjectionOrderStatus.CheckedPass)
            {
                _logger.LogError($"当前注塑订单{eventData.OrderNo}不符合订单审核状态");
                return default;
            }
            return true;
        }

        [UnitOfWork]
        protected virtual async Task ChangeOrderStatusAsync(CheckInjectionEto eventData)
        {
            var order = await _orderRepository.FindAsync(t => t.OrderNo == eventData.OrderNo);
            order.Check((int)eventData.Status, eventData.Remark);
            await _orderRepository.UpdateAsync(order);
        }

        [UnitOfWork]
        protected virtual async Task<int> ChangeOrderDetailStatusAsync(CheckInjectionEto eventData)
        {
            var orderDetail = await _orderDetailRepository.FindAsync(t => t.ProName == eventData.OrderNo);
            if (eventData.Status == EnumInjectionOrderStatus.CheckedPass)
            {
                orderDetail.SetStatus((int)EnumOrderDetailStatus.CheckedPass);
            }
            if (eventData.Status == EnumInjectionOrderStatus.CheckedNoPass)
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