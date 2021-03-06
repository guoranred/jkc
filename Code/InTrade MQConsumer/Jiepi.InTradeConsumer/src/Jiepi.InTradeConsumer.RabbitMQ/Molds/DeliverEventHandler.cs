using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Jiepei.InTradeConsumer.MoldOrders;
using Jiepei.InTradeConsumer.Domain.OrderMains;
using Jiepei.InTradeConsumer.OrderDetails;
using Volo.Abp.Uow;
using Jiepei.ERP.Molds;
using Jiepei.InTradeConsumer.Domain.Shareds;
using Jiepei.ERP.EventBus.Shared.Molds;
using Microsoft.Extensions.Logging;

namespace Jiepi.InTradeConsumer.Service
{
    /// <summary>
    /// 订单发货
    /// </summary>
    public class DeliverEventHandler : IDistributedEventHandler<DeliverMoldEto>, ITransientDependency
    {
        private readonly IRepository<MoldOrder, int> _orderRepository;
        private readonly IRepository<OrderDetail, int> _orderDetailRepository;
        private readonly IRepository<OrderMain, int> _orderMainRepository;
        private readonly ILogger<DeliverEventHandler> _logger;

        public DeliverEventHandler(IRepository<MoldOrder, int> orderRepository
            , IRepository<OrderDetail, int> orderDetailRepository
            , IRepository<OrderMain, int> orderMainRepository
            , ILogger<DeliverEventHandler> logger)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderMainRepository = orderMainRepository;
            _logger = logger;
        }

        public async Task HandleEventAsync(DeliverMoldEto eventData)
        {
            if (!await ValidAsync(eventData))
            {
                return;
            }

            await ChangeOrderStatusAsync(eventData);
            var id = await ChangeOrderDetailStatusAsync(eventData);
            await ChangeOrderMainStatusAsync(eventData, id);
        }

        [UnitOfWork]
        protected virtual async Task<bool> ValidAsync(DeliverMoldEto eventData)
        {           
            if (eventData == null)
            {
                _logger.LogError("系统异常");
                return default;
            }

            var order = await _orderRepository.FindAsync(t => t.OrderNo == eventData.OrderNo);
            if (order == null)
            {
                _logger.LogError($"未查询到当前模具订单{eventData.OrderNo}信息");
                return default;
            }

            if (order.Status >= (int)EnumMoldOrderStatus.HaveSend)
            {
                _logger.LogError($"订单{eventData.OrderNo}不符合发货条件");
                return default;
            }
            return  true;
        }

        [UnitOfWork]
        protected virtual async Task ChangeOrderStatusAsync(DeliverMoldEto eventData)
        {
            var entity = await _orderRepository.FindAsync(t => t.OrderNo == eventData.OrderNo);
            entity.SetStaus((int)EnumMoldOrderStatus.HaveSend);
            await _orderRepository.UpdateAsync(entity);
        }

        [UnitOfWork]
        protected virtual async Task<int> ChangeOrderDetailStatusAsync(DeliverMoldEto eventData)
        {
            var entity = await _orderDetailRepository.FindAsync(t => t.ProName == eventData.OrderNo);
            entity.SetStatus((int)EnumOrderDetailStatus.HaveSend);
            entity.SetIsSendToCustomer(true);
            entity.SetSendExpName(eventData.CourierCompany);
            entity.SetSendExpNo(eventData.TrackingNo);
            entity.SetSendTime(eventData.SendTime);

            await _orderDetailRepository.UpdateAsync(entity);

            return entity?.MainId ?? 0;
        }

        [UnitOfWork]
        protected virtual async Task ChangeOrderMainStatusAsync(DeliverMoldEto eventData, int id)
        {
            var entity = await _orderMainRepository.FindAsync(t => t.Id == id);
            entity.SetStatus((int)EnumOrderMainStatus.HaveSend);
            entity.SetSendExpNo(eventData.TrackingNo);
            entity.SetShipDate(eventData.SendTime);
            await _orderMainRepository.UpdateAsync(entity);
        }
    }
}
