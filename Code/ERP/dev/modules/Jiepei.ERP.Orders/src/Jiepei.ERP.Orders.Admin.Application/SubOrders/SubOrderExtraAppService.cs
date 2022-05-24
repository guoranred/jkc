using Jiepei.ERP.Commons;
using Jiepei.ERP.Orders.Admin;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Shared.Consumers.Orders;
using Jiepei.ERP.Shared.Consumers.Orders.SubOrders;
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
    /// 
    /// </summary>
    public class SubOrderExtraAppService : OrdersAdminAppServiceBase, ISubOrderExtraAppService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ISubOrderRepository _subOrderRepository;
        private readonly ISubOrderManager _subOrderManager;
        private readonly IRepository<OrderCost, Guid> _orderCostRepository;
        private readonly IRepository<OrderDelivery, Guid> _orderDeliveryRepository;
        private readonly IRepository<OrderLog, Guid> _orderLogRepository;
        private readonly ISubOrderMoldItemRepository _subOrderMoldItemRepository;
        private readonly ISubOrderInjectionItemRepository _subOrderInjectionItemRepository;
        private readonly ISubOrderCncItemRepository _subOrderCncItemRepository;
        private readonly IRepository<SubOrderFlow, Guid> _subOrderFlowRepository;

        public SubOrderExtraAppService(IOrderRepository orderRepository
            , ISubOrderRepository subOrderRepository
            , ISubOrderManager subOrderManager
            , IRepository<OrderCost, Guid> orderCostRepository
            , IRepository<OrderDelivery, Guid> orderDeliveryRepository
            , IRepository<OrderLog, Guid> orderLogRepository
            , ISubOrderMoldItemRepository subOrderMoldItemRepository
            , ISubOrderInjectionItemRepository subOrderInjectionItemRepository
            , ISubOrderCncItemRepository subOrderCncItemRepository
            , IRepository<SubOrderFlow, Guid> subOrderFlowRepository

            )
        {
            _orderRepository = orderRepository;
            _subOrderRepository = subOrderRepository;
            _subOrderManager = subOrderManager;
            _orderCostRepository = orderCostRepository;
            _orderDeliveryRepository = orderDeliveryRepository;
            _orderLogRepository = orderLogRepository;
            _subOrderMoldItemRepository = subOrderMoldItemRepository;
            _subOrderInjectionItemRepository = subOrderInjectionItemRepository;
            _subOrderCncItemRepository = subOrderCncItemRepository;
            _subOrderFlowRepository = subOrderFlowRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<bool> CancelExterAsync(MQSubOrderCancelDto input)
        {
            await ValidCancelExterAsync(input);

            var order = await _orderRepository.FindAsync(x => x.ChannelOrderNo == input.ChannelOrderNo);

            if (order == null)
                throw new BusinessException(message: $"系统不存在{input.ChannelOrderNo}的订单数据");

            var subOrderNo = order.OrderNo + "-" + OrderHelper.GetOrderType(input.OrderType) ;
            await SetSubOrderStatusAsync(order.Id, EnumSubOrderStatus.Cancel);
            await CreateOrderLogAsync(order.OrderNo, $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户取消了订单");
            await CreateSubOrderFlowAsync(subOrderNo, EnumSubOrderFlowType.Cancel, "", $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户取消了订单");
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task PaymentExterAsync(MQSubOrderPaymentDto input)
        {
            await ValidCommonExterAsync(input);

            var order = await _orderRepository.FindAsync(x => x.ChannelOrderNo == input.ChannelOrderNo);

            if (order == null)
                throw new BusinessException(message: $"系统不存在{input.ChannelOrderNo}的订单数据");

            if (order.IsPay == false)
            {
                var reult = await SetSubOrderStatusAsync(order.Id, EnumSubOrderStatus.SureOrder);
                await UpdateOrderPaymentExterAsync(reult.Item2, input);
            }
            else
            {
                await UpdateOrderPaymentExterAsync(order, input);
            }

            await CreateOrderLogAsync(order.OrderNo, $"来自捷配{EnumChannel.InternalTrade.GetDescription()}的用户支付了{input.PaidMoney}");

            var subOrderNo = order.OrderNo+ "-" + OrderHelper.GetOrderType(input.OrderType) ;
            await CreateSubOrderFlowAsync(subOrderNo, EnumSubOrderFlowType.Payment, "", $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户支付了{input.PaidMoney}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<bool> ReceiveExterAsync(MQSubOrderReceiveDto input)
        {
            //var order = await ValidReceiveInjectionExterAsync(input);

            var order = await _orderRepository.FindAsync(x => x.ChannelOrderNo == input.ChannelOrderNo);

            if (order == null)
                throw new BusinessException(message: $"系统不存在{input.ChannelOrderNo}的订单数据");

            await SetSubOrderReceiveAsync(order.Id, EnumSubOrderStatus.Finish);
            await CreateOrderLogAsync(order.OrderNo, $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户收货订单");

            var subOrderNo = order.OrderNo+ "-" + OrderHelper.GetOrderType(input.OrderType) ;
            await CreateSubOrderFlowAsync(subOrderNo, EnumSubOrderFlowType.Complete, "", $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户收货订单");
            return true;
        }

        #region 待优化

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<bool> InjectionTaskExterAsync(MQ_Injection_OrderTaskDto input)
        {
            await ValidCommonExterAsync(input);
            var order = await _orderRepository.FindAsync(x => x.ChannelOrderNo == input.ChannelOrderNo);
            if (order != null)
                throw new BusinessException(message: $"系统已存在{input.ChannelOrderNo}相同的订单数据");

            var mainOrderNo = OrderHelper.CreateOrderNo();
            var injectionOrderNo = mainOrderNo+ "-" + OrderHelper.GetOrderType(EnumOrderType.Injection) ;
            //var customerId = await GetCustomerByOriginExterAsync(input.Origin);

            order = await CreateOrderExterAsync(mainOrderNo, input, input.Order);
            await CreateOrderCostExterAsync(mainOrderNo, input.OrderCost);
            await CreateOrderDeliverieExterAsync(mainOrderNo, input.OrderDelivery);
            await CreateOrderLogAsync(mainOrderNo, $"渠道{EnumChannel.InternalTrade.GetDescription()}客户创建了新的{input.OrderType.GetDescription()}订单");

            await CreateOrderInjectionExterAsync(mainOrderNo, injectionOrderNo, order.Id, input);
            await CreateSubOrderFlowAsync(injectionOrderNo, EnumSubOrderFlowType.Create, "", $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户创建了订单");

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<bool> MoldTaskExterAsync(MQ_Mold_OrderTaskDto input)
        {
            await ValidCommonExterAsync(input);
            var order = await _orderRepository.FindAsync(x => x.ChannelOrderNo == input.ChannelOrderNo);

            if (order != null)
                throw new BusinessException(message: $"系统已存在{input.ChannelOrderNo}相同的订单数据");

            var mainOrderNo = OrderHelper.CreateOrderNo();
            var moldOrderNo = mainOrderNo+ "-" + OrderHelper.GetOrderType(EnumOrderType.Mold);
            //var customerId = await GetCustomerByOriginExterAsync(input.Origin);

            order = await CreateOrderExterAsync(mainOrderNo, input, input.Order);
            await CreateOrderCostExterAsync(mainOrderNo, input.OrderCost);
            await CreateOrderDeliverieExterAsync(mainOrderNo, input.OrderDelivery);
            await CreateOrderLogAsync(mainOrderNo, $"渠道{EnumChannel.InternalTrade.GetDescription()}客户创建了新的{input.OrderType.GetDescription()}订单");

            await CreateOrderMoldExterAsync(mainOrderNo, moldOrderNo, order.Id, input);
            await CreateSubOrderFlowAsync(moldOrderNo, EnumSubOrderFlowType.Create, "", $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户创建了订单");

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<bool> CncTaskExterAsync(MQ_Cnc_OrderTaskDto input)
        {
            await ValidCommonExterAsync(input);
            var order = await _orderRepository.FindAsync(x => x.ChannelOrderNo == input.ChannelOrderNo);

            if (order != null)
                throw new BusinessException(message: $"系统已存在{input.ChannelOrderNo}相同的订单数据");

            var mainOrderNo = OrderHelper.CreateOrderNo();
            var cncOrderNo = mainOrderNo+ "-" + OrderHelper.GetOrderType(EnumOrderType.Cnc);

            order = await CreateOrderExterAsync(mainOrderNo, input, input.Order);
            await CreateOrderCostExterAsync(mainOrderNo, input.OrderCost);
            await CreateOrderDeliverieExterAsync(mainOrderNo, input.OrderDelivery);
            await CreateOrderLogAsync(mainOrderNo, $"渠道{EnumChannel.InternalTrade.GetDescription()}客户创建了新的{input.OrderType.GetDescription()}订单");

            await CreateOrderCncExterAsync(mainOrderNo, cncOrderNo, order.Id, input);
            await CreateSubOrderFlowAsync(cncOrderNo, EnumSubOrderFlowType.Create, "", $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户创建了订单");

            return true;
        }

        private async Task CreateOrderInjectionExterAsync(string mainOrderNo, string orderNo, Guid orderId, MQ_Injection_OrderTaskDto input)
        {
            if (input == null)
                return;

            var subOrder = new SubOrder(
                 id: GuidGenerator.Create()
                , orderId: orderId
                , orderNo: orderNo
                , channel: input.Order.Channel
                , channelOrderNo: input.ChannelOrderNo
                , channelUserId: input.ChannelUserId
                , cost: 0m// input.Order.TotalMoney
                , sellingPrice: 0m//input.Order.SellingMoney
                , orderType: input.Order.OrderType
                , organizationUnitId: null
                , status: EnumSubOrderStatus.WaitCheck
                , remark: input.Order.Remark
                );

            var injectionOrderExtra = new SubOrderInjectionItem(GuidGenerator.Create()
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
            await _subOrderInjectionItemRepository.InsertAsync(injectionOrderExtra);
        }

        private async Task CreateOrderMoldExterAsync(string mainOrderNo, string orderNo, Guid orderId, MQ_Mold_OrderTaskDto input)
        {
            if (input == null)
                return;

            var subOrder = new SubOrder(GuidGenerator.Create()
                    , orderId: orderId
                    , orderNo: orderNo
                    , channel: input.Order.Channel
                    , channelOrderNo: input.ChannelOrderNo
                    , channelUserId: input.ChannelUserId
                    , cost: 0m// input.Order.TotalMoney
                    , sellingPrice: 0m//input.Order.SellingMoneyed
                    , organizationUnitId: null
                    , orderType: EnumOrderType.Mold
                    , status: EnumSubOrderStatus.WaitCheck
                    , remark: input.Order.Remark
                );


            var orderMoldItem = new SubOrderMoldItem(GuidGenerator.Create()
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
            await _subOrderMoldItemRepository.InsertAsync(orderMoldItem);
        }

        private async Task CreateOrderCncExterAsync(string mainOrderNo, string orderNo, Guid orderId, MQ_Cnc_OrderTaskDto input)
        {
            if (input == null)
                return;

            var subOrder = new SubOrder(GuidGenerator.Create()
                    , orderId: orderId
                    , orderNo: orderNo
                    , channel: input.Order.Channel
                    , channelOrderNo: input.ChannelOrderNo
                    , channelUserId: input.ChannelUserId
                    , cost: 0m// input.Order.TotalMoney
                    , sellingPrice: 0m//input.Order.SellingMoneyed
                    , organizationUnitId: null
                    , orderType: EnumOrderType.Cnc
                    , status: EnumSubOrderStatus.WaitCheck
                    , remark: input.Order.Remark
                );


            var list = new List<SubOrderCncItem>();
            foreach (var item in input.CncBoms)
            {
                var orderCncItem = new SubOrderCncItem(GuidGenerator.Create()
                , subOrder.Id
                , item.ProName
                , item.Picture
                , item.FileName
                , item.FilePath
                , item.Size
                , item.Qty
                , item.Material.Value
                , item.Surface.Value
                , input.Order.ApplicationArea
                );
                list.Add(orderCncItem);
            }

            await _subOrderRepository.InsertAsync(subOrder);
            await _subOrderCncItemRepository.InsertManyAsync(list);
        }

        private async Task<Order> CreateOrderExterAsync(string orderNo, MQ_BaseOrderDto mqbase, MQ_OrderTask_OrderDto mqOrder)
        {
            var order = new Order(GuidGenerator.Create()
                , orderNo
                , EnumOrderStatus.WaitCheck
                , mqbase.Channel
                , mqbase.ChannelOrderNo
                , mqbase.ChannelUserId
                , mqOrder.Remark
                , mqOrder.OrderType
                //, 0
                //, 0
                //, 0
                , mqOrder.DeliveryDays
                , mqOrder.DeliveryDate);
            return await _orderRepository.InsertAsync(order);
        }


        #endregion

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

        private async Task ValidCommonExterAsync(MQ_BaseOrderDto input)
        {
            if (input == null)
                throw new BusinessException(message: $"系统异常：{nameof(MQ_BaseOrderDto)}为空");

            //if (input.OrderType != EnumOrderType.Injection)
            //    throw new BusinessException(message: $"订单不符合注塑类型");
            await Task.CompletedTask;
        }
        private async Task ValidCancelExterAsync(MQSubOrderCancelDto input)
        {
            await ValidCommonExterAsync(input);
            //if (order == null)
            //    throw new BusinessException(message: $"订单{input.ChannelOrderNo}数据不存在");
            //if (order.Status >= EnumOrderStatus.SureOrder)
            //    throw new BusinessException(message: $"订单{input.ChannelOrderNo}已支付,取消无效");
        }
        private async Task ValidReceiveInjectionExterAsync(MQ_Injection_OrderReceiveDto input)
        {
            await ValidCommonExterAsync(input);
            //if (order == null)
            //    throw new BusinessException(message: $"订单{input.ChannelOrderNo}数据不存在");
            //if (order.Status < EnumOrderStatus.HaveSend)
            //    throw new BusinessException(message: $"订单{input.ChannelOrderNo}未发货,收货无效");
            //if (order.Status == EnumOrderStatus.Finish)
            //    throw new BusinessException(message: $"订单{input.ChannelOrderNo}已完成，收货无效");
            //return order;
        }

        /// <summary>
        /// 修改子订单状态
        /// </summary>
        /// <param name="OrderId">订单id</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        private async Task<(bool, Order)> SetSubOrderStatusAsync(Guid OrderId, EnumSubOrderStatus status)
        {
            var entity = await _subOrderRepository.GetAsync(t => t.Id == OrderId);

            if (entity.Status == status)
            {
                throw new UserFriendlyException("请勿重复操作！");
            }

            entity.SetStatus(status);

            var injectionOrder = await _subOrderRepository.UpdateAsync(entity);

            var orderEntity = await SetOrderStatusAsync(OrderId, status);

            return (!injectionOrder.OrderNo.IsNullOrEmpty(), orderEntity);
        }

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="Id">订单号</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        private async Task<Order> SetOrderStatusAsync(Guid Id, EnumSubOrderStatus status)
        {
            var entity = await _orderRepository.GetAsync(t => t.Id == Id);

            entity.Status = TransformSubOrderStatusToOrderStatus(status);

            var result = await _orderRepository.UpdateAsync(entity);

            return result;
        }

        /// <summary>
        /// 子订单状态转换成订单状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private EnumOrderStatus TransformSubOrderStatusToOrderStatus(EnumSubOrderStatus status)
        {
            //return status switch
            //{
            //    EnumSubOrderStatus.WaitCheck => EnumOrderStatus.WaitCheck,
            //    EnumSubOrderStatus.CheckedNoPass => EnumOrderStatus.CheckedNoPass,
            //    EnumSubOrderStatus.CheckedPass => EnumOrderStatus.WaitCheck,
            //    EnumSubOrderStatus.OfferComplete => EnumOrderStatus.CheckedPass,
            //    EnumSubOrderStatus.Cancel => EnumOrderStatus.Cancel,
            //    EnumSubOrderStatus.SureOrder => EnumOrderStatus.SureOrder,
            //    EnumSubOrderStatus.Purchasing => EnumOrderStatus.Purchasing,
            //    EnumSubOrderStatus.WaitSend => EnumOrderStatus.WaitSend,
            //    EnumSubOrderStatus.HaveSend => EnumOrderStatus.HaveSend,
            //    EnumSubOrderStatus.Finish => EnumOrderStatus.Finish,
            //    _ => throw new ArgumentException(),
            //};

            switch (status)
            {
                case EnumSubOrderStatus.WaitCheck:
                    return EnumOrderStatus.WaitCheck;
                case EnumSubOrderStatus.CheckedNoPass:
                    return EnumOrderStatus.CheckedNoPass;
                case EnumSubOrderStatus.CheckedPass:
                    return EnumOrderStatus.CheckedPass;
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

        private async Task UpdateOrderPaymentExterAsync(Order order, MQSubOrderPaymentDto input)
        {
            order.PaidMoney = input.PaidMoney;
            order.PendingMoney = input.PendingMoney;
            order.PayTime = input.PayTime;
            order.IsPay = true;
            order.PayMode = (byte)input.PayModel;
            await _orderRepository.UpdateAsync(order);
        }

        private async Task<(bool, Order)> SetSubOrderReceiveAsync(Guid id, EnumSubOrderStatus status)
        {
            var entity = await _subOrderRepository.GetAsync(t => t.OrderId == id);
            if (entity.Status == status)
            {
                throw new UserFriendlyException("请勿重复操作！");
            }
            entity.SetStatus(status);
            var injectionOrder = await _subOrderRepository.UpdateAsync(entity);
            var orderEntity = await SetOrderStatusAsync(id, status);
            return (!injectionOrder.OrderNo.IsNullOrEmpty(), orderEntity);
        }

        #region 日志
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

        /// <summary>
        /// 创建子订单流程
        /// </summary>
        /// <param name="childOrderNo"></param>
        /// <param name="type"></param>
        /// <param name="remark"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private async Task CreateSubOrderFlowAsync(string childOrderNo, EnumSubOrderFlowType type, string remark, string content)
        {
            var entity = new SubOrderFlow(GuidGenerator.Create(), childOrderNo, type, content, remark);
            await _subOrderFlowRepository.InsertAsync(entity);
        }
        #endregion
    }
}
