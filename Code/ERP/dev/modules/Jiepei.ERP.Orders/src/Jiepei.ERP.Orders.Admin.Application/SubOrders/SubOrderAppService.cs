using Jiepei.ERP.Orders.Admin;
using Jiepei.ERP.Orders.Materials;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Orders.SubOrders.Dtos;
using Jiepei.ERP.SubOrders;
using Jiepei.ERP.Suppliers.Suppliers;
using Jiepei.ERP.Suppliers.Unionfab.Services.Order;
using Jiepei.ERP.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.SubOrders
{
    /// <summary>
    /// Sub 订单
    /// </summary>
    public class SubOrderAppService : OrdersAdminAppServiceBase, ISubOrderAppService
    {
        private readonly ISubOrderManager _subOrderManager;
        private readonly ISubOrderRepository _subOrderRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IRepository<OrderDelivery> _orderDeliveryRepository;
        private readonly IRepository<OrderCost> _orderCostRepository;
        private readonly IRepository<OrderLog> _orderLogRepository;
        private readonly IRepository<SubOrderFlow> _subOrderFlowRepository;
        private readonly IMaterialManager _materialManager;
        private readonly ID3MaterialRepository _d3MaterialRepository;
        private readonly IMaterialPriceRepository _materialPriceRepository;
        private readonly ISupplierUnionfabAppService _unionfabAppService;
        private readonly ISubOrderThreeDItemRepository _subOrderThreeDItemRepository;
        public SubOrderAppService(ISubOrderManager subOrderManager
            , IOrderRepository orderRepository
            , IRepository<OrderDelivery> orderDeliveryRepository
            , IRepository<OrderCost> orderCostRepository
            , IRepository<SubOrderFlow> subOrderFlowRepository
            , IMaterialManager materialManager
            , ID3MaterialRepository d3MaterialRepository
            , IMaterialPriceRepository materialPriceRepository
            , ISubOrderRepository subOrderRepository
            , IRepository<OrderLog> orderLogRepository
            , ISupplierUnionfabAppService unionfabAppService
            , ISubOrderThreeDItemRepository subOrderThreeDItemRepository)
        {
            _subOrderManager = subOrderManager;
            _orderRepository = orderRepository;
            _orderDeliveryRepository = orderDeliveryRepository;
            _orderCostRepository = orderCostRepository;
            _subOrderFlowRepository = subOrderFlowRepository;
            _materialManager = materialManager;
            _d3MaterialRepository = d3MaterialRepository;
            _materialPriceRepository = materialPriceRepository;
            _subOrderRepository = subOrderRepository;
            _orderLogRepository = orderLogRepository;
            _unionfabAppService = unionfabAppService;
            _subOrderThreeDItemRepository = subOrderThreeDItemRepository;
        }


        ///// <summary>
        ///// 创建订单
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[RemoteService(false)]
        //public async Task<OrderBaseDto> CreateAsync(CreateOrderExtraDto input)
        //{
        //    //var orderNo = OrderHelper.CreateOrderNo(EnumOrderType.Mold);
        //    var orderNo = OrderHelper.CreateOrderNo();
        //    var orderid = GuidGenerator.Create();
        //    // 创建模具订单
        //    var subOrderId = await CreateSubOrderAsync(input, orderNo, orderid);
        //    //创建Bom
        //    var deliveryInfo = await CreateD3OrderExtraAsync(input.ExtraProperties, subOrderId, input.ChannelId);

        //    // 创建订单
        //    var result = await CreateOrderAsync(input, orderNo, orderid, deliveryInfo.Item1, deliveryInfo.Item2);
        //    // 创建配送信息
        //    await CreateDelivery(input.DeliveryInfo, orderNo);

        //    await CreateOrderCostAsync(orderNo, deliveryInfo.Item1);
        //    // 创建日志
        //    await CreateOperatorLog(orderNo, input.Remark, "创建订单");
        //    return new OrderBaseDto { Id = result.Id, OrderNo = orderNo };
        //}

        ///// <summary>
        ///// 获取订单详情
        ///// </summary>
        ///// <param name="id">主订单 Id</param>
        ///// <param name="orderType">订单类型</param>
        ///// <returns></returns>
        //public async Task<SubOrderDetailDto> GetDetailAsync(Guid id, EnumOrderType orderType)
        //{
        //    Check.NotNull(id, nameof(id));
        //    Check.NotNull(orderType, nameof(orderType));

        //    var entity = await _subOrderManager.GetSubOrderDetail(id);

        //    var result = new SubOrderDetailDto
        //    {
        //        SubOrder = ObjectMapper.Map<SubOrder, SubOrderDto>(entity.Item1),
        //        SubOrderFlow = ObjectMapper.Map<SubOrderFlow, SubOrderFlowDto>(entity.Item2)
        //    };

        //    await GetSubOrderExtra(result, orderType);

        //    return result;
        //}


        ///// <summary>
        ///// 取消订单
        ///// </summary>
        ///// <param name="id">主订单 Id</param>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task CancelAsync(Guid id, CancelInput input)
        //{
        //    var subOrder = await _subOrderRepository.FindAsync(t => t.OrderId == id);
        //    var d3Material = await _subOrderThreeDItemRepository.GetListAsync(t => t.SubOrderId == subOrder.Id);
        //    foreach (var item in d3Material.Select(t => t.SupplierOrderCode).Distinct())
        //    {
        //        var closeOrder = new CloseOrderRequest(item);
        //        await _unionfabAppService.CloseAsync(closeOrder);
        //    }

        //    await _subOrderManager.CancelAsync(id, input.Rremark);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task CheckAsync(Guid id, CheckInput input)
        //{
        //    await _subOrderManager.CheckAsync(id, input.IsPassed, input.Remark);
        //}

        ///// <summary>
        ///// 完成订单
        ///// </summary>
        ///// <param name="id">主订单 Id</param>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task CompleteAsync(Guid id, CompleteInput input)
        //{
        //    await _subOrderManager.CompleteAsync(id, input.Remark);
        //}


        ///// <summary>
        ///// 发货
        ///// </summary>
        ///// <param name="id">主订单 Id</param>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task DeliverAsync(Guid id, DeliverInput input)
        //{
        //    await _subOrderManager.DeliverAsync(id, input.TrackingNo, input.CourierCompany, input.Remark);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task DesignChangeAsync(Guid id, DesignChangeInput input)
        //{
        //    await _subOrderManager.DesignChange(id, input.FileName, input.FilePath, input.Picture, input.ProMoney, input.remark);

        //}

        ///// <summary>
        ///// 投产
        ///// </summary>
        ///// <param name="id">主订单 Id</param>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task ManufactureAsync(Guid id, ManufactureInput input)
        //{
        //    await _subOrderManager.ManufactureAsync(id, input.Remark);
        //}

        ///// <summary>
        ///// 报价
        ///// </summary>
        ///// <param name="id">主订单 Id</param>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task OfferAsync(Guid id, OfferInput input)
        //{
        //    //获取成本价 (优联价格)
        //    //var subOrder = await _subOrderRepository.FindAsync(t => t.OrderId == id);
        //    //var d3Material = await _subOrderThreeDItemRepository.GetListAsync(t => t.SubOrderId == subOrder.Id);
        //    //var cost = 0m;
        //    //foreach (var item in d3Material.Select(t => t.SupplierOrderCode).Distinct())
        //    //{
        //    //    var unionfabOrder = await _unionfabAppService.GetAsync(item);
        //    //    if (unionfabOrder != null)
        //    //        cost += unionfabOrder.Data.Price.TotalPriceWithTax.HasValue ? unionfabOrder.Data.Price.TotalPriceWithTax.Value : 0;
        //    //}


        //    await _subOrderManager.OfferAsync(id, input.Cost, input.SellingPrice, input.ShipPrice, input.DiscountMoney, input.Remark);
        //}

        /// <summary>
        /// 修改交期
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeliveryDays(Guid id, DeliveryDaysInput input)
        {
            await _subOrderManager.UpdateDeliveryDaysAsync(id, input.DeliveryDays, input.Rremark);
        }



        private async Task GetSubOrderExtra(SubOrderDetailDto input, EnumOrderType orderType)
        {
            switch (orderType)
            {
                case EnumOrderType.Mold:
                    input.SubOrderItem = await GetMoldOrderExtraDtoAsync(input.SubOrder.Id);
                    break;
                case EnumOrderType.Injection:
                    input.SubOrderItem = await GetInjectionOrderExtraDtoAsync(input.SubOrder.Id);
                    break;
                case EnumOrderType.Cnc:
                    input.SubOrderItem = await GetCncOrderExtraDtoAsync(input.SubOrder.Id);
                    break;
                default:
                    throw new UserFriendlyException("订单类型不存在");
            }
        }

        /// <summary>
        /// 获取模具扩展信息
        /// </summary>
        /// <param name="id">子订单 Id</param>
        /// <returns></returns>
        private async Task<SubOrderMoldItemDto> GetMoldOrderExtraDtoAsync(Guid id)
        {
            return ObjectMapper.Map<SubOrderMoldItem, SubOrderMoldItemDto>(await _subOrderManager.GetSubOrderMoldItemAsync(id));
        }

        /// <summary>
        /// 获取注塑扩展信息
        /// </summary>
        /// <param name="id">子订单 Id</param>
        /// <returns></returns>
        private async Task<SubOrderInjectionItemDto> GetInjectionOrderExtraDtoAsync(Guid id)
        {
            return ObjectMapper.Map<SubOrderInjectionItem, SubOrderInjectionItemDto>(await _subOrderManager.GetSubOrderInjectionItemAsync(id));
        }
        /// <summary>
        /// 获取 CNC 扩展信息
        /// </summary>
        /// <param name="id">子订单 Id</param>
        /// <returns></returns>
        private async Task<SubOrderCncItemDto> GetCncOrderExtraDtoAsync(Guid id)
        {
            return ObjectMapper.Map<SubOrderCncItem, SubOrderCncItemDto>(await _subOrderManager.GetSubOrderCncItemAsync(id));
        }

        private async Task<Order> CreateOrderAsync(CreateOrderExtraDto input, string orderNo, Guid orderid, decimal sunPrice, int deliveryDays)
        {
            //计算交期日期
            var deliveryDate = await _materialManager.Calculation3DDeliveryDays(deliveryDays);

            var orderEntity = new Order(
                id: orderid,
                orderNo: orderNo,
                status: EnumOrderStatus.WaitCheck,
                channel: input.ChannelId,
                channelOrderNo: input.ChannelOrderNo,
                channelUserId: input.CustomerId,
                customerRemark: input.Remark,
                orderType: input.OrderType,
                //cost: sunPrice,
                //sellingPrice: sunPrice,
                //pendingMoney: sunPrice,
                deliveryDate: deliveryDate,
                deliveryDays: deliveryDays);
            return await _orderRepository.InsertAsync(orderEntity);
        }

        private async Task<OrderDelivery> CreateDelivery(CreateDeliveryDto input, string orderNo)
        {
            var deliveryEntity = ObjectMapper.Map<CreateDeliveryDto, OrderDelivery>(input);
            deliveryEntity.SetOrderNo(orderNo);
            return await _orderDeliveryRepository.InsertAsync(deliveryEntity);
        }

        private async Task<OrderCost> CreateOrderCostAsync(string orderNo, decimal price)
        {
            var entity = new OrderCost(
                GuidGenerator.Create(),
                orderNo,
                price,
                price * 0.08m,
                0.08m,
                0);
            return await _orderCostRepository.InsertAsync(entity);
        }

        private async Task CreateOperatorLog(string orderNo, string remark, string note)
        {
            await CreateOrderLog(orderNo, note);
            await CreateSubOrderFlow(orderNo, remark, note);
        }

        private async Task CreateOrderLog(string orderNo, string note)
        {
            var entity = new OrderLog(GuidGenerator.Create(), orderNo, "创建订单");
            await _orderLogRepository.InsertAsync(entity);
        }

        private async Task CreateSubOrderFlow(string orderNo, string remark, string note)
        {
            var entity = new SubOrderFlow(GuidGenerator.Create(), orderNo, EnumSubOrderFlowType.Create, remark, note);
            await _subOrderFlowRepository.InsertAsync(entity);
        }

        private async Task<Guid> CreateSubOrderAsync(CreateOrderExtraDto createOrderExtraDto, string orderNo, Guid orderId)
        {
            var subOrderId = GuidGenerator.Create();
            var orderEntity = new SubOrder(
                  id: subOrderId
                , orderId: orderId
                , orderNo: orderNo
                , channel: createOrderExtraDto.ChannelId
                , channelOrderNo: createOrderExtraDto.ChannelOrderNo
                , channelUserId: createOrderExtraDto.CustomerId
                , cost: 0m
                , sellingPrice: 0m
                , orderType: createOrderExtraDto.OrderType
                , organizationUnitId: createOrderExtraDto.OrganizationUnitId
                , status: EnumSubOrderStatus.WaitCheck
                , remark: createOrderExtraDto.Remark
            );
            await _subOrderRepository.InsertAsync(orderEntity);
            return subOrderId;
        }
        private async Task<(decimal, int)> CreateD3OrderExtraAsync(ExtraPropertyDictionary input, Guid subOrderId, Guid channelId)
        {
            var sumPrice = 0m;
            var maxDay = 0;
            var extraOrder = JsonConvert.DeserializeObject<CreateSubOrderThreeDItemListDto>(System.Text.Json.JsonSerializer.Serialize(input));
            foreach (var item in extraOrder.SubOrderThreeDItemDtos ?? new List<CreateSubOrderThreeDItemDto>())
            {
                var cncOrderBomEntity = ObjectMapper.Map<CreateSubOrderThreeDItemDto, SubOrderThreeDItem>(item);

                var d3MaterialEntiy = await _d3MaterialRepository.GetQueryableAsync();
                var materialPriceEntiy = await _materialPriceRepository.GetQueryableAsync();
                var query = d3MaterialEntiy
                    .Join(materialPriceEntiy, e => e.Id, o => o.MaterialId, (e, o) => new { e, o })
                    .Where(t => t.o.MaterialId == cncOrderBomEntity.MaterialId)
                    .Where(t => t.o.ChannelId == channelId);
                var entiy = (await AsyncExecuter.ToListAsync(query)).First();

                //单价
                var unitPrice = _materialManager.GetUnitPrice(cncOrderBomEntity.Volume, Convert.ToDecimal(entiy.e.Density), entiy.o.Price);
                //后处理价格
                var handleMethodDescPrice = _materialManager.GetHandleMethodDescPrice(cncOrderBomEntity.HandleMethod, cncOrderBomEntity.HandleMethodDesc);

                var orginalMoney = entiy.o.Discount * unitPrice * cncOrderBomEntity.Count;

                //交期天数
                var deliveryNum = await _materialManager.Calculation3DDelivery(
                     channelId
                   , cncOrderBomEntity.MaterialId
                   , cncOrderBomEntity.HandleMethod
                   , cncOrderBomEntity.HandleMethodDesc);
                if (maxDay < deliveryNum)
                    maxDay = deliveryNum;

                sumPrice += orginalMoney;
                cncOrderBomEntity.SetSubOrderId(subOrderId);
                cncOrderBomEntity.SetHandleFee(handleMethodDescPrice);
                cncOrderBomEntity.SetPrice(unitPrice);
                cncOrderBomEntity.SetOrginalMoney(orginalMoney);
            }

            return (sumPrice, maxDay);
        }
    }
}
