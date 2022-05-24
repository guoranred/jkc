using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Shared.Consumers.Orders;
using Volo.Abp;
using Jiepei.ERP.SubOrders;
using Volo.Abp.Domain.Repositories;
using Jiepei.ERP.Utilities;
using Jiepei.ERP.Commons;

namespace Jiepei.ERP.Orders.SubOrders
{
    public class InjectionOrderExtraAppService : OrdersAppService, IInjectionOrderExtraAppService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRepository<OrderCost> _orderCostRepository;
        private readonly IRepository<OrderDelivery> _orderDeliveryRepository;
        private readonly IRepository<OrderLog> _orderLogRepository;
        private readonly ISubOrderRepository _subOrderRepository;
        private readonly IRepository<InjectionOrderExtra> _injectionOrderExtras;
        private readonly IRepository<SubOrderFlow> _subOrderFlows;
        private readonly ISubOrderManager _subOrderManager;

        public InjectionOrderExtraAppService(IOrderRepository orderRepository
            , IRepository<OrderCost> orderCostRepository
            , IRepository<OrderDelivery> orderDeliveryRepository
            , IRepository<OrderLog> orderLogRepository
            , ISubOrderRepository subOrderRepository
            , IRepository<InjectionOrderExtra> injectionOrderExtras
            , IRepository<SubOrderFlow> subOrderFlows, ISubOrderManager subOrderManager)
        {
            _orderRepository = orderRepository;
            _orderCostRepository = orderCostRepository;
            _orderDeliveryRepository = orderDeliveryRepository;
            _orderLogRepository = orderLogRepository;
            _subOrderRepository = subOrderRepository;
            _injectionOrderExtras = injectionOrderExtras;
            _subOrderFlows = subOrderFlows;
            _subOrderManager = subOrderManager;
        }

