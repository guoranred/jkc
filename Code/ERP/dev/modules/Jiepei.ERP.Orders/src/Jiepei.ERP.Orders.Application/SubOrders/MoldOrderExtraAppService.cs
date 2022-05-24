using Jiepei.ERP.Commons;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Shared.Consumers.Orders;
using Jiepei.ERP.SubOrders;
using Jiepei.ERP.Utilities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.SubOrders
{
    /// <summary>
    /// 扩展类：ERP 消费者调用
    /// 查询：禁用租户
    /// </summary>
    public class MoldOrderExtraAppService : OrdersAppService, IMoldOrderExtraAppService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRepository<OrderCost> _orderCostRepository;
        private readonly IRepository<OrderDelivery> _orderDeliveryRepository;
        private readonly IRepository<OrderLog> _orderLogRepository;
        private readonly ISubOrderRepository _subOrderRepository;
        private readonly IRepository<MoldOrderExtra> _moldOrderExtras;
        private readonly IRepository<InjectionOrderExtra> _injectionOrderExtras;
        private readonly IRepository<CncOrderExtra> _cncOrderExtras;
        private readonly IRepository<SubOrderFlow> _subOrderFlows;

        private readonly ISubOrderManager _subOrderManager;

        public MoldOrderExtraAppService(IOrderRepository orderRepository
            , IRepository<OrderCost> orderCostRepository
            , IRepository<OrderDelivery> orderDeliveryRepository
            , IRepository<OrderLog> orderLogRepository
            , ISubOrderRepository subOrderRepository
            , IRepository<MoldOrderExtra> moldOrderExtras
            , IRepository<InjectionOrderExtra> injectionOrderExtras
            , IRepository<CncOrderExtra> cncOrderExtras
            , IRepository<SubOrderFlow> subOrderFlows
            , ISubOrderManager subOrderManager)
        {
            _orderRepository = orderRepository;
            _orderCostRepository = orderCostRepository;
            _orderDeliveryRepository = orderDeliveryRepository;
            _orderLogRepository = orderLogRepository;
            _subOrderRepository = subOrderRepository;
            _moldOrderExtras = moldOrderExtras;
            _injectionOrderExtras = injectionOrderExtras;
            _cncOrderExtras = cncOrderExtras;
            _subOrderFlows = subOrderFlows;
            _subOrderManager = subOrderManager;
        }

        [AllowAnonymous]
        public async Task<bool> PostTaskExterAsync(MQ_Mold_OrderTaskDto input)
        {
            var order = await ValidCommonExterAsync(input);
            if (order != null)
                throw new BusinessException(message: $"系统已存在{input.ChannelOrderNo}相同的订单数据");

            var mainOrderNo = OrderHelper.CreateOrderNo();
            var moldOrderNo = OrderHelper.GetOrderType(EnumOrderType.Mold) + mainOrderNo;
            //var customerId = await GetCustomerByOriginExterAsync(input.Origin);

            await CreateOrderExterAsync(mainOrderNo, input.Order);
            await CreateOrderCostExterAsync(mainOrderNo, input.OrderCost);
            await CreateOrderDeliverieExterAsync(mainOrderNo, input.OrderDelivery);
            await CreateOrderLogAsync(mainOrderNo, $"渠道{EnumChannel.InternalTrade.GetDescription()}客户创建了新的{input.OrderType.GetDescription()}订单");

            await CreateOrderMoldExterAsync(mainOrderNo, moldOrderNo, input);
            await CreateMoldFlowAsync(moldOrderNo, SubOrderFlowType.Create, "", $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户创建了订单");

            return true;
        }

        [AllowAnonymous]
        public async Task<bool> PutCancelExterAsync(MQ_Mold_OrderCancelDto input)
        {
            var order = await ValidCancelMoldExterAsync(input);
            if (order == null)
                throw new BusinessException(message: $"系统不存在{input.ChannelOrderNo}的订单数据");

            var moldOrderNo = OrderHelper.GetOrderType(EnumOrderType.Mold) + order.OrderNo;
            await SetMoldOrderStatusAsync(order.OrderNo, EnumSubOrderStatus.Cancel);
            await CreateOrderLogAsync(order.OrderNo, $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户取消了订单");
            await CreateMoldFlowAsync(moldOrderNo, SubOrderFlowType.Cancel, "", $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户取消了订单");
            return true;
        }

        [AllowAnonymous]
        public async Task PutPaymentExterAsync(MQ_Mold_OrderPaymentDto input)
        {
            var order = await ValidCommonExterAsync(input);
            if (order == null)
                throw new BusinessException(message: $"系统不存在{input.ChannelOrderNo}的订单数据");

            if (order.IsPay == false)
            {
                var reult = await SetMoldOrderStatusAsync(order.OrderNo, EnumSubOrderStatus.SureOrder);
                await UpdateOrderPaymentExterAsync(reult.Item2, input);
                var moldOrderNo = OrderHelper.GetOrderType(EnumOrderType.Mold) + order.OrderNo;
                await CreateMoldFlowAsync(moldOrderNo, SubOrderFlowType.Payment, "", $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户支付了{input.PaidMoney}");

            }
            else
            {
                await UpdateOrderPaymentExterAsync(order, input);
            }

            await CreateOrderLogAsync(order.OrderNo, $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户支付了{input.PaidMoney}");
        }

        [AllowAnonymous]
        public async Task<bool> PutReceiveExterAsync(MQ_Mold_OrderReceiveDto input)
        {
            var order = await ValidReceiveMoldExterAsync(input);
            if (order == null)
                throw new BusinessException(message: $"系统不存在{input.ChannelOrderNo}的订单数据");

            await SetMoldOrderReceiveAsync(order.OrderNo, EnumSubOrderStatus.Finish);
            await CreateOrderLogAsync(order.OrderNo, $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户收货订单");

            var moldOrderNo = OrderHelper.GetOrderType(EnumOrderType.Mold) + order.OrderNo;
            await CreateMoldFlowAsync(moldOrderNo, SubOrderFlowType.Complete, "", $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户收货订单");
            return true;
        }
        #region private


        private async Task<Orders.Order> ValidCommonExterAsync(MQ_BaseOrderDto input)
        {
            if (input == null)
                throw new BusinessException(message: $"系统异常：{nameof(MQ_BaseOrderDto)}为空");

            if (input.OrderType != EnumOrderType.Mold)
                throw new BusinessException(message: $"订单不符合模具类型");


            var order = await _orderRepository.FindAsync(x => x.ChannelOrderNo == input.ChannelOrderNo);
            return order;
        }

        private async Task<Order> ValidCancelMoldExterAsync(MQ_Mold_OrderCancelDto input)
        {
            var order = await ValidCommonExterAsync(input);
            if (order == null)
                throw new BusinessException(message: $"订单{input.ChannelOrderNo}数据不存在");
            if (order.Status >= EnumOrderStatus.SureOrder)
                throw new BusinessException(message: $"订单{input.ChannelOrderNo}已支付,取消无效");

            return order;
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

        private async Task<Order> ValidReceiveMoldExterAsync(MQ_Mold_OrderReceiveDto input)
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

        private async Task CreateOrderMoldExterAsync(string mainOrderNo, string orderNo, MQ_Mold_OrderTaskDto input)
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

            var moldOrderExtra = new MoldOrderExtra(GuidGenerator.Create()
                , subOrder.Id
                , input.MoldOrder.ProName
                , input.MoldOrder.Picture
                , input.MoldOrder.FileName
                , input.MoldOrder.FilePath
                , $"{input.MoldOrder.Long}*{input.MoldOrder.Width}*{input.MoldOrder.Height}"
                , input.MoldOrder.Qty
                , input.MoldOrder.Color
                , input.MoldOrder.Material
                , input.MoldOrder.Surface
                , input.Order.ApplicationArea
                , input.Order.Usage);

            await _subOrderRepository.InsertAsync(subOrder);
            await _moldOrderExtras.InsertAsync(moldOrderExtra);
        }

        /// <summary>
        /// 创建模具订单流程
        /// </summary>
        /// <param name="childOrderNo"></param>
        /// <param name="type"></param>
        /// <param name="remark"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private async Task CreateMoldFlowAsync(string childOrderNo, SubOrderFlowType type, string remark, string content)
        {
            var entity = new SubOrderFlow(GuidGenerator.Create(), childOrderNo, type, content, remark);
            await _subOrderFlows.InsertAsync(entity);
        }

        /// <summary>
        /// 修改模具订单状态
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        private async Task<(bool, Order)> SetMoldOrderStatusAsync(string orderNo, EnumSubOrderStatus status)
        {

            var entity = await _subOrderRepository.GetAsync(t => t.MainOrderNo == orderNo);

            if (entity.Status == status)
            {
                throw new UserFriendlyException("请勿重复操作！");
            }

            entity.SetStatus(status);

            var moldOrder = await _subOrderRepository.UpdateAsync(entity);

            var orderEntity = await SetOrderStatusAsync(orderNo, status);

            return (!moldOrder.OrderNo.IsNullOrEmpty(), orderEntity);

        }

        private async Task UpdateOrderPaymentExterAsync(Order order, MQ_Mold_OrderPaymentDto input)
        {
            order.PaidMoney = input.PaidMoney.Value;
            order.PendingMoney = input.PendingMoney.Value;
            order.PayTime = input.PayTime;
            order.IsPay = true;
            order.PayMode = (byte)input.PayModel;
            await _orderRepository.UpdateAsync(order);
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

            entity.Status = TransformMoldOrderStatusToOrderStatus(status);

            var result = await _orderRepository.UpdateAsync(entity);

            return result;
        }

        /// <summary>
        /// 模具订单状态转换成订单状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private EnumOrderStatus TransformMoldOrderStatusToOrderStatus(EnumSubOrderStatus status)
        {
            switch (status)
            {
                case EnumSubOrderStatus.WaitCheck:
                    return EnumOrderStatus.WaitCheck;
                case EnumSubOrderStatus.CheckedNoPass:
                    return EnumOrderStatus.CheckedNoPass;
                case EnumSubOrderStatus.CheckedPass:
                    return EnumOrderStatus.WaitCheck;
                case EnumSubOrderStatus.OfferComplete:
                    return EnumOrderStatus.CheckedPass;
                case EnumSubOrderStatus.Cancel:
                    return EnumOrderStatus.Cancel;
                case EnumSubOrderStatus.SureOrder:
                    return EnumOrderStatus.SureOrder;
                case EnumSubOrderStatus.Purchasing:
                    return EnumOrderStatus.Purchasing;
                case EnumSubOrderStatus.WaitSend:
                    return EnumOrderStatus.WaitSend;
                case EnumSubOrderStatus.HaveSend:
                    return EnumOrderStatus.HaveSend;
                case EnumSubOrderStatus.Finish:
                    return EnumOrderStatus.Finish;
                default:
                    throw new ArgumentException();
            }
        }
        private async Task<(bool, Order)> SetMoldOrderReceiveAsync(string orderNo, EnumSubOrderStatus status)
        {
            var entity = await _subOrderRepository.GetAsync(t => t.MainOrderNo == orderNo);
            if (entity.Status == status)
            {
                throw new UserFriendlyException("请勿重复操作！");
            }
            entity.SetStatus(status);
            var moldOrder = await _subOrderRepository.UpdateAsync(entity);
            var orderEntity = await SetOrderStatusAsync(orderNo, status);
            return (!moldOrder.OrderNo.IsNullOrEmpty(), orderEntity);
        }
        #endregion
    }
}
