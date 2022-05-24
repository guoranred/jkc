using Jiepei.ERP.Commons;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Shared.Consumers.Orders;
using Jiepei.ERP.SubOrders;
using Jiepei.ERP.Utilities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.SubOrders
{
    public class CncOrderExtraAppService : OrdersAppService, ICncOrderExtraAppService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRepository<OrderCost> _orderCostRepository;
        private readonly IRepository<OrderDelivery> _orderDeliveryRepository;
        private readonly IRepository<OrderLog> _orderLogRepository;
        private readonly ISubOrderRepository _subOrderRepository;
        private readonly IRepository<CncOrderExtra> _cncOrderExtras;
        private readonly IRepository<SubOrderFlow> _subOrderFlows;

        private readonly ISubOrderManager _subOrderManager;

        public CncOrderExtraAppService(IOrderRepository orderRepository
            , IRepository<OrderCost> orderCostRepository
            , IRepository<OrderDelivery> orderDeliveryRepository
            , IRepository<OrderLog> orderLogRepository
            , ISubOrderRepository subOrderRepository
            , IRepository<CncOrderExtra> cncOrderExtras
            , IRepository<SubOrderFlow> subOrderFlows
            , ISubOrderManager subOrderManager)
        {
            _orderRepository = orderRepository;
            _orderCostRepository = orderCostRepository;
            _orderDeliveryRepository = orderDeliveryRepository;
            _orderLogRepository = orderLogRepository;
            _subOrderRepository = subOrderRepository;
            _cncOrderExtras = cncOrderExtras;
            _subOrderFlows = subOrderFlows;
            _subOrderManager = subOrderManager;
        }

        [AllowAnonymous]
        public async Task<bool> PostTaskExterAsync(MQ_Cnc_OrderTaskDto input)
        {
            var order = await ValidCommonExterAsync(input);
            if (order != null)
                throw new BusinessException(message: $"系统已存在{input.ChannelOrderNo}相同的订单数据");

            var mainOrderNo = OrderHelper.CreateOrderNo();
            var cncOrderNo = OrderHelper.GetOrderType(EnumOrderType.Cnc) + mainOrderNo;

            await CreateOrderExterAsync(mainOrderNo, input.Order);
            await CreateOrderCostExterAsync(mainOrderNo, input.OrderCost);
            await CreateOrderDeliverieExterAsync(mainOrderNo, input.OrderDelivery);
            await CreateOrderLogAsync(mainOrderNo, $"渠道{EnumChannel.InternalTrade.GetDescription()}客户创建了新的{input.OrderType.GetDescription()}订单");

            await CreateOrderCncExterAsync(mainOrderNo, cncOrderNo, input);
            await CreateCncFlowAsync(cncOrderNo, SubOrderFlowType.Create, "", $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户创建了订单");

            return true;
        }

        [AllowAnonymous]
        public async Task<bool> PutCancelExterAsync(MQ_Cnc_OrderCancelDto input)
        {
            var order = await ValidCancelCncExterAsync(input);
            if (order == null)
                throw new BusinessException(message: $"系统不存在{input.ChannelOrderNo}的订单数据");

            var injectoinOrderNo = OrderHelper.GetOrderType(EnumOrderType.Cnc) + order.OrderNo;
            await SetCncOrderStatusAsync(order.OrderNo, EnumSubOrderStatus.Cancel);
            await CreateOrderLogAsync(order.OrderNo, $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户取消了订单");
            await CreateCncFlowAsync(injectoinOrderNo, SubOrderFlowType.Cancel, "", $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户取消了订单");
            return true;
        }

        [AllowAnonymous]
        public async Task PutPaymentExterAsync(MQ_Cnc_OrderPaymentDto input)
        {
            var order = await ValidCommonExterAsync(input);
            if (order == null)
                throw new BusinessException(message: $"系统不存在{input.ChannelOrderNo}的订单数据");

            if (order.IsPay == false)
            {
                var reult = await SetCncOrderStatusAsync(order.OrderNo, EnumSubOrderStatus.SureOrder);
                await UpdateOrderPaymentExterAsync(reult.Item2, input);
            }
            else
            {
                await UpdateOrderPaymentExterAsync(order, input);
            }

            await CreateOrderLogAsync(order.OrderNo, $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户支付了{input.PaidMoney}");

            var CncOrderNo = OrderHelper.GetOrderType(EnumOrderType.Cnc) + order.OrderNo;
            await CreateCncFlowAsync(CncOrderNo, SubOrderFlowType.Payment, "", $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户支付了{input.PaidMoney}");
        }

        [AllowAnonymous]
        public async Task<bool> PutReceiveExterAsync(MQ_Cnc_OrderReceiveDto input)
        {
            var order = await ValidReceiveCncExterAsync(input);
            if (order == null)
                throw new BusinessException(message: $"系统不存在{input.ChannelOrderNo}的订单数据");

            await SetCncOrderReceiveAsync(order.OrderNo, EnumSubOrderStatus.Finish);
            await CreateOrderLogAsync(order.OrderNo, $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户收货订单");

            var CncOrderNo = OrderHelper.GetOrderType(EnumOrderType.Cnc) + order.OrderNo;
            await CreateCncFlowAsync(CncOrderNo, SubOrderFlowType.Complete, "", $"来自渠道{EnumChannel.InternalTrade.GetDescription()}的用户收货订单");
            return true;
        }

        private async Task CreateOrderExterAsync(string orderNo, MQ_OrderTask_OrderDto input)
        {
            var order = ObjectMapper.Map<MQ_OrderTask_OrderDto, Order>(input);
            order.SetOrderNo(orderNo);
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

        private async Task CreateOrderCncExterAsync(string mainOrderNo, string orderNo, MQ_Cnc_OrderTaskDto input)
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


            var list = new List<CncOrderExtra>();
            foreach (var item in input.CncBoms)
            {
                var cncOrderExtra = new CncOrderExtra(GuidGenerator.Create()
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
            }

            await _subOrderRepository.InsertAsync(subOrder);
            await _cncOrderExtras.InsertManyAsync(list);
        }

        /// <summary>
        /// 创建Cnc订单流程
        /// </summary>
        /// <param name="childOrderNo"></param>
        /// <param name="type"></param>
        /// <param name="remark"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private async Task CreateCncFlowAsync(string childOrderNo, SubOrderFlowType type, string remark, string content)
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

            if (input.OrderType != EnumOrderType.Cnc)
                throw new BusinessException(message: $"订单不符合Cnc类型");


            var order = await _orderRepository.FindAsync(x => x.ChannelOrderNo == input.ChannelOrderNo);
            return order;
        }
        private async Task<Order> ValidCancelCncExterAsync(MQ_Cnc_OrderCancelDto input)
        {
            var order = await ValidCommonExterAsync(input);
            if (order == null)
                throw new BusinessException(message: $"订单{input.ChannelOrderNo}数据不存在");
            if (order.Status >= EnumOrderStatus.SureOrder)
                throw new BusinessException(message: $"订单{input.ChannelOrderNo}已支付,取消无效");

            return order;
        }

        private async Task<Order> ValidReceiveCncExterAsync(MQ_Cnc_OrderReceiveDto input)
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
        /// 修改Cnc订单状态
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        private async Task<(bool, Order)> SetCncOrderStatusAsync(string orderNo, EnumSubOrderStatus status)
        {
            var entity = await _subOrderRepository.GetAsync(t => t.MainOrderNo == orderNo);

            if (entity.Status == status)
            {
                throw new UserFriendlyException("请勿重复操作！");
            }

            entity.SetStatus(status);

            var CncOrder = await _subOrderRepository.UpdateAsync(entity);

            var orderEntity = await SetOrderStatusAsync(orderNo, status);

            return (!CncOrder.OrderNo.IsNullOrEmpty(), orderEntity);
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

            entity.Status = TransformCncOrderStatusToOrderStatus(status);

            var result = await _orderRepository.UpdateAsync(entity);

            return result;
        }
        private async Task<(bool, Order)> SetCncOrderReceiveAsync(string orderNo, EnumSubOrderStatus status)
        {
            var entity = await _subOrderRepository.GetAsync(t => t.MainOrderNo == orderNo);
            if (entity.Status == status)
            {
                throw new UserFriendlyException("请勿重复操作！");
            }
            entity.SetStatus(status);
            var CncOrder = await _subOrderRepository.UpdateAsync(entity);
            var orderEntity = await SetOrderStatusAsync(orderNo, status);
            return (!CncOrder.OrderNo.IsNullOrEmpty(), orderEntity);
        }

        private async Task UpdateOrderPaymentExterAsync(Order order, MQ_Cnc_OrderPaymentDto input)
        {
            order.PaidMoney = input.PaidMoney.Value;
            order.PendingMoney = input.PendingMoney.Value;
            order.PayTime = input.PayTime;
            order.IsPay = true;
            order.PayMode = (byte)input.PayModel;
            await _orderRepository.UpdateAsync(order);
        }
        /// <summary>
        /// Cnc订单状态转换成订单状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private EnumOrderStatus TransformCncOrderStatusToOrderStatus(EnumSubOrderStatus status)
        {
            return status switch
            {
                EnumSubOrderStatus.WaitCheck => EnumOrderStatus.WaitCheck,
                EnumSubOrderStatus.CheckedNoPass => EnumOrderStatus.CheckedNoPass,
                EnumSubOrderStatus.CheckedPass => EnumOrderStatus.WaitCheck,
                EnumSubOrderStatus.OfferComplete => EnumOrderStatus.CheckedPass,
                EnumSubOrderStatus.Cancel => EnumOrderStatus.Cancel,
                EnumSubOrderStatus.SureOrder => EnumOrderStatus.SureOrder,
                //EnumSubOrderStatus.Production => EnumOrderStatus.Purchasing,
                EnumSubOrderStatus.Purchasing => EnumOrderStatus.Purchasing,
                EnumSubOrderStatus.WaitSend => EnumOrderStatus.WaitSend,
                EnumSubOrderStatus.HaveSend => EnumOrderStatus.HaveSend,
                EnumSubOrderStatus.Finish => EnumOrderStatus.Finish,
                _ => throw new ArgumentException(),
            };
        }
    }
}
