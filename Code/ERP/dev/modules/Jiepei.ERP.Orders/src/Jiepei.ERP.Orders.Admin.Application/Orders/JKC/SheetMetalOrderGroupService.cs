using Jiepei.ERP.Orders.Admin.Orders;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Orders.SubOrders;
using Jiepei.ERP.SubOrders;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace Jiepei.ERP.Orders.Admin
{
    public class SheetMetalOrderGroupService : OrdersAdminAppServiceBase, ISheetMetalOrderGroupService
    {
        /// <summary>
        /// 订单包仓储
        /// </summary>
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDeliveryRepository _orderDeliverieRepository;
        private readonly IOrderCostRepository _orderCostRepository;
        private readonly ISubOrderRepository _subOrderRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<OrderLog> _orderLogRepository;
        private readonly IRepository<SubOrderFlow, Guid> _subOrderFlowRepository;
        private readonly ISubOrderSheetMetalItemRepository _subOrderSheetMetalItemRepository;

        public SheetMetalOrderGroupService(IOrderRepository orderRepository
            , IOrderDeliveryRepository orderDeliverieRepository
            , IOrderCostRepository orderCostRepository
            , ISubOrderRepository subOrderRepository
            , IUnitOfWorkManager unitOfWorkManager
            , IRepository<OrderLog> orderLogRepository
            , IRepository<SubOrderFlow, Guid> subOrderFlowRepository
            , ISubOrderSheetMetalItemRepository subOrderSheetMetalItems)
        {
            _orderRepository = orderRepository;
            _orderDeliverieRepository = orderDeliverieRepository;
            _orderCostRepository = orderCostRepository;
            _subOrderRepository = subOrderRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _orderLogRepository = orderLogRepository;
            _subOrderFlowRepository = subOrderFlowRepository;
            _subOrderSheetMetalItemRepository = subOrderSheetMetalItems;
        }

        private static T GetSerializeParameter<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        private MethodBaseInfo GetMethodBaseInfo(string methodName)
        {
            var declaringType = System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType;
            string nameSpace = "";
            if (declaringType != null)
            {
                nameSpace = declaringType.FullName;
            }
            return new MethodBaseInfo(methodName, nameSpace);
        }

        /// <summary>
        /// 修改订单包价格
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Tuple<ApiHttpResponseDto, MethodBaseInfo>> UpdateOrderGroupPrice(string data)
        {
            var methodBaseInfo = GetMethodBaseInfo(nameof(UpdateOrderGroupPrice));
            try
            {
                var param = GetSerializeParameter<ApiOrderMainSalePricetDto>(data);
                if (string.IsNullOrWhiteSpace(param.GroupNo))
                    return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                        new ApiHttpResponseDto($"订单包编号不能为空！无效的参数{nameof(ApiOrderMainSalePricetDto)}"),
                        methodBaseInfo);
                var order = await _orderRepository.FindAsync(o => o.OrderNo == param.GroupNo);
                if (null == order)
                    return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                        new ApiHttpResponseDto($"未通过该订单包编号找到相应记录！订单包编号：{param.GroupNo}"),
                        methodBaseInfo);
                //订单包中所有订单产品总价

                using var uow = _unitOfWorkManager.Begin(isTransactional: true);

                var orderCost = await _orderCostRepository.FindAsync(t => t.OrderNo == param.GroupNo);
                var subOrder = await _subOrderRepository.FindAsync(t => t.OrderId == order.Id);

                orderCost.SetDiscountMoney(param.PreferentialMoney);
                orderCost.SetShipMoney(param.ShipMoney);
                orderCost.SetTaxMoney(param.TaxMoney);
                //产品销售价
                var sumPrice = orderCost.ProMoney + param.PreferentialMoney + param.ShipMoney + param.TaxMoney;
                var oldPrice = order.SellingPrice;
                order.SetSellingPrice(sumPrice);
                subOrder.SetSellingPrice(sumPrice);

                await _orderCostRepository.UpdateAsync(orderCost);
                await _orderRepository.UpdateAsync(order);
                await _subOrderRepository.UpdateAsync(subOrder);

                await CreateOrderLog(order.OrderNo, "第三方修改订单包价格" + oldPrice + "--->" + sumPrice);
                await CreateSubOrderFlow(order.OrderNo, EnumSubOrderFlowType.Offer, "修改报价", "第三方修改订单包价格" + oldPrice + "--->" + sumPrice);

                await uow.CompleteAsync();

                return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(new ApiHttpResponseDto(true, "修改订单包价格成功"),
                    methodBaseInfo);
            }
            catch (Exception ex)
            {
                return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(new ApiHttpResponseDto($"修改订单包价格失败，原因：{ex.Message}"),
                    methodBaseInfo);
            }
        }

        /// <summary>
        /// 修改用户收货地址
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Tuple<ApiHttpResponseDto, MethodBaseInfo>> UpdateOrderMainReceiver(string data)
        {
            var methodBaseInfo = GetMethodBaseInfo(nameof(UpdateOrderMainReceiver));
            try
            {
                var param = GetSerializeParameter<ApiOrderMainReceiverDto>(data);
                if (string.IsNullOrWhiteSpace(param.GroupNo))
                    return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                        new ApiHttpResponseDto($"订单包编号不能为空！无效的参数{nameof(ApiOrderMainReceiverDto)}"),
                        methodBaseInfo);
                var order = await _orderRepository.FindAsync(o => o.OrderNo == param.GroupNo);
                if (null == order)
                    return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                        new ApiHttpResponseDto($"未通过该订单包编号找到相应记录！订单包编号：{param.GroupNo}"), methodBaseInfo);
                using var uow = _unitOfWorkManager.Begin(isTransactional: true);

                var orderDeliverie = await _orderDeliverieRepository.FindAsync(t => t.OrderNo == param.GroupNo);
                //无法使用框架的ObjectMapper映射
                orderDeliverie.SetReceiverName(param.ReceiverName);
                orderDeliverie.SetReceiverTel(param.ReceiverTel);
                orderDeliverie.SetOrderContactName(param.OrderContactName);
                orderDeliverie.SetOrderContactMobile(param.OrderContactMobile);
                orderDeliverie.SetReceiverAddress(param.ReceiverAddress);

                await _orderDeliverieRepository.UpdateAsync(orderDeliverie);
                await CreateOrderLog(order.OrderNo, "第三方修改用户收货地址");

                await uow.CompleteAsync();

                return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(new ApiHttpResponseDto(true, "修改用户收货地址成功"), methodBaseInfo);
            }
            catch (Exception ex)
            {
                return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(new ApiHttpResponseDto($"修改用户收货地址失败，原因：{ex.Message}"),
                    methodBaseInfo);
            }
        }


        /// <summary>
        /// 修改订单交货天数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Tuple<ApiHttpResponseDto, MethodBaseInfo>> UpdateOrderDetailDeliveryDay(string data)
        {
            var methodBaseInfo = GetMethodBaseInfo(nameof(UpdateOrderDetailDeliveryDay));
            try
            {
                var param = GetSerializeParameter<ApiOrderDetailDeliveryDayDto>(data);
                if (string.IsNullOrWhiteSpace(param.OrderNo))
                    return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                        new ApiHttpResponseDto($"订单编号不能为空！无效的参数{nameof(ApiOrderDetailDeliveryDayDto)}"),
                        methodBaseInfo);
                var subOrder = await _subOrderRepository.FindAsync(o => o.OrderNo == param.OrderNo);
                if (null == subOrder)
                    return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                        new ApiHttpResponseDto($"未通过该订单编号找到相应记录！订单编号：{param.OrderNo}"),
                        methodBaseInfo);

                using var uow = _unitOfWorkManager.Begin(isTransactional: true);

                var order = await _orderRepository.FindAsync(o => o.Id == subOrder.OrderId);
                var oldDeliveryDay = order.DeliveryDays;
                order.SetDeliveryDays(param.DeliveryDays);
                order.SetDeliveryDate(param.DeliveryDate);

                await _orderRepository.UpdateAsync(order);
                await CreateOrderLog(order.OrderNo, "第三方修改交期" + oldDeliveryDay + "--->" + param.DeliveryDays);
                await CreateSubOrderFlow(order.OrderNo, EnumSubOrderFlowType.DeliveryDay, "修改交期", "第三方修改交期" + oldDeliveryDay + "--->" + param.DeliveryDays);

                await uow.CompleteAsync();

                return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                    new ApiHttpResponseDto(true, "修改订单交货天数成功"),
                    methodBaseInfo);
            }
            catch (Exception ex)
            {
                return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                    new ApiHttpResponseDto($"修改订单交货天数失败，原因：{ex.Message}"),
                    methodBaseInfo);
            }
        }

        /// <summary>
        /// 修改订单快递信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Tuple<ApiHttpResponseDto, MethodBaseInfo>> DeliverProducts(string data)
        {
            var methodBaseInfo = GetMethodBaseInfo(nameof(DeliverProducts));
            try
            {
                var param = GetSerializeParameter<ApiDeliverProductsDto>(data);
                if (string.IsNullOrWhiteSpace(param.OrderNo))
                    return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                        new ApiHttpResponseDto($"订单编号不能为空！无效的参数{nameof(ApiDeliverProductsDto)}"),
                        methodBaseInfo);
                var subOrder = await _subOrderRepository.FindAsync(o => o.OrderNo == param.OrderNo);
                if (null == subOrder)
                    return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                        new ApiHttpResponseDto($"未通过该订单编号找到相应记录！订单编号：{param.OrderNo}"),
                        methodBaseInfo);

                using var uow = _unitOfWorkManager.Begin(isTransactional: true);
                var order = await _orderRepository.FindAsync(o => o.Id == subOrder.OrderId);

                order.SetTrackingNo(param.SendExpName);
                order.SetCourierCompany(param.SendExpNo);
                order.SetOrderStatus(EnumOrderStatus.HaveSend);//将状态改为已发货(待收货)
                subOrder.SetStatus(EnumSubOrderStatus.HaveSend);

                await _orderRepository.UpdateAsync(order);
                await _subOrderRepository.UpdateAsync(subOrder);

                await CreateOrderLog(order.OrderNo, "第三方发货");
                await CreateSubOrderFlow(order.OrderNo, EnumSubOrderFlowType.Deliver, "发货", "第三方发货");

                await uow.CompleteAsync();

                return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                    new ApiHttpResponseDto(true, "修改订单快递信息成功"),
                    methodBaseInfo);
            }
            catch (Exception ex)
            {
                return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                    new ApiHttpResponseDto($"修改订单快递信息失败，原因：{ex.Message}"),
                    methodBaseInfo);
            }
        }

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Tuple<ApiHttpResponseDto, MethodBaseInfo>> UpdateOrderDetailStatus(string data)
        {
            var methodBaseInfo = GetMethodBaseInfo(nameof(UpdateOrderDetailStatus));
            try
            {
                var param = GetSerializeParameter<ApiOrderDetailStatusDto>(data);
                if (string.IsNullOrWhiteSpace(param.OrderNo))
                    return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                        new ApiHttpResponseDto($"订单编号不能为空！无效的参数{nameof(ApiOrderDetailStatusDto)}"),
                        methodBaseInfo);
                var subOrder = await _subOrderRepository.FindAsync(o => o.OrderNo == param.OrderNo);
                if (null == subOrder)
                    return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                        new ApiHttpResponseDto($"未通过该订单编号找到相应记录！订单编号：{param.OrderNo}"),
                        methodBaseInfo);
                if (!Enum.IsDefined(typeof(EnumApiOrderStatus), param.Status))
                    return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                        new ApiHttpResponseDto($"无效的订单状态！无法转换的枚举值,参数值{JsonConvert.SerializeObject(param)}"),
                        methodBaseInfo);

                using var uow = _unitOfWorkManager.Begin(isTransactional: true);

                var order = await _orderRepository.FindAsync(t => t.Id == subOrder.OrderId);
                if (subOrder.Status != SubOrderStatusMap((EnumApiOrderStatus)param.Status))
                    subOrder.Status = SubOrderStatusMap((EnumApiOrderStatus)param.Status);
                if (order.Status != OrderStatusMap((EnumApiOrderStatus)param.Status))
                    order.Status = OrderStatusMap((EnumApiOrderStatus)param.Status);

                await _orderRepository.UpdateAsync(order);
                await _subOrderRepository.UpdateAsync(subOrder);

                await CreateOrderLog(order.OrderNo, "第三方修改状态");
                await CreateSubOrderFlow(order.OrderNo, EnumSubOrderFlowType.UpdateStatus, "修改状态", "第三方修改状态");


                await uow.CompleteAsync();

                return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                    new ApiHttpResponseDto(true, "修改订单状态成功"),
                    methodBaseInfo);
            }
            catch (Exception ex)
            {
                return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                    new ApiHttpResponseDto($"修改订单状态失败，原因：{ex.Message}"),
                    methodBaseInfo);
            }
        }

        /// <summary>
        /// 修改订单产品金额
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Tuple<ApiHttpResponseDto, MethodBaseInfo>> UpdateOrderDetailTotalMoney(string data)
        {
            var methodBaseInfo = GetMethodBaseInfo(nameof(UpdateOrderDetailTotalMoney));
            try
            {
                var param = GetSerializeParameter<ApiOrderDetailTotalMoneyDto>(data);
                if (string.IsNullOrWhiteSpace(param.OrderNo))
                    return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                        new ApiHttpResponseDto($"订单编号不能为空！无效的参数{nameof(ApiOrderDetailTotalMoneyDto)}"),
                        methodBaseInfo);
                var subOrder = await _subOrderRepository.FindAsync(o => o.OrderNo == param.OrderNo);

                if (null == subOrder)
                    return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                        new ApiHttpResponseDto($"未通过该订单编号找到相应记录！订单编号：{param.OrderNo}"),
                        methodBaseInfo);
                //修改订单报价
                using var uow = _unitOfWorkManager.Begin(isTransactional: true);

                var order = await _orderRepository.FindAsync(t => t.Id == subOrder.OrderId);
                var orderCost = await _orderCostRepository.FindAsync(t => t.OrderNo == order.OrderNo);
                var oldMoney = orderCost.ProMoney;
                var sumMoney = orderCost.ShipMoney + orderCost.TaxMoney + orderCost.DiscountMoney + param.TotalMoney;
                orderCost.SetProMoney(param.TotalMoney);
                order.SetSellingPrice(sumMoney);
                subOrder.SetSellingPrice(sumMoney);

                await _orderRepository.UpdateAsync(order);
                await _subOrderRepository.UpdateAsync(subOrder);
                await _orderCostRepository.UpdateAsync(orderCost);

                await CreateOrderLog(order.OrderNo, "第三方修改订单产品金额" + oldMoney + "--->" + param.TotalMoney);
                await CreateSubOrderFlow(order.OrderNo, EnumSubOrderFlowType.Offer, "修改订单产品金额", "第三方修改订单产品金额" + oldMoney + "--->" + param.TotalMoney);

                await uow.CompleteAsync();


                return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                    new ApiHttpResponseDto(true, "修改订单产品金额成功"),
                    methodBaseInfo);
            }
            catch (Exception ex)
            {
                return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                    new ApiHttpResponseDto($"修改订单产品金额失败，原因：{ex.Message}"),
                    methodBaseInfo);
            }
        }

        /// <summary>
        /// 修改订单产品套数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Tuple<ApiHttpResponseDto, MethodBaseInfo>> UpdateOrderProductNum(string data)
        {
            var methodBaseInfo = GetMethodBaseInfo(nameof(UpdateOrderProductNum));
            try
            {
                var param = GetSerializeParameter<ApiOrderProductNumDto>(data);
                if (string.IsNullOrWhiteSpace(param.OrderNo))
                    return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                        new ApiHttpResponseDto($"订单编号不能为空！无效的参数{nameof(ApiOrderDetailTotalMoneyDto)}"),
                        methodBaseInfo);
                var subOrder = await _subOrderRepository.FindAsync(o => o.OrderNo == param.OrderNo);
                if (null == subOrder)
                    return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                        new ApiHttpResponseDto($"未通过该订单编号找到相应记录！订单编号：{param.OrderNo}"),
                        methodBaseInfo);
                using var uow = _unitOfWorkManager.Begin(isTransactional: true);

                var subOrderSheetMetal = await _subOrderSheetMetalItemRepository.FindAsync(t => t.SubOrderId == subOrder.Id);
                var order = await _orderRepository.FindAsync(t => t.Id == subOrder.OrderId);
                var orderDeliverie = await _orderDeliverieRepository.FindAsync(t => t.OrderNo == order.OrderNo);
                var oldNum = subOrderSheetMetal.ProductNum;

                subOrderSheetMetal.SetProductNum(param.ProductNum);
                orderDeliverie.SetWeight(param.Weight);

                await _orderDeliverieRepository.UpdateAsync(orderDeliverie);
                await _subOrderSheetMetalItemRepository.UpdateAsync(subOrderSheetMetal);

                await CreateOrderLog(order.OrderNo, "第三方修改产品套数" + oldNum + "--->" + param.ProductNum);
                await CreateSubOrderFlow(order.OrderNo, EnumSubOrderFlowType.ProductNum, "修改订单产品金额", "第三方修改产品套数" + oldNum + "--->" + param.ProductNum);

                await uow.CompleteAsync();

                return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                    new ApiHttpResponseDto(true, "修改订单产品套数成功"),
                    methodBaseInfo);
            }
            catch (Exception ex)
            {
                return new Tuple<ApiHttpResponseDto, MethodBaseInfo>(
                    new ApiHttpResponseDto($"修改订单产品套数失败，原因：{ex.Message}"),
                    methodBaseInfo);
            }
        }

        private async Task CreateOrderLog(string orderNo, string note)
        {
            var entity = new OrderLog(GuidGenerator.Create(), orderNo, note);
            await _orderLogRepository.InsertAsync(entity);
        }

        private async Task CreateSubOrderFlow(string orderNo, EnumSubOrderFlowType type, string remark, string note)
        {
            var entity = new SubOrderFlow(GuidGenerator.Create(), orderNo, type, remark, note);
            await _subOrderFlowRepository.InsertAsync(entity);
        }

        private static EnumOrderStatus OrderStatusMap(EnumApiOrderStatus remotelyEnum)
        {
            switch (remotelyEnum)
            {
                case EnumApiOrderStatus.已取消:
                    return EnumOrderStatus.Cancel;
                case EnumApiOrderStatus.待审核:
                    return EnumOrderStatus.WaitCheck;
                case EnumApiOrderStatus.审核不通过:
                    return EnumOrderStatus.CheckedNoPass;
                case EnumApiOrderStatus.审核通过:
                    return EnumOrderStatus.CheckedPass;
                case EnumApiOrderStatus.生产中:
                    return EnumOrderStatus.Purchasing;
                case EnumApiOrderStatus.已发货:
                    return EnumOrderStatus.HaveSend;
                case EnumApiOrderStatus.交易完成:
                    return EnumOrderStatus.Finish;
                default:
                    return EnumOrderStatus.Purchasing;
            }
        }

        private static EnumSubOrderStatus SubOrderStatusMap(EnumApiOrderStatus remotelyEnum)
        {
            switch (remotelyEnum)
            {
                case EnumApiOrderStatus.已取消:
                    return EnumSubOrderStatus.Cancel;
                case EnumApiOrderStatus.待审核:
                    return EnumSubOrderStatus.WaitCheck;
                case EnumApiOrderStatus.审核不通过:
                    return EnumSubOrderStatus.CheckedNoPass;
                case EnumApiOrderStatus.审核通过:
                    return EnumSubOrderStatus.CheckedPass;
                case EnumApiOrderStatus.生产中:
                    return EnumSubOrderStatus.Purchasing;
                case EnumApiOrderStatus.已发货:
                    return EnumSubOrderStatus.HaveSend;
                case EnumApiOrderStatus.交易完成:
                    return EnumSubOrderStatus.Finish;
                default:
                    return EnumSubOrderStatus.Purchasing;
            }
        }
    }
}
