using Jiepei.ERP.EventBus.Shared.Pays;
using Jiepei.ERP.Orders.SubOrders;
using Jiepei.ERP.SubOrders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus;
using Volo.Abp.Guids;
using Volo.Abp.Uow;

namespace Jiepei.ERP.Orders.Orders.EventHandlers
{
    public class PayOrderEventHandler : ILocalEventHandler<OrderPayChangedEto>, ITransientDependency
    {
        private readonly IGuidGenerator _IGuidGenerator;
        private readonly IOrderRepository _orderRepository;
        private readonly IRepository<OrderLog> _orderLogRepository;
        private readonly IRepository<SubOrder> _subOrderRepository;
        private readonly IRepository<SubOrderFlow> _subOrderFlowRepository;
        private readonly ILogger<PayOrderEventHandler> _logger;

        public PayOrderEventHandler(IGuidGenerator IGuidGenerator
            , IOrderRepository orderRepository
            , IRepository<OrderLog> orderLogRepository
            , IRepository<SubOrder> subOrderRepository
            , IRepository<SubOrderFlow> subOrderFlowRepository
            , ILogger<PayOrderEventHandler> logger)
        {
            _IGuidGenerator = IGuidGenerator;
            _orderRepository = orderRepository;
            _orderLogRepository = orderLogRepository;
            _subOrderRepository = subOrderRepository;
            _subOrderFlowRepository = subOrderFlowRepository;
            _logger = logger;
        }

        [UnitOfWork(true)]
        public async Task HandleEventAsync(OrderPayChangedEto eventData)
        {
            try
            {
                var order = await _orderRepository.GetAsync(x => x.OrderNo == eventData.OrderNo);
                if (order == null || order.Status < EnumOrderStatus.CheckedPass)
                    return;

                if (!order.IsPay)
                {
                    order.IsPay = true;
                    switch (order.OrderType)
                    {

                        case EnumOrderType.Cnc:
                            order.Status = EnumOrderStatus.SureOrder;
                            break;
                        case EnumOrderType.Print3D:
                            order.Status = EnumOrderStatus.SureOrder;
                            break;
                        case EnumOrderType.SheetMetal:
                            order.Status = EnumOrderStatus.SureOrder;
                            break;
                        default:
                            break;
                    }
                }
                // if (order.PendingMoney > 0)
                // {

                order.PendingMoney = order.PendingMoney - eventData.Amount;
                order.PaidMoney = order.PaidMoney + eventData.Amount;
                order.PayTime = DateTime.Now;
                order.PayMode = (byte)EnumPayMode.Cash;
               // await _orderRepository.UpdateAsync(order);

                var orderLog = new OrderLog(_IGuidGenerator.Create(), eventData.OrderNo, $"用户:{order.ChannelUserId} 支付了{eventData.Amount}");
                await _orderLogRepository.InsertAsync(orderLog);

                await SubOrderHandleEventAsync(order.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("支付回调事件总线的结果：" + ex.Message);
            }

        }

        private async Task SubOrderHandleEventAsync(Guid orderId)
        {
            try
            {
                var subOrders = await _subOrderRepository.GetListAsync(x => x.OrderId == orderId);
                var subOrderFlows = new List<SubOrderFlow>();
                foreach (var subOrder in subOrders ?? new List<SubOrder>())
                {
                    switch (subOrder.OrderType)
                    {

                        case EnumOrderType.Cnc:
                            if (subOrder.Status == EnumSubOrderStatus.OfferComplete)
                            {
                                subOrder.SetStatus(EnumSubOrderStatus.SureOrder);
                                await _subOrderRepository.UpdateAsync(subOrder);
                            }
                            break;
                        case EnumOrderType.Print3D:
                            if (subOrder.Status == EnumSubOrderStatus.OfferComplete)
                            {
                                subOrder.SetStatus(EnumSubOrderStatus.SureOrder);
                                await _subOrderRepository.UpdateAsync(subOrder);
                            }
                            break;
                        case EnumOrderType.SheetMetal:
                            if (subOrder.Status == EnumSubOrderStatus.OfferComplete)
                            {
                                subOrder.SetStatus(EnumSubOrderStatus.SureOrder);
                                await _subOrderRepository.UpdateAsync(subOrder);
                            }
                            break;
                        default:
                            break;
                    }



                    var subOrderFlow = new SubOrderFlow(_IGuidGenerator.Create(), subOrder.OrderNo, EnumSubOrderFlowType.Payment, "客户付款成功", "支付订单");
                    subOrderFlows.Add(subOrderFlow);
                }
                await _subOrderFlowRepository.InsertManyAsync(subOrderFlows);
            }
            catch (Exception ex)
            {
                _logger.LogError("支付回调事件总线的结果：" + ex.Message);
            }

        }
    }
}