        [AllowAnonymous]
        public async Task<bool> PostTaskExterAsync(MQ_Injection_OrderTaskDto input)
        {
            var order = await ValidCommonExterAsync(input);
            if (order != null)
                throw new BusinessException(message: $"系统已存在{input.ChannelOrderNo}相同的订单数据");

            var mainOrderNo = OrderHelper.CreateOrderNo();
            var injectionOrderNo = OrderHelper.GetOrderType(EnumOrderType.Injection) + mainOrderNo;
            //var customerId = await GetCustomerByOriginExterAsync(input.Origin);

            await CreateOrderExterAsync(mainOrderNo, input.Order);
            await CreateOrderCostExterAsync(mainOrderNo, input.OrderCost);
            await CreateOrderDeliverieExterAsync(mainOrderNo, input.OrderDelivery);
            await CreateOrderLogAsync(mainOrderNo, $"渠道{EnumChannel.InternalTrade.GetDescription()}客户创建了新的{input.OrderType.GetDescription()}订单");

            await CreateOrderInjectionExterAsync(mainOrderNo, injectionOrderNo, input);
            await CreateInjectionFlowAsync(injectionOrderNo, SubOrderFlowType.Create, "", $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户创建了订单");

            return true;
        }

        [AllowAnonymous]
        public async Task<bool> PutCancelExterAsync(MQ_Injection_OrderCancelDto input)
        {
            var order = await ValidCancelInjectionExterAsync(input);
            if (order == null)
                throw new BusinessException(message: $"系统不存在{input.ChannelOrderNo}的订单数据");

            var injectoinOrderNo = OrderHelper.GetOrderType(EnumOrderType.Injection) + order.OrderNo;
            await SetInjectionOrderStatusAsync(order.OrderNo, EnumSubOrderStatus.Cancel);
            await CreateOrderLogAsync(order.OrderNo, $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户取消了订单");
            await CreateInjectionFlowAsync(injectoinOrderNo, SubOrderFlowType.Cancel, "", $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户取消了订单");
            return true;
        }

        [AllowAnonymous]
        public async Task PutPaymentExterAsync(MQ_Injection_OrderPaymentDto input)
        {
            var order = await ValidCommonExterAsync(input);
            if (order == null)
                throw new BusinessException(message: $"系统不存在{input.ChannelOrderNo}的订单数据");

            if (order.IsPay == false)
            {
                var reult = await SetInjectionOrderStatusAsync(order.OrderNo, EnumSubOrderStatus.SureOrder);
                await UpdateOrderPaymentExterAsync(reult.Item2, input);
            }
            else
            {
                await UpdateOrderPaymentExterAsync(order, input);
            }

            await CreateOrderLogAsync(order.OrderNo, $"来自捷配{EnumChannel.InternalTrade.GetDescription()}的用户支付了{input.PaidMoney}");

            var injectionOrderNo = OrderHelper.GetOrderType(EnumOrderType.Injection) + order.OrderNo;
            await CreateInjectionFlowAsync(injectionOrderNo, SubOrderFlowType.Payment, "", $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户支付了{input.PaidMoney}");
        }

        [AllowAnonymous]
        public async Task<bool> PutReceiveExterAsync(MQ_Injection_OrderReceiveDto input)
        {
            var order = await ValidReceiveInjectionExterAsync(input);
            if (order == null)
                throw new BusinessException(message: $"系统不存在{input.ChannelOrderNo}的订单数据");

            await SetInjectionOrderReceiveAsync(order.OrderNo, EnumSubOrderStatus.Finish);
            await CreateOrderLogAsync(order.OrderNo, $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户收货订单");

            var injectionOrderNo = OrderHelper.GetOrderType(EnumOrderType.Injection) + order.OrderNo;
            await CreateInjectionFlowAsync(injectionOrderNo, SubOrderFlowType.Complete, "", $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户收货订单");
            return true;
        }

        private async Task CreateOrderExterAsync(string orderNo, MQ_OrderTask_OrderDto input)
        {
            var order = new Order(GuidGenerator.Create()
                , orderNo
                , input.Remark
                , input.OrderType
                , input.DeliveryDays
                , input.DeliveryDate);
            await _orderRepository.InsertAsync(order);
        }

        /// <summary>
        /// 创建订单金额数据
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task CreateOrderCostExterAsync(string orderNo, MQ_OrderTask_OrderCostDto input)
        {
            if (input == null)
                return;

            var cost = ObjectMapper.Map<MQ_OrderTask_OrderCostDto, OrderCost>(input);
            cost.OrderNo = orderNo;
            await _orderCostRepository.InsertAsync(cost);
        }

        /// <summary>
        /// 创建订单地址信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task CreateOrderDeliverieExterAsync(string orderNo, MQ_OrderTask_OrderDeliveryDto input)
        {
            if (input == null)
                return;

            var deliverie = ObjectMapper.Map<MQ_OrderTask_OrderDeliveryDto, OrderDelivery>(input);
            deliverie.SetOrderNo(orderNo);
            await _orderDeliveryRepository.InsertAsync(deliverie);
        }

        /// <summary>
        /// 创建订单操作日志
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="note">日志内容</param>
        /// <returns></returns>
        private async Task CreateOrderLogAsync(string orderNo, string note)
        {
            var userName = CurrentUser.UserName;
            note = userName.IsNullOrWhiteSpace() ? note : $"{userName}:{note}";
            var entity = new OrderLog(GuidGenerator.Create(), orderNo, note);
            await _orderLogRepository.InsertAsync(entity);
        }

        private async Task CreateOrderInjectionExterAsync(string mainOrderNo, string orderNo, MQ_Injection_OrderTaskDto input)
        {
            if (input == null)
                return;

            var subOrder = new SubOrder(GuidGenerator.Create()
                , mainOrderNo
                , orderNo
                , input.Order.Channel
                , input.ChannelOrderNo
                , input.ChannelUserId
                , input.Order.TotalMoney
                , input.Order.SellingMoney
                , input.Order.OrderType
                , null
                , input.Order.Remark
                );

            var injectionOrderExtra = new InjectionOrderExtra(GuidGenerator.Create()
                , subOrder.Id
                , input.InjectionOrder.ProName
                , input.InjectionOrder.Picture
                , input.InjectionOrder.FileName
                , input.InjectionOrder.FilePath
                , input.InjectionOrder.Size
                , input.InjectionOrder.Qty
                , input.InjectionOrder.Color
                , input.InjectionOrder.Material.Value
                , input.InjectionOrder.Surface.Value
                , input.InjectionOrder.PackMethod.Value);

            await _subOrderRepository.InsertAsync(subOrder);
            await _injectionOrderExtras.InsertAsync(injectionOrderExtra);
        }

        /// <summary>
        /// 创建模具订单流程
        /// </summary>
        /// <param name="childOrderNo"></param>
        /// <param name="type"></param>
        /// <param name="remark"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private async Task CreateInjectionFlowAsync(string childOrderNo, SubOrderFlowType type, string remark, string content)
        {
            var entity = new SubOrderFlow(GuidGenerator.Create(), childOrderNo, type, content, remark);
            await _subOrderFlows.InsertAsync(entity);
        }

        private async Task<string> GetOrderNoAsync(string exterOrderNo)
        {
            var orders = await _orderRepository.GetQueryableAsync();
            return orders.Where(t => t.ChannelOrderNo == exterOrderNo).Select(t => t.OrderNo).FirstOrDefault();
        }

        #region Valid
        private async Task<Order> ValidCommonExterAsync(MQ_BaseOrderDto input)
        {
            if (input == null)
                throw new BusinessException(message: $"系统异常：{nameof(MQ_BaseOrderDto)}为空");

            if (input.OrderType != EnumOrderType.Injection)
                throw new BusinessException(message: $"订单不符合注塑类型");

            var order = await _orderRepository.FindAsync(x => x.ChannelOrderNo == input.ChannelOrderNo);
            return order;
        }
        private async Task<Order> ValidCancelInjectionExterAsync(MQ_Injection_OrderCancelDto input)
        {
            var order = await ValidCommonExterAsync(input);
            if (order == null)
                throw new BusinessException(message: $"订单{input.ChannelOrderNo}数据不存在");
            if (order.Status >= EnumOrderStatus.SureOrder)
                throw new BusinessException(message: $"订单{input.ChannelOrderNo}已支付,取消无效");

            return order;
        }

        private async Task<Order> ValidReceiveInjectionExterAsync(MQ_Injection_OrderReceiveDto input)
        {
            var order = await ValidCommonExterAsync(input);
            if (order == null)
                throw new BusinessException(message: $"订单{input.ChannelOrderNo}数据不存在");
            if (order.Status < EnumOrderStatus.HaveSend)
                throw new BusinessException(message: $"订单{input.ChannelOrderNo}未发货,收货无效");
            if (order.Status == EnumOrderStatus.Finish)
                throw new BusinessException(message: $"订单{input.ChannelOrderNo}已完成，收货无效");
            return order;
        }
        #endregion

        /// <summary>
        /// 修改注塑订单状态
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        private async Task<(bool, Order)> SetInjectionOrderStatusAsync(string orderNo, EnumSubOrderStatus status)
        {
            var entity = await _subOrderRepository.GetAsync(t => t.MainOrderNo == orderNo);

            if (entity.Status == status)
            {
                throw new UserFriendlyException("请勿重复操作！");
            }

            entity.SetStatus(status);

            var injectionOrder = await _subOrderRepository.UpdateAsync(entity);

            var orderEntity = await SetOrderStatusAsync(orderNo, status);

            return (!injectionOrder.OrderNo.IsNullOrEmpty(), orderEntity);
        }
        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        private async Task<Order> SetOrderStatusAsync(string orderNo, EnumSubOrderStatus status)
        {
            var entity = await _orderRepository.GetAsync(t => t.OrderNo == orderNo);

            entity.Status = TransformInjectionOrderStatusToOrderStatus(status);

            var result = await _orderRepository.UpdateAsync(entity);

            return result;
        }
        private async Task<(bool, Order)> SetInjectionOrderReceiveAsync(string orderNo, EnumSubOrderStatus status)
        {
            var entity = await _subOrderRepository.GetAsync(t => t.MainOrderNo == orderNo);
            if (entity.Status == status)
            {
                throw new UserFriendlyException("请勿重复操作！");
            }
            entity.SetStatus(status);
            var injectionOrder = await _subOrderRepository.UpdateAsync(entity);
            var orderEntity = await SetOrderStatusAsync(orderNo, status);
            return (!injectionOrder.OrderNo.IsNullOrEmpty(), orderEntity);
        }

        private async Task UpdateOrderPaymentExterAsync(Order order, MQ_Injection_OrderPaymentDto input)
        {
            order.PaidMoney = input.PaidMoney.Value;
            order.PendingMoney = input.PendingMoney.Value;
            order.PayTime = input.PayTime;
            order.IsPay = true;
            order.PayMode = (byte)input.PayModel;
            await _orderRepository.UpdateAsync(order);
        }
        /// <summary>
        /// 注塑订单状态转换成订单状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private EnumOrderStatus TransformInjectionOrderStatusToOrderStatus(EnumSubOrderStatus status)
        {
            return status switch
            {
                EnumSubOrderStatus.WaitCheck => EnumOrderStatus.WaitCheck,
                EnumSubOrderStatus.CheckedNoPass => EnumOrderStatus.CheckedNoPass,
                EnumSubOrderStatus.CheckedPass => EnumOrderStatus.WaitCheck,
                EnumSubOrderStatus.OfferComplete => EnumOrderStatus.CheckedPass,
                EnumSubOrderStatus.Cancel => EnumOrderStatus.Cancel,
                EnumSubOrderStatus.SureOrder => EnumOrderStatus.SureOrder,
                EnumSubOrderStatus.Purchasing => EnumOrderStatus.Purchasing,
                EnumSubOrderStatus.WaitSend => EnumOrderStatus.WaitSend,
                EnumSubOrderStatus.HaveSend => EnumOrderStatus.HaveSend,
                EnumSubOrderStatus.Finish => EnumOrderStatus.Finish,
                _ => throw new ArgumentException(),
            };
        }
    }
}
