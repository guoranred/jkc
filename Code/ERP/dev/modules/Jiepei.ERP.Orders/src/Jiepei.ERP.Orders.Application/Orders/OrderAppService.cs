using Jiepei.ERP.DeliverCentersClient.Dto.SheetMetal;
using Jiepei.ERP.Orders.Application.External;
using Jiepei.ERP.Orders.Application.External.Order;
using Jiepei.ERP.Orders.Application.External.Order.Models;
using Jiepei.ERP.Orders.Orders.Dtos;
using Jiepei.ERP.Orders.SubOrders;
using Jiepei.ERP.Orders.SubOrders.Dtos;
using Jiepei.ERP.SubOrders;
using Jiepei.ERP.Suppliers.Suppliers;
using Jiepei.ERP.Suppliers.Unionfab.Services.Order;
using Jiepei.ERP.Utilities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.Orders
{
    [Authorize]
    public class OrderAppService : OrdersAppService, IOrderAppService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRepository<OrderCost> _orderCostRepository;
        private readonly IRepository<OrderDelivery> _orderDeliveryRepository;
        private readonly ISubOrderManager _subOrderManager;
        private readonly ISubOrderRepository _subOrderRepository;
        private readonly ISubOrderAppService _subOrderAppService;
        private readonly ISubOrderThreeDItemRepository _orderThreeDItemRepository;
        private readonly ISubOrderCncItemRepository _orderCncItemRepository;
        private readonly ISubOrderSheetMetalItemRepository _orderSheetMetalItemsRepository;
        private readonly ISupplierUnionfabAppService _unionfabAppService;
        private readonly IRepository<SubOrderFlow> _subOrderFlowsRepository;
        public OrderExternalService _orderExternalService { get; set; }

        public OrderAppService(IOrderRepository orderRepository
            , IRepository<OrderCost> orderCostRepository
            , IRepository<OrderDelivery> orderDeliveryRepository
            , ISubOrderManager subOrderManager
            , ISubOrderRepository subOrderRepository
            , ISubOrderAppService subOrderAppService
            , ISubOrderThreeDItemRepository orderThreeDItemRepository
            , IRepository<SubOrderFlow> subOrderFlowsRepository
            , ISupplierUnionfabAppService unionfabAppService
            , ISubOrderCncItemRepository orderCncItemRepository
            , ISubOrderSheetMetalItemRepository orderSheetMetalItemsRepository)
        {
            _orderRepository = orderRepository;
            _orderCostRepository = orderCostRepository;
            _orderDeliveryRepository = orderDeliveryRepository;
            _subOrderManager = subOrderManager;
            _subOrderRepository = subOrderRepository;
            _subOrderAppService = subOrderAppService;
            _orderThreeDItemRepository = orderThreeDItemRepository;
            _subOrderFlowsRepository = subOrderFlowsRepository;
            _unionfabAppService = unionfabAppService;
            _orderCncItemRepository = orderCncItemRepository;
            _orderSheetMetalItemsRepository = orderSheetMetalItemsRepository;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OrderBaseDto> CreateAsync(CreateOrderExtraDto input)
        {
            var OrderBaseDto = new OrderBaseDto();
            switch (input.OrderType)
            {
                case EnumOrderType.Mold:
                    break;
                case EnumOrderType.Injection:
                    break;
                case EnumOrderType.Cnc:
                    OrderBaseDto = await _subOrderAppService.CreateCncAsync(input);
                    break;
                case EnumOrderType.Print3D:
                    OrderBaseDto = await _subOrderAppService.CreateThreeDAsync(input);
                    break;
                case EnumOrderType.SheetMetal:
                    OrderBaseDto = await _subOrderAppService.CreateSheetMetalAsync(input);
                    break;
                default:
                    break;
            }

            return OrderBaseDto;
        }

        public async Task<List<OrderDto>> GetListAsync(List<Guid> ids)
        {
            var orders = await _orderRepository.GetListAsync(x => ids.Contains(x.Id));
            var dtos = new List<OrderDto>();
            foreach (var entity in orders ?? new List<Order>())
            {
                dtos.Add(ObjectMapper.Map<Order, OrderDto>(entity));
            }
            return dtos;
        }


        /// <summary>
        /// 获取用户3d订单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<CustomerOrderListDto>> GetCustomer3DOrderListAsync(GetCustomer3DOrderListInput input)
        {
            var query = await _orderRepository.GetQueryableAsync();
            var subOrder = await _subOrderRepository.GetQueryableAsync();
            var d3Order = await _orderThreeDItemRepository.GetQueryableAsync();

            var query2 = query
            .Where(t => t.ChannelId == input.ChannelId)
            .Where(t => t.OrderType == EnumOrderType.Print3D)
            .Where(t => t.ChannelUserId == CurrentUser.Id.ToString())
            .WhereIf(!input.OrderNo.IsNullOrEmpty(), t => t.OrderNo == input.OrderNo)
            .WhereIf(input.Status.HasValue, t => t.Status == input.Status)
            .WhereIf(input.StartDate.HasValue, t => t.CreationTime > input.StartDate)
            .WhereIf(input.EndDate.HasValue, t => t.CreationTime < input.EndDate);

            var ordersCount = await AsyncExecuter.CountAsync(query2);
            query2 = query2.OrderByDescending(t => t.CreationTime).PageBy(input.SkipCount, input.MaxResultCount);
            var ordersList = await AsyncExecuter.ToListAsync(query2);

            var subOrders = await _subOrderRepository.GetListAsync(t => ordersList.Select(s => s.Id).Contains(t.OrderId));
            var d3OrderExtras = await _orderThreeDItemRepository.GetListAsync(t => subOrders.Select(s => s.Id).Contains(t.SubOrderId));
            var customerOrderList = new List<CustomerOrderListDto>();

            foreach (var item in ordersList)
            {
                var bomList = new List<CustomerSubOrderThreeDItemDto>();
                var d3OrderExtraList = d3OrderExtras.Where(t => t.SubOrderId == subOrders.Where(t => t.OrderId == item.Id).First().Id);

                foreach (var s in d3OrderExtraList)
                {
                    var customer3DOrderExtraBom = new CustomerSubOrderThreeDItemDto()
                    {
                        FileName = s.FileName,
                        FilePath = s.FilePath,
                        Thumbnail = s.Thumbnail,
                        Color = s.Color,
                        MaterialId = s.MaterialId,
                        MaterialName = s.MaterialName,
                        Count = s.Count,
                        Volume = s.Volume,
                        Size = s.Size,
                        HandleMethod = s.HandleMethod,
                        HandleMethodDesc = s.HandleMethodDesc,
                        OrginalMoney = s.OrginalMoney,
                        DeliveryDays = s.DeliveryDays,
                        SupplierFileId = s.SupplierFileId,
                        SupplierPreViewId = s.SupplierPreViewId
                    };
                    bomList.Add(customer3DOrderExtraBom);
                }
                var customerOrder = GetCustomerOrderDto(item);
                customerOrder.Customer3DOrderExtraBomDtos = bomList;
                customerOrderList.Add(customerOrder);
            }

            return new PagedResultDto<CustomerOrderListDto>(ordersCount, customerOrderList);
        }

        /// <summary>
        /// 获取用户Cnc订单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<CustomerOrderListDto>> GetCustomerCncOrderListAsync(GetCustomerCncOrderListInput input)
        {
            var query = await _orderRepository.GetQueryableAsync();
            var subOrder = await _subOrderRepository.GetQueryableAsync();
            var d3Order = await _orderCncItemRepository.GetQueryableAsync();

            var query2 = query
            .Where(t => t.ChannelId == input.ChannelId)
            .Where(t => t.OrderType == EnumOrderType.Cnc)
            .Where(t => t.ChannelUserId == CurrentUser.Id.ToString())
            .WhereIf(!input.OrderNo.IsNullOrEmpty(), t => t.OrderNo == input.OrderNo)
            .WhereIf(input.Status.HasValue, t => t.Status == input.Status)
            .WhereIf(input.StartDate.HasValue, t => t.CreationTime > input.StartDate)
            .WhereIf(input.EndDate.HasValue, t => t.CreationTime < input.EndDate);

            var ordersCount = await AsyncExecuter.CountAsync(query2);
            query2 = query2.OrderByDescending(t => t.CreationTime).PageBy(input.SkipCount, input.MaxResultCount);
            var ordersList = await AsyncExecuter.ToListAsync(query2);

            var subOrders = await _subOrderRepository.GetListAsync(t => ordersList.Select(s => s.Id).Contains(t.OrderId));
            var cncOrderExtras = await _orderCncItemRepository.GetListAsync(t => subOrders.Select(s => s.Id).Contains(t.SubOrderId));
            var customerOrderList = new List<CustomerOrderListDto>();

            foreach (var item in ordersList)
            {
                var bomList = new List<CustomerSubOrderCncItemDto>();
                var cncExtraList = cncOrderExtras.Where(t => t.SubOrderId == subOrders.Where(t => t.OrderId == item.Id).First().Id);

                foreach (var s in cncExtraList)
                {
                    var customerCncOrderExtraBom = new CustomerSubOrderCncItemDto()
                    {
                        FileName = s.FileName,
                        FilePath = s.FilePath,
                        SubOrderId = s.SubOrderId,
                        Surface = s.Surface,
                        SurfaceName = s.SurfaceName,
                        MaterialName = s.MaterialName,
                        Material = s.Material,
                        Picture = s.Picture,
                        Size = s.Size,
                        ApplicationArea = s.ApplicationArea,
                        ProductName = s.ProductName,
                        Quantity = s.Quantity,
                    };
                    bomList.Add(customerCncOrderExtraBom);
                }
                var customerOrder = GetCustomerOrderDto(item);
                customerOrder.CustomerSubOrderCncItemDtos = bomList;
                customerOrderList.Add(customerOrder);
            }

            return new PagedResultDto<CustomerOrderListDto>(ordersCount, customerOrderList);
        }

        /// <summary>
        /// 获取用户钣金订单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<CustomerOrderListDto>> GetCustomerSheetMatelOrderListAsync(GetCustomerSheetMatelOrderListInput input)
        {
            var query = await _orderRepository.GetQueryableAsync();
            var subOrder = await _subOrderRepository.GetQueryableAsync();
            var sheetOrder = await _orderSheetMetalItemsRepository.GetQueryableAsync();

            var query2 = query
            .Where(t => t.ChannelId == input.ChannelId)
            .Where(t => t.OrderType == EnumOrderType.SheetMetal)
            .Where(t => t.ChannelUserId == CurrentUser.Id.ToString())
            .WhereIf(!input.OrderNo.IsNullOrEmpty(), t => t.OrderNo == input.OrderNo)
            .WhereIf(input.Status.HasValue, t => t.Status == input.Status)
            .WhereIf(input.StartDate.HasValue, t => t.CreationTime > input.StartDate)
            .WhereIf(input.EndDate.HasValue, t => t.CreationTime < input.EndDate);

            var ordersCount = await AsyncExecuter.CountAsync(query2);
            query2 = query2.OrderByDescending(t => t.CreationTime).PageBy(input.SkipCount, input.MaxResultCount);
            var ordersList = await AsyncExecuter.ToListAsync(query2);

            var subOrders = await _subOrderRepository.GetListAsync(t => ordersList.Select(s => s.Id).Contains(t.OrderId));
            var sheetOrderExtras = await _orderSheetMetalItemsRepository.GetListAsync(t => subOrders.Select(s => s.Id).Contains(t.SubOrderId));
            var customerOrderList = new List<CustomerOrderListDto>();

            foreach (var item in ordersList)
            {
                var bomList = new List<CustomerSubOrderSheetMetalItemDto>();
                var sheetExtraList = sheetOrderExtras.Where(t => t.SubOrderId == subOrders.Where(t => t.OrderId == item.Id).First().Id);

                foreach (var s in sheetExtraList)
                {
                    var customerSheetMetalOrderExtraBom = new CustomerSubOrderSheetMetalItemDto()
                    {
                        FileName = s.FileName,
                        FilePath = s.FilePath,
                        SubOrderId = s.SubOrderId,
                        SupplierFileId = s.SupplierFileId,
                        SupplierPreViewId = s.SupplierPreViewId,
                        AssembleType = s.AssembleType,
                        NeedDesign = s.NeedDesign,
                        PreviewUrl = s.PreviewUrl,
                        ProcessParameters = s.ProcessParameters,
                        ProductNum = s.ProductNum,
                        PurchasedParts = s.PurchasedParts,
                        PurchasedPartsName = s.PurchasedParts.GetDescription(),
                        Thumbnail = s.Thumbnail,
                        MaterialName = s.MaterialName,
                        SurfaceProcess = s.SurfaceProcess,
                    };
                    bomList.Add(customerSheetMetalOrderExtraBom);
                }
                var customerOrder = GetCustomerOrderDto(item);
                customerOrder.CustomerSubOrderSheetMetalItemDtos = bomList;
                customerOrderList.Add(customerOrder);
            }

            return new PagedResultDto<CustomerOrderListDto>(ordersCount, customerOrderList);
        }


        /// <summary>
        ///会员中心-3d订单列表详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CustomerOrderDetialListDto> GetCustomer3DOrderDetailListAsync(Guid id)
        {
            var order = await _orderRepository.FindAsync(t => t.Id == id && t.ChannelUserId == CurrentUser.Id.ToString());
            var subOrders = await _subOrderRepository.GetListAsync(t => order.Id == t.OrderId);
            var d3OrderExtras = await _orderThreeDItemRepository.GetListAsync(t => subOrders.Select(s => s.Id).Contains(t.SubOrderId));
            var orderCosts = await _orderCostRepository.GetListAsync(t => order.OrderNo == t.OrderNo);
            var orderDeliverys = await _orderDeliveryRepository.GetListAsync(t => order.OrderNo == t.OrderNo);

            var subOrder = subOrders.Where(t => t.OrderId == order.Id).First();
            var d3OrderExtraData = d3OrderExtras.Where(t => t.SubOrderId == subOrder.Id)
                .Select(t => ObjectMapper.Map<SubOrderThreeDItem, CustomerSubOrderThreeDItemDto>(t)).ToList();
            var orderCostData = orderCosts.Where(t => t.OrderNo == order.OrderNo)
                .Select(t => ObjectMapper.Map<OrderCost, CostDto>(t)).ToList();
            var orderDeliveryData = orderDeliverys.Where(t => t.OrderNo == order.OrderNo)
                .Select(t => ObjectMapper.Map<OrderDelivery, DeliveryDto>(t)).ToList();
            var checkNoPassReason = "";
            if (order.Status == EnumOrderStatus.CheckedNoPass)
            {
                var subOrderFlowList = await _subOrderFlowsRepository.GetListAsync(t => t.OrderNo == subOrder.OrderNo && t.Type == EnumSubOrderFlowType.CheckNoPass);
                var subOrderFlow = subOrderFlowList.OrderByDescending(t => t.Id).First();
                checkNoPassReason = subOrderFlow?.Remark;
            }
            return GetCustomerOrderDetialListDto(
                order: order
                , orderCostData: orderCostData
                , d3OrderExtraData: d3OrderExtraData
                , sheetMetalOrderExtraData: null
                , cncOrderExtraData: null
                , orderDeliveryData: orderDeliveryData
                , checkNoPassReason: checkNoPassReason);
        }

        /// <summary>
        ///会员中心-钣金订单列表详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CustomerOrderDetialListDto> GetCustomerSheetMetalOrderDetailListAsync(Guid id)
        {
            var order = await _orderRepository.FindAsync(t => t.Id == id && t.ChannelUserId == CurrentUser.Id.ToString());
            var subOrders = await _subOrderRepository.GetListAsync(t => order.Id == t.OrderId);
            var sheetMetalOrderExtras = await _orderSheetMetalItemsRepository.GetListAsync(t => subOrders.Select(s => s.Id).Contains(t.SubOrderId));
            var orderCosts = await _orderCostRepository.GetListAsync(t => order.OrderNo == t.OrderNo);
            var orderDeliverys = await _orderDeliveryRepository.GetListAsync(t => order.OrderNo == t.OrderNo);

            var subOrder = subOrders.Where(t => t.OrderId == order.Id).First();
            var sheetMetalOrderExtraData = sheetMetalOrderExtras.Where(t => t.SubOrderId == subOrder.Id)
                .Select(t => ObjectMapper.Map<SubOrderSheetMetalItem, CustomerSubOrderSheetMetalItemDto>(t)).ToList();
            var orderCostData = orderCosts.Where(t => t.OrderNo == order.OrderNo)
                .Select(t => ObjectMapper.Map<OrderCost, CostDto>(t)).ToList();
            var orderDeliveryData = orderDeliverys.Where(t => t.OrderNo == order.OrderNo)
                .Select(t => ObjectMapper.Map<OrderDelivery, DeliveryDto>(t)).ToList();
            //var checkNoPassReason = "";
            //if (order.Status == EnumOrderStatus.CheckedNoPass)
            //{
            //    var subOrderFlowList = await _subOrderFlowsRepository.GetListAsync(t => t.OrderNo == subOrder.OrderNo && t.Type == EnumSubOrderFlowType.CheckNoPass);
            //    var subOrderFlow = subOrderFlowList.OrderByDescending(t => t.Id).First();
            //    checkNoPassReason = subOrderFlow?.Remark;
            //}
            return GetCustomerOrderDetialListDto(
            order: order
            , orderCostData: orderCostData
            , d3OrderExtraData: null
            , sheetMetalOrderExtraData: sheetMetalOrderExtraData
            , cncOrderExtraData: null
            , orderDeliveryData: orderDeliveryData
            , checkNoPassReason: ""); //checkNoPassReason);
        }


        /// <summary>
        ///会员中心-Cnc订单列表详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CustomerOrderDetialListDto> GetCustomerCncOrderDetailListAsync(Guid id)
        {
            var order = await _orderRepository.FindAsync(t => t.Id == id && t.ChannelUserId == CurrentUser.Id.ToString());
            var subOrders = await _subOrderRepository.GetListAsync(t => order.Id == t.OrderId);
            var cncOrderExtras = await _orderCncItemRepository.GetListAsync(t => subOrders.Select(s => s.Id).Contains(t.SubOrderId));
            var orderCosts = await _orderCostRepository.GetListAsync(t => order.OrderNo == t.OrderNo);
            var orderDeliverys = await _orderDeliveryRepository.GetListAsync(t => order.OrderNo == t.OrderNo);

            var subOrder = subOrders.Where(t => t.OrderId == order.Id).First();
            var cncOrderExtraData = cncOrderExtras.Where(t => t.SubOrderId == subOrder.Id)
                .Select(t => ObjectMapper.Map<SubOrderCncItem, CustomerSubOrderCncItemDto>(t)).ToList();
            var orderCostData = orderCosts.Where(t => t.OrderNo == order.OrderNo)
                .Select(t => ObjectMapper.Map<OrderCost, CostDto>(t)).ToList();
            var orderDeliveryData = orderDeliverys.Where(t => t.OrderNo == order.OrderNo)
                .Select(t => ObjectMapper.Map<OrderDelivery, DeliveryDto>(t)).ToList();
            var checkNoPassReason = "";
            if (order.Status == EnumOrderStatus.CheckedNoPass)
            {
                var subOrderFlowList = await _subOrderFlowsRepository.GetListAsync(t => t.OrderNo == subOrder.OrderNo && t.Type == EnumSubOrderFlowType.CheckNoPass);
                var subOrderFlow = subOrderFlowList.OrderByDescending(t => t.Id).First();
                checkNoPassReason = subOrderFlow?.Remark;
            }
            return GetCustomerOrderDetialListDto(
            order: order
            , orderCostData: orderCostData
            , d3OrderExtraData: null
            , sheetMetalOrderExtraData: null
            , cncOrderExtraData: cncOrderExtraData
            , orderDeliveryData: orderDeliveryData
            , checkNoPassReason: checkNoPassReason);
        }

        /// <summary>
        /// 我的订单-用户3d订单数量统计
        /// </summary>
        /// <returns></returns>
        public async Task<CustomerOrderCountDto> GetCustomerOrderCountAsync(EnumOrderType enumOrderType)
        {
            var orders = await _orderRepository.GetListAsync(t =>
                t.ChannelUserId == CurrentUser.Id.ToString()
                && t.OrderType == enumOrderType);
            var total = orders.Count;
            var Pending = orders.Where(t =>
                   t.Status == EnumOrderStatus.CheckedPass
                   || t.Status == EnumOrderStatus.HaveSend).Count();
            var threeOrderCount = new CustomerOrderCountDto()
            {
                Total = total,
                PendingSum = Pending,
                ProcessedSum = total - Pending
            };
            return threeOrderCount;
        }

  
            /// <summary>
            /// 取消订单
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public async Task CancelAsync(Guid id)
        {
            var subOrder = await _subOrderRepository.FindAsync(t => t.OrderId == id);

            switch (subOrder.OrderType)
            {
                case EnumOrderType.Cnc:
                    if (subOrder.Status <= EnumSubOrderStatus.OfferComplete
                        && subOrder.Status != EnumSubOrderStatus.CheckedNoPass
                        )
                    {
                        await _subOrderAppService.CancelAsync(subOrder.Id, new CancelInput() { Rremark = "取消订单（客户自主取消）" });
                    }
                    break;
                case EnumOrderType.Print3D:
                    if (subOrder.Status < EnumSubOrderStatus.Cancel)
                    {
                        if (subOrder.Status != EnumSubOrderStatus.WaitCheck && subOrder.Status != EnumSubOrderStatus.CheckedNoPass)
                        {
                            var d3Material = await _orderThreeDItemRepository.GetListAsync(t => t.SubOrderId == subOrder.Id);
                            foreach (var item in d3Material.Select(t => t.SupplierOrderCode).Distinct())
                            {
                                var closeOrder = new CloseOrderRequest(item);
                                await _unionfabAppService.CloseAsync(closeOrder);
                            }
                        }
                        await _subOrderManager.CancelAsync(subOrder.Id, "取消订单（客户自主取消）");
                    }
                    break;
                case EnumOrderType.SheetMetal:
                    if (subOrder.Status <= EnumSubOrderStatus.OfferComplete
                        && subOrder.Status != EnumSubOrderStatus.CheckedNoPass
                    )
                    {
                        await _subOrderAppService.CancelAsync(subOrder.Id, new CancelInput() { Rremark = "取消订单（客户自主取消）" });
                    }
                    break;
            }

        }


        /// <summary>
        /// 收货订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task CompleteAsync(Guid id)
        {
            var subOrder = await _subOrderRepository.GetAsync(t => t.OrderId == id);
            switch (subOrder.OrderType)
            {
                case EnumOrderType.Cnc:
                    await _subOrderAppService.CompleteAsync(subOrder.Id, new CompleteInput() { Remark = "完成订单（客户收货）" });
                    break;
                case EnumOrderType.Print3D:
                    await _subOrderManager.CompleteAsync(subOrder.Id, "完成订单（客户收货）");
                    break;
                case EnumOrderType.SheetMetal:
                    await _subOrderAppService.CompleteAsync(subOrder.Id, new CompleteInput() { Remark = "完成订单（客户收货）" });
                    break;
            }
        }

        /// <summary>
        /// 修改订单(重新上传文件)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOrderFileAsync(UpdateOrderInput input)
        {
            return await _subOrderAppService.UpdateOrderFileAsync(input);
        }

        [AllowAnonymous]
        public async Task PayNotifyAsync(string orderNo)
        {
            var order = await _orderRepository.FindAsync(t => t.OrderNo == orderNo);
            if (order.OrderType == EnumOrderType.SheetMetal || order.OrderType == EnumOrderType.Cnc)
            {
                var input = new UpdateOrderPayStatusInput()
                {
                    IsPay = true
                };
                var orderDto = ObjectMapper.Map<Order, OrderDto>(order);
                await _subOrderAppService.PaymentAsync(orderDto);
            }
        }

        /// <summary>
        /// 订单支付确认
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        //private async Task<ApiHttpResponseDto> ModifyOrderPayStatusAsync(OrderDto order)
        //{
        //    //var updateOrderPayStatusDto = ObjectMapper.Map<UpdateOrderPayStatusInput, UpdateOrderPayStatusDto>(input);
        //    //updateOrderPayStatusDto.GroupNo = orderNo;

        //    var orderExternal = await _subOrderAppService.PaymentAsync(order);
        //    var apiHttpResponse = ObjectMapper.Map<ApiHttpResponse, ApiHttpResponseDto>(orderExternal);
        //    return apiHttpResponse;
        //}

        #region private
        private async Task<(OrderCost, OrderDelivery)> GetOrderExtersAsync(string orderNo)
        {
            var deliveryEntity = await _orderDeliveryRepository.FindAsync(t => t.OrderNo == orderNo);
            var costEntity = await _orderCostRepository.FindAsync(t => t.OrderNo == orderNo);
            return (costEntity, deliveryEntity);
        }

        private async Task<OrderDetailDto> GetOrderDetailAsync<T>(Order order, T subOrder)
        {
            var orderExters = await GetOrderExtersAsync(order.OrderNo);
            var result = new OrderDetailDto
            {
                Order = ObjectMapper.Map<Order, OrderDto>(order),
                SubOrder = subOrder,
                DeliveryInfo = ObjectMapper.Map<OrderDelivery, DeliveryDto>(orderExters.Item2),
                CostInfo = ObjectMapper.Map<OrderCost, CostDto>(orderExters.Item1)
            };
            return result;
        }

        private async Task<List<OrderDelivery>> GetOrderDeliverysAsync(List<string> orderNos)
        {
            var query = await _orderDeliveryRepository.GetQueryableAsync();
            query = query.WhereIf((orderNos?.Count ?? 0) > 0, t => orderNos.Contains(t.OrderNo));
            var entities = await AsyncExecuter.ToListAsync(query);
            return entities;
        }

        private CustomerOrderDetialListDto GetCustomerOrderDetialListDto(
              Order order
            , List<CostDto> orderCostData
            , List<CustomerSubOrderThreeDItemDto> d3OrderExtraData
            , List<CustomerSubOrderSheetMetalItemDto> sheetMetalOrderExtraData
            , List<CustomerSubOrderCncItemDto> cncOrderExtraData
            , List<DeliveryDto> orderDeliveryData
            , string checkNoPassReason)
        {
            return new CustomerOrderDetialListDto()
            {
                OrderId = order.Id,
                OrderNo = order.OrderNo,
                Status = order.Status,
                StatusName = order.Status.GetDescription(),
                SellingPrice = order.SellingPrice,
                CreationTime = order.CreationTime,
                CostDto = orderCostData,
                OrderName = order.OrderName,
                CustomerRemark = order.CustomerRemark,
                Customer3DOrderExtraBomDtos = d3OrderExtraData,
                CustomerSheetMetalOrderDtos = sheetMetalOrderExtraData,
                CustomerCncOrderDtos = cncOrderExtraData,
                DeliveryDate = order.DeliveryDate,
                DeliveryDays = order.DeliveryDays,
                DeliveryDto = orderDeliveryData,
                CheckNoPassReason = checkNoPassReason
            };
        }
        private CustomerOrderListDto GetCustomerOrderDto(Order order)
        {
            return new CustomerOrderListDto
            {
                OrderId = order.Id,
                OrderNo = order.OrderNo,
                CreationTime = order.CreationTime,
                Status = order.Status,
                StatusName = order.Status.GetDescription(),
                SellingPrice = order.SellingPrice,
                DeliveryDate = order.DeliveryDate,
                DeliveryDays = order.DeliveryDays,
                OrderName = order.OrderName,
                IsPay = order.IsPay,
                Customer3DOrderExtraBomDtos = null,
                CustomerSubOrderCncItemDtos = null,
                CustomerSubOrderSheetMetalItemDtos = null
            };
        }
        #endregion



    }
}
