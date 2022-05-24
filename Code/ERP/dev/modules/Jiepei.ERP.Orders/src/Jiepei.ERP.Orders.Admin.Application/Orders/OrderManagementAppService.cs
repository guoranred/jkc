using Jiepei.BizMO.DeliverCenters.Orders.Orders.Dtos;
using Jiepei.ERP.DeliverCentersClient.DeliverCenterClients;
using Jiepei.ERP.Members.Admin;
using Jiepei.ERP.Members.Admin.CustomerServices;
using Jiepei.ERP.Orders.Admin.Application.Contracts.Orders.Dtos;
using Jiepei.ERP.Orders.Admin.Orders;
using Jiepei.ERP.Orders.Materials;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Orders.Pays;
using Jiepei.ERP.Orders.SubOrders;
using Jiepei.ERP.Orders.SubOrders.Dtos;
using Jiepei.ERP.Shared.Enums.Pays;
using Jiepei.ERP.SubOrders;
using Jiepei.ERP.Suppliers;
using Jiepei.ERP.Suppliers.Suppliers;
using Jiepei.ERP.Suppliers.Unionfab.Services.Order;
using Jiepei.ERP.Utilities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;

namespace Jiepei.ERP.Orders.Admin
{
    /// <summary>
    /// 订单管理
    /// </summary>
    //[Authorize(OrdersPermissions.Orders.Default)]
    [Authorize()]
    public partial class OrderManagementAppService : OrdersAdminAppServiceBase, IOrderManagementAppService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ISubOrderRepository _subOrderRepository;
        private readonly ISubOrderManager _subOrderManager;
        private readonly ISubOrderMoldItemRepository _orderMoldItemRepository;
        private readonly ISubOrderInjectionItemRepository _orderInjectionItemRepository;
        private readonly ISubOrderCncItemRepository _orderCncItemRepository;
        private readonly IRepository<MaterialSupplier, Guid> _materialSupplierRepository;
        private readonly ISubOrderThreeDItemRepository _orderThreeDItemRepository;
        private readonly IRepository<SubOrderFlow, Guid> _subOrderFlowRepository;
        private readonly IRepository<OrderDelivery, Guid> _orderDeliveryRepository;
        private readonly IRepository<OrderLog, Guid> _orderLogRepository;
        private readonly ISupplierUnionfabAppService _unionfabAppService;
        private readonly IMemberManagementAppService _memberManagementAppService;
        private readonly IRepository<OrderCost, Guid> _orderCostRepository;
        private readonly ISupplierAppService _supplierAppService;
        private readonly ISubOrderSheetMetalItemRepository _orderSheetMetalItemRepository;
        private readonly ISheetMetalOrderGroupService _sheetMetalOrderGroupService;
        private readonly IConsultantServiceManagementAppService _consultantServiceManagementAppService;
        private readonly IOrderPayDetailLogRepository _orderPayDetailLogRepository;
        private readonly IOrderPayLogRepository _orderPayLogRepostiroy;
        private readonly IDataFilter _dataFilter;
        private readonly ISheetMetalApi _sheetMetalApi;
        private readonly ICncApi _cncApi;
        private readonly ILocalEventBus _localEventBus;

        public OrderManagementAppService(IOrderRepository orderRepository,
                                         ISubOrderRepository subOrderRepository,
                                         ISubOrderManager subOrderManager,
                                         ISubOrderMoldItemRepository orderMoldItemRepository,
                                         ISubOrderInjectionItemRepository orderInjectionItemRepository,
                                         ISubOrderCncItemRepository orderCncItemRepository,
                                         ISubOrderThreeDItemRepository orderThreeDItemRepository,
                                         ISupplierUnionfabAppService unionfabAppService,
                                         IRepository<MaterialSupplier, Guid> materialSupplierRepository,
                                         IMemberManagementAppService managementAppService,
                                         IRepository<SubOrderFlow, Guid> subOrderFlowRepository,
                                         IRepository<OrderDelivery, Guid> orderDeliverieRepository,
                                         IRepository<OrderLog, Guid> orderLogRepository,
                                         IRepository<OrderCost, Guid> orderCostRepository,
                                         ISupplierAppService supplierAppService,
                                         ISubOrderSheetMetalItemRepository orderSheetMetalItemRepository,
                                         ISheetMetalOrderGroupService sheetMetalOrderGroupService,
                                         IConsultantServiceManagementAppService consultantServiceManagementAppService,
                                         IDataFilter dataFilter,
                                         IOrderPayDetailLogRepository orderPayDetailLogRepository,
                                         IOrderPayLogRepository orderPayLogRepostiroy,
                                         ISheetMetalApi sheetMetalApi,
                                         ICncApi cncApi, 
                                         ILocalEventBus localEventBus)
        {
            _orderRepository = orderRepository;
            _subOrderRepository = subOrderRepository;
            _subOrderManager = subOrderManager;
            _orderMoldItemRepository = orderMoldItemRepository;
            _orderInjectionItemRepository = orderInjectionItemRepository;
            _orderCncItemRepository = orderCncItemRepository;
            _orderThreeDItemRepository = orderThreeDItemRepository;
            _unionfabAppService = unionfabAppService;
            _materialSupplierRepository = materialSupplierRepository;
            _memberManagementAppService = managementAppService;
            _subOrderFlowRepository = subOrderFlowRepository;
            _orderDeliveryRepository = orderDeliverieRepository;
            _orderLogRepository = orderLogRepository;
            _orderCostRepository = orderCostRepository;
            _supplierAppService = supplierAppService;
            _orderSheetMetalItemRepository = orderSheetMetalItemRepository;
            _sheetMetalOrderGroupService = sheetMetalOrderGroupService;
            _consultantServiceManagementAppService = consultantServiceManagementAppService;
            _dataFilter = dataFilter;
            _orderPayDetailLogRepository = orderPayDetailLogRepository;
            _orderPayLogRepostiroy = orderPayLogRepostiroy;
            _sheetMetalApi = sheetMetalApi;
            _cncApi = cncApi;
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 客服订单详情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        //[Authorize(OrdersPermissions.Orders.Detail)]
        public async Task<CustomerServiceOrderDetailDto> GetCustomerServiceOrderDetail(Guid id, EnumOrderType orderType)
        {
            var strbom = await GetBoms(id, orderType);

            var subOrder = await _subOrderRepository.GetAsync(id);

            var member = await _memberManagementAppService.GetAsync(Guid.Parse(subOrder.ChannelUserId));

            //     var bomDtos = new List<OrderItemDto>();

            var subOrderFlow = await _subOrderFlowRepository.FindAsync(t => t.OrderNo == subOrder.OrderNo && t.Type == EnumSubOrderFlowType.Complete);

            var order = await _orderRepository.FindAsync(t => t.Id == subOrder.OrderId);

            var orderCost = await _orderCostRepository.FindAsync(t => t.OrderNo == order.OrderNo);

            var delivery = await _orderDeliveryRepository.FindAsync(t => t.OrderNo == order.OrderNo);

            var customerService = new CustomerServiceDto();

            if (order.CustomerService != null)
            {
                customerService = await _consultantServiceManagementAppService.GetAsync((Guid)order.CustomerService);
            }

            return GetCustomerServiceOrderDetailDto(order, subOrder, orderCost, subOrderFlow, strbom, member, delivery, customerService);
        }

        private async Task<string> GetBoms(Guid id, EnumOrderType type)
        {
            var strbom = "";
            switch (type)
            {
                case EnumOrderType.Cnc:
                    var cncBoms = await _orderCncItemRepository.GetListAsync(t => t.SubOrderId == id);
                    var cncBomDtos = new List<SubOrderCncItemDto>();
                    foreach (var item in cncBoms)
                    {
                        cncBomDtos.Add(ObjectMapper.Map<SubOrderCncItem, SubOrderCncItemDto>(item));
                    }
                    // var cncBomDtos = ObjectMapper.Map<List<SubOrderCncItem>, List<SubOrderCncItemDto>>(cncBoms);
                    return strbom = System.Text.Json.JsonSerializer.Serialize(cncBomDtos);

                case EnumOrderType.Print3D:
                    var threedBoms = await _orderThreeDItemRepository.GetListAsync(t => t.SubOrderId == id);
                    var threedBomDtos = new List<OrderItemDto>();
                    foreach (var item in threedBoms)
                    {
                        threedBomDtos.Add(ObjectMapper.Map<SubOrderThreeDItem, OrderItemDto>(item));
                    }
                    //var threedBomDtos = ObjectMapper.Map<List<SubOrderThreeDItem>, List<OrderItemDto>>(threedBoms);
                    return strbom = System.Text.Json.JsonSerializer.Serialize(threedBomDtos);

                case EnumOrderType.SheetMetal:
                    var sheetMetalBoms = await _orderSheetMetalItemRepository.GetListAsync(t => t.SubOrderId == id);
                    var sheetMetalBomDtos = new List<SubOrderSheetMetalItemDto>();
                    foreach (var item in sheetMetalBoms)
                    {
                        sheetMetalBomDtos.Add(ObjectMapper.Map<SubOrderSheetMetalItem, SubOrderSheetMetalItemDto>(item));
                    }
                    //  var sheetMetalBomDtos = ObjectMapper.Map<List<SubOrderSheetMetalItem>, List<SubOrderSheetMetalItemDto>>(sheetMetalBoms);
                    return strbom = System.Text.Json.JsonSerializer.Serialize(sheetMetalBomDtos);

                default:
                    return "";
            }
        }

        /// <summary>
        /// 客服订单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[Authorize(OrdersPermissions.Orders.Search)]
        public async Task<PagedResultDto<CustomerServiceOrderDto>> GetCustomerServiceOrderListAsync(GetCustomerServiceOrderInput input)
        {
            var orders = await _orderRepository.GetQueryableAsync();
            var subOrders = await _subOrderRepository.GetQueryableAsync();
            var payDetailLogs = await _orderPayDetailLogRepository.GetQueryableAsync();
            var payLogs = await _orderPayLogRepostiroy.GetQueryableAsync();

            //var query = orders.Join(subOrders,
            //            order => order.Id,
            //            subOrder => subOrder.OrderId,
            //            (order, subOrder) => new OrderAndSubOrder { SubOrder = subOrder, Order = order });

            var query = from order in orders
                        join subOrder in subOrders on order.Id equals subOrder.OrderId
                        join payDetailLog in payDetailLogs.Where(t => t.IsSuccess == true) on order.OrderNo equals payDetailLog.OrderNo into orderPayDetailLogs
                        from payDetailLog in orderPayDetailLogs.DefaultIfEmpty()
                        join payLog in payLogs.Where(t => t.IsPaySuccess == true) on payDetailLog.PayLogId equals payLog.Id into payLogPayDetailLogs
                        from payLog in payLogPayDetailLogs.DefaultIfEmpty()
                        select new OrderAndSubOrder { SubOrder = subOrder, Order = order, PayLog = payLog };

            query = CreateFilteredQuery(query, input);

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);

            query = ApplyPaging(query, input);

            var result = await AsyncExecuter.ToListAsync(query);
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var members = await _memberManagementAppService
                .GetListByIdsAsync(result.Select(t => Guid.Parse(t.SubOrder.ChannelUserId)).Distinct().ToArray());

                var moldItems = await GetMoldItemsAsync(result);
                var injectionItems = await GetInjectionItemsAsync(result);
                var cncItems = await GetCncItemsAsync(result);
                var threedItems = await GetThreeDItemsAsync(result);
                var sheetMetalItems = await GetSheetMetalItemsAsync(result);

                var listDtos = new List<CustomerServiceOrderDto>();

                foreach (var item in result)
                {
                    var orderDto = BuildCustomerServiceOrderDto(item, members);
                    orderDto.OrderItems = GetOrderItems(item.SubOrder.Id, item.SubOrder.OrderType, moldItems, injectionItems, cncItems, threedItems, sheetMetalItems);
                    orderDto.OrderItems
                        .Select(t => t.SupplierOrderCode ?? "")
                        .Distinct()
                        .ToList()
                        .ForEach(t => orderDto.SupplierOrderCodes += t + ",");
                    orderDto.SupplierOrderCodes = orderDto.SupplierOrderCodes?.Remove(orderDto.SupplierOrderCodes.Length - 1);
                    listDtos.Add(orderDto);
                }

                return new PagedResultDto<CustomerServiceOrderDto>(totalCount, listDtos);
            }
        }

        //[Authorize(OrdersPermissions.Orders.Search)]
        public async Task<PagedResultDto<CustomerServiceOrderDto>> GetOrdersBySupplierCode(string supplierOrderCode)
        {
            var items = await _orderThreeDItemRepository.GetListAsync(t => t.SupplierOrderCode == supplierOrderCode);
            if (!items.Any())
                throw new UserFriendlyException("生产编号 {supplierOrderCode} 不存在");

            return await GetCustomerServiceOrderListAsync(new GetCustomerServiceOrderInput { Id = items[0].SubOrderId });
        }


        /// <summary>
        /// 查询子订单
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<SubOrderDto> GetSubOrderByOrderNo(string orderNo)
        {
            var order = await _orderRepository.FindAsync(t => t.OrderNo == orderNo);
            var subOrder = await _subOrderRepository.FindAsync(t => t.OrderId == order.Id);
            return ObjectMapper.Map<SubOrder, SubOrderDto>(subOrder);
        }

        #region 操作

        /// <summary>
        ///  编辑客服备注
        /// </summary>
        /// <param name="id">订单 Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        //[Authorize(OrdersPermissions.Orders.EditNote)]
        public async Task<object> EditNoteAsync(Guid id, EditNoteInput input)
        {
            var order = await _orderRepository.GetAsync(t => t.Id == id);
            order.SetNote(input.Note);
            var result = await _orderRepository.UpdateAsync(order);
            return new { order.Id, order.CustomerServiceRemark };
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="id">子订单Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        //[Authorize(OrdersPermissions.Orders.Check)]
        public async Task CheckAsync(Guid id, CheckInput input)
        {
            var subOrder = await _subOrderRepository.GetAsync(t => t.Id == id);

            await CheckAsync(subOrder, input);

            var supplier = await _supplierAppService.GetByIdAsync(input.SupplierId);
            if (input.IsPassed && supplier.Name == "优联")
                await SubmitUnionfabOrderAsync(subOrder.Id, input);
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="orderNo">子订单号</param>
        /// <param name="input"><see cref="CheckInput"/></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task CheckAsync(string orderNo, CheckInput input)
        {
            var subOrder = await _subOrderRepository.FindAsync(t => t.OrderNo == orderNo);
            await CheckAsync(subOrder, input);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="id">主订单 Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        //[Authorize(OrdersPermissions.Orders.Cancel)]
        public async Task CancelAsync(Guid id, CancelInput input)
        {
            //await _subOrderManager.CancelAsync(id, input.Rremark);

            //var subOrder = await _subOrderRepository.FindAsync(t => t.OrderId == id);
            //var d3Material = await _orderThreeDItemRepository.GetListAsync(t => t.SubOrderId == subOrder.Id);
            //foreach (var item in d3Material.Select(t => t.SupplierOrderCode).Distinct())
            //{
            //    var closeOrder = new CloseOrderRequest(item);
            //    await _unionfabAppService.CloseAsync(closeOrder);
            //}

            var subOrder = await _subOrderRepository.FindAsync(t => t.Id == id);
            if (subOrder.Status != EnumSubOrderStatus.WaitCheck && subOrder.Status < EnumSubOrderStatus.Cancel)
            {
                var threedBoms = await _orderThreeDItemRepository.GetListAsync(t => t.SubOrderId == id);
                var supplierList = await _supplierAppService.GetListAsync(threedBoms.Select(t => t.SupplierId).ToList()); ;
                foreach (var item in threedBoms.Select(t => new { t.SupplierId, t.SupplierOrderCode }).Distinct())
                {
                    var supplier = supplierList?.FirstOrDefault(x => x.Id == item.SupplierId);
                    if (supplier.IsEnable && supplier.Name == "优联")
                    {
                        var closeOrder = new CloseOrderRequest(item.SupplierOrderCode);
                        await _unionfabAppService.CloseAsync(closeOrder);
                    }
                }
            }
            //await _subOrderManager.CancelAsync(id, input.Rremark);

            await CancelAsync(subOrder, input);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderNo">子订单号</param>
        /// <param name="input"><see cref="CancelInput"/></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task CancelAsync(string orderNo, CancelInput input)
        {
            var subOrder = await _subOrderRepository.FindAsync(t => t.OrderNo == orderNo);

            await CancelAsync(subOrder, input);
        }

        /// <summary>
        /// 报价
        /// </summary>
        /// <param name="id">子订单 Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        //[Authorize(OrdersPermissions.Orders.Offer)]
        public async Task OfferAsync(Guid id, OfferInput input)
        {
            var subOrder = await _subOrderRepository.FindAsync(t => t.Id == id);

            await OfferAsync(subOrder, input);
        }

        /// <summary>
        /// 报价
        /// </summary>
        /// <param name="orderNo">子订单号</param>
        /// <param name="input"><see cref="OfferInput"/></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task OfferAsync(string orderNo, OfferInput input)
        {
            var subOrder = await _subOrderRepository.FindAsync(t => t.OrderNo == orderNo);

            await OfferAsync(subOrder, input);
        }

        /// <summary>
        /// 投产
        /// </summary>
        /// <param name="id">子订单 Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        //[Authorize(OrdersPermissions.Orders.Manufacture)]
        public async Task ManufactureAsync(Guid id, ManufactureInput input)
        {
            var subOrder = await _subOrderRepository.FindAsync(t => t.Id == id);
            if (subOrder == null)
                throw new UserFriendlyException("订单不存在");

            if (subOrder.Status == EnumSubOrderStatus.SureOrder)
            {
                var threedBoms = await _orderThreeDItemRepository.GetListAsync(t => t.SubOrderId == id);
                var supplierList = await _supplierAppService.GetListAsync(threedBoms.Select(t => t.SupplierId).ToList()); ;
                foreach (var item in threedBoms.Select(t => new { t.SupplierId, t.SupplierOrderCode }).Distinct())
                {
                    var supplier = supplierList?.FirstOrDefault(x => x.Id == item.SupplierId);
                    if (supplier.IsEnable && supplier.Name == "优联")
                    {
                        var request = new ConfirmOrderRequest(item.SupplierOrderCode);
                        await _unionfabAppService.ConfirmAsync(request);
                    }
                }

                await ManufactureAsync(subOrder, input);
            }
            else
                throw new UserFriendlyException("订单状态不符合");
        }

        /// <summary>
        /// 投产
        /// </summary>
        /// <param name="orderNo">子订单号</param>
        /// <param name="input"><see cref="ManufactureInput"/></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task ManufactureAsync(string orderNo, ManufactureInput input)
        {
            var subOrder = await _subOrderRepository.FindAsync(t => t.OrderNo == orderNo);

            await ManufactureAsync(subOrder, input);
        }

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="orderNo">子订单号</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task DeliverAsync(string orderNo, DeliverDto input)
        {
            var subOrder = await _subOrderRepository.FindAsync(t => t.OrderNo == orderNo);

            await _subOrderManager.DeliverAsync(subOrder, input.TrackingNo, input.CourierCompany, input.Remark);
        }

        /// <summary>
        /// 订单完成
        /// </summary>
        /// <param name="id">子订单Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        //[Authorize(OrdersPermissions.Orders.Complete)]
        public async Task CompleteAsync(Guid id, CompleteInput input)
        {
            await _subOrderManager.CompleteAsync(id, input.Remark);
        }

        [AllowAnonymous]
        public async Task CompleteAsync(string orderNo)
        {
            await _subOrderManager.CompleteAsync(orderNo);
        }

        /// <summary>
        /// 设置交期
        /// </summary>
        /// <param name="orderNo">子订单号</param>
        /// <param name="input"><see cref="SetDeliveryDayDto"/></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        [AllowAnonymous]
        public async Task SetDeliveryDaysAsync(string orderNo, SetDeliveryDayDto input)
        {
            var subOrder = await _subOrderRepository.FindAsync(t => t.OrderNo == orderNo);
            if (subOrder == null)
                throw new UserFriendlyException("Oops!订单不存在");

            await _subOrderManager.SetDeliveryDaysAsync(subOrder, input.DeliveryDays);
        }

        #endregion 操作

        /// <summary>
        /// 获取订单操作记录
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <returns></returns>
        //[Authorize(OrdersPermissions.Orders.Detail)]
        public async Task<List<OrderLogDto>> GetOrderLogsAsync(string orderNo)
        {
            var subOrderEntities = await _subOrderRepository.FindAsync(t => t.OrderNo == orderNo);
            var orderEntities = await _orderRepository.FindAsync(t => t.Id == subOrderEntities.OrderId);
            var orderLogEntities = await _orderLogRepository.GetListAsync(t => t.OrderNo == orderEntities.OrderNo);

            var dtos = new List<OrderLogDto>();
            foreach (var entity in orderLogEntities ?? new List<OrderLog>())
            {
                dtos.Add(ObjectMapper.Map<OrderLog, OrderLogDto>(entity));
            }
            dtos = dtos.OrderByDescending(t => t.CreationTime).ToList();
            return dtos;
        }

        /// <summary>
        /// 获取供应商报价
        /// </summary>
        /// <param name="subOrderId"></param>
        /// <returns></returns>
        //[Authorize(OrdersPermissions.Orders.Detail)]
        public async Task GetSupplierCost(Guid subOrderId)
        {
            var d3orderExtra = await _orderThreeDItemRepository.GetListAsync(t => t.SubOrderId == subOrderId);
            //var supplierList = await _supplierAppService.GetListAsync(d3orderExtra.Select(t => t.SupplierOrderCode).ToList());
            //foreach (var item in d3orderExtra?.Distinct())
            //{
            //if (item.Name == "优联")
            //{
            var subOrder = await _subOrderRepository.FindAsync(t => t.Id == subOrderId);
            var cost = await GetCostAsync(d3orderExtra);
            await AddCostAsync(subOrder, cost);
            //}
            //}
        }

        public async Task PayNotifyAsync(string orderNo)
        {
            var input = new UpdateOrderPayStatusInput()
            {
                IsPay = true
            };
            var order = await _orderRepository.FindAsync(t => t.OrderNo == orderNo);
            await ModifyOrderPayStatusAsync(order.Id, input);
        }

        /// <summary>
        /// 修改用户收货信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task Orderdelivery(string orderNo, OrderDeliveryDto input)
        {
            var subOrder = await _subOrderRepository.FindAsync(t => t.OrderNo == orderNo);

            var orderDelivery = await _orderDeliveryRepository.FindAsync(t => t.OrderNo == orderNo);
            orderDelivery.SetDelivery(input.ReceiverName,
                         input.ReceiverCompany,
                         input.ProvinceCode,
                         input.ProvinceName,
                         input.CityCode,
                         input.CityName,
                         input.CountyCode,
                         input.CountyName,
                         input.ReceiverAddress,
                         input.ReceiverTel
                         );
            await _orderDeliveryRepository.UpdateAsync(orderDelivery);
        }

        #region 私有

        private async Task<List<SubOrderMoldItem>> GetMoldItemsAsync(List<OrderAndSubOrder> inputs)
        {
            var moldOrders = inputs.Select(t => t.SubOrder).Where(t => t.OrderType == EnumOrderType.Mold);
            return await _orderMoldItemRepository.GetListAsync(t => moldOrders.Select(i => i.Id).Contains(t.SubOrderId));
        }

        private async Task<List<SubOrderInjectionItem>> GetInjectionItemsAsync(List<OrderAndSubOrder> inputs)
        {
            var injectionOrders = inputs.Select(t => t.SubOrder).Where(t => t.OrderType == EnumOrderType.Injection);
            return await _orderInjectionItemRepository.GetListAsync(t => injectionOrders.Select(i => i.Id).Contains(t.SubOrderId));
        }

        private async Task<List<SubOrderCncItem>> GetCncItemsAsync(List<OrderAndSubOrder> inputs)
        {
            var cncOrders = inputs.Select(t => t.SubOrder).Where(t => t.OrderType == EnumOrderType.Cnc);
            return await _orderCncItemRepository.GetListAsync(t => cncOrders.Select(c => c.Id).Contains(t.SubOrderId));
        }

        private async Task<List<SubOrderSheetMetalItem>> GetSheetMetalItemsAsync(List<OrderAndSubOrder> inputs)
        {
            var sheetMetalOrders = inputs.Select(t => t.SubOrder).Where(t => t.OrderType == EnumOrderType.SheetMetal);
            return await _orderSheetMetalItemRepository.GetListAsync(t => sheetMetalOrders.Select(c => c.Id).Contains(t.SubOrderId));
        }

        private async Task<List<SubOrderThreeDItem>> GetThreeDItemsAsync(List<OrderAndSubOrder> inputs)
        {
            var threedOrders = inputs.Select(t => t.SubOrder).Where(t => t.OrderType == EnumOrderType.Print3D);
            return await _orderThreeDItemRepository.GetListAsync(t => threedOrders.Select(d => d.Id).Contains(t.SubOrderId));
        }

        private CustomerServiceOrderDto BuildCustomerServiceOrderDto(OrderAndSubOrder item, List<MemberInfomationDto> members)
        {
            var member = members.FirstOrDefault(t => t.Id == Guid.Parse(item.SubOrder.ChannelUserId));
            return new CustomerServiceOrderDto
            {
                Id = item.SubOrder.Id,
                OrderId = item.SubOrder.OrderId,
                OrderNo = item.SubOrder.OrderNo,
                OrderType = item.Order.OrderType,
                SellingPrice = item.Order.SellingPrice,
                Status = item.SubOrder.Status.GetDescription(),
                PayTime = item.Order.PayTime,
                CustomerServiceRemark = item.Order.CustomerServiceRemark,
                CreationTime = item.Order.CreationTime,
                CreatorId = item.Order.CreatorId,
                ChannelId = item.Order.ChannelId,
                OrderName = item.Order.OrderName,
                IsPay = item.Order.IsPay,
                DeliveryDate = item.Order.DeliveryDate,
                CustomerService = item.Order.CustomerService,
                MemberName = member?.Name,
                PhoneNumber = member?.PhoneNumber,
                PayType = item.PayLog == null ? "" : ((EnumPayType)item.PayLog.PayType).GetDescription(),
                PayCode = item.PayLog?.PayCode
            };
        }

        private List<OrderItemDto> GetOrderItems(Guid subOrderId,
                                                 EnumOrderType orderType,
                                                 List<SubOrderMoldItem> moldItems,
                                                 List<SubOrderInjectionItem> injectionItems,
                                                 List<SubOrderCncItem> cncItems,
                                                 List<SubOrderThreeDItem> threedItems,
                                                 List<SubOrderSheetMetalItem> sheetMetalItems)
        {
            var orderItems = new List<OrderItemDto>();

            if (orderType == EnumOrderType.Mold && moldItems.Any())
            {
                orderItems = moldItems.Where(t => t.SubOrderId == subOrderId)
                    .Select(t => ObjectMapper.Map<SubOrderMoldItem, OrderItemDto>(t))
                    .ToList();
            }
            if (orderType == EnumOrderType.Injection && injectionItems.Any())
            {
                orderItems = injectionItems.Where(t => t.SubOrderId == subOrderId)
                    .Select(t => ObjectMapper.Map<SubOrderInjectionItem, OrderItemDto>(t))
                    .ToList();
            }
            if (orderType == EnumOrderType.Cnc && cncItems.Any())
            {
                orderItems = cncItems.Where(t => t.SubOrderId == subOrderId)
                    .Select(t => ObjectMapper.Map<SubOrderCncItem, OrderItemDto>(t))
                    .ToList();
            }
            if (orderType == EnumOrderType.Print3D && threedItems.Any())
            {
                orderItems = threedItems.Where(t => t.SubOrderId == subOrderId)
                    .Select(t => ObjectMapper.Map<SubOrderThreeDItem, OrderItemDto>(t))
                    .ToList();
            }
            if (orderType == EnumOrderType.SheetMetal && sheetMetalItems.Any())
            {
                orderItems = sheetMetalItems.Where(t => t.SubOrderId == subOrderId)
                    .Select(t => ObjectMapper.Map<SubOrderSheetMetalItem, OrderItemDto>(t))
                    .ToList();
            }
            return orderItems;
        }

        private void BuildPrintFiles(SubOrderThreeDItem file, List<OpenOrderFileData> printFiles)
        {
            printFiles.Add(new OpenOrderFileData
            {
                Count = file.Count,
                FileId = file.SupplierFileId,
                HandleMethod = file.HandleMethod,
                HandleMethodDesc = file.HandleMethodDesc
                //PreViewId = file.SupplierPreViewId,
                //SupportVolume = file.SupportVolume,
                //Volume = file.Volume
            });
        }

        /// <summary>
        /// 订单提交到优联
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task SubmitUnionfabOrderAsync(Guid id, CheckInput input)
        {
            /*
             * 获取优联订单编号 ==> 上传文件 URL ==> 获取文件信息并更新 ==> 提交订单
             */
            var subOrder = await _subOrderManager.GetByIdAsync(id);

            var threedOrderItems = await _orderThreeDItemRepository.GetListAsync(t => t.SubOrderId == id);

            var materialSuppliers = await GetSpuIdListAsync(input.SupplierId, threedOrderItems.Select(t => t.MaterialId).ToList());

            // 生成订单编号
            var orderCode = (await _unionfabAppService.CreateCodeAsync()).Data;

            var orderItems = new List<OpenOrderItemData>();

            foreach (var item in materialSuppliers)
            {
                var printFiles = new List<OpenOrderFileData>();
                foreach (var orderItem in threedOrderItems)
                {
                    // 上传文件 URL
                    var fileResponse = await _unionfabAppService.CreateAsync(orderCode, BuildCreateFileRequest(orderItem));

                    orderItem.SupplierId = input.SupplierId;
                    orderItem.SupplierFileId = fileResponse.Data.FileId;
                    orderItem.SupplierOrderCode = orderCode;
                    orderItem.SupplierPreViewId = $"modelFileId={orderItem.SupplierFileId}&code={orderCode}";

                    if (orderItem.MaterialId == orderItem.MaterialId)
                    {
                        BuildPrintFiles(orderItem, printFiles);
                    }
                }
                BuildOrderItems(orderItems, printFiles, item.SupplierSpuId, subOrder.Remark);
            }

            // 订单交付信息
            var deliver = BuildOpenOrderDeliverData(subOrder);
            // 订单信息
            var openOrder = BuildOpenOrderData(orderItems, subOrder);
            // 提交订单
            await _unionfabAppService.CreateAsync(new CreateOrderRequest(deliver, openOrder, orderCode));

            //var cost = await GetCostAsync(threedOrderItems);
            //await AddCostAsync(subOrder, cost);

            // 更新 SupplierFileId、SupplierPreViewId、SupplierOrderCode
            await _orderThreeDItemRepository.UpdateManyAsync(threedOrderItems);
        }

        private void BuildOrderItems(List<OpenOrderItemData> orderItems, List<OpenOrderFileData> printFiles, string spuId, string remark)
        {
            orderItems.Add(new OpenOrderItemData
            {
                SpuId = spuId,
                PriceCorrection = 0m,
                PrintFiles = printFiles,
                Remark = remark
            });
        }

        private CreateFileRequestInput BuildCreateFileRequest(SubOrderThreeDItem file)
        {
            return new CreateFileRequestInput
            {
                Url = file.FilePath,
                Md5 = file.FileMD5,
                Name = file.FileName,
            };
        }

        private OpenOrderDeliverData BuildOpenOrderDeliverData(SubOrder subOrder)
        {
            return new OpenOrderDeliverData
            {
                // todo:移动到配置文件
                CustomerName = $"捷配-J{subOrder.OrderNo}A",
                RepresentativeName = "刘斌",
                RepresentativeContactInfo = "13758223479",

                ReceiverAddress = "浙江省杭州市下城区新天地尚座西楼1201 杭州捷配",
                Recipient = "刘斌",
                ContactInfo = "13758223479",

                ConsignorName = "",
                ExpressCompany = "",
                ExpressNumber = ""
            };
        }

        private OpenOrderData BuildOpenOrderData(List<OpenOrderItemData> orderItems, SubOrder subOrder)
        {
            return new OpenOrderData
            {
                Items = orderItems,
                Remark = subOrder.Remark,
                PayMethod = ""
            };
        }

        private async Task<List<MaterialSupplier>> GetSpuIdListAsync(Guid supplierId, List<Guid> materialIds)
        {
            var query = (await _materialSupplierRepository.GetQueryableAsync())
                        .Where(t => t.SupplierId == supplierId && materialIds.Contains(t.MaterialId));
            var result = await AsyncExecuter.ToListAsync(query);

            if (!result.Any())
                throw new UserFriendlyException($"供应商{supplierId}相关材料不存在");
            return result;
        }

        /// <summary>
        /// 子订单 Id
        /// </summary>
        /// <param name="threeDItems"></param>
        /// <returns></returns>
        private async Task<decimal> GetCostAsync(List<SubOrderThreeDItem> threeDItems)
        {
            var cost = 0m;
            foreach (var item in threeDItems.Select(t => t.SupplierOrderCode).Distinct())
            {
                var unionfabOrder = await _unionfabAppService.GetAsync(item);
                if (unionfabOrder != null)
                    cost += unionfabOrder.Data.Price.TotalPriceWithTax.HasValue ? unionfabOrder.Data.Price.TotalPriceWithTax.Value : 0;
            }
            return cost;
        }

        /// <summary>
        /// 添加成本
        /// </summary>
        /// <param name="subOrder"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        private async Task AddCostAsync(SubOrder subOrder, decimal cost)
        {
            var order = await _orderRepository.FindAsync(t => t.Id == subOrder.OrderId);
            order.Cost = cost;
            subOrder.Cost = cost;
            await _subOrderRepository.UpdateAsync(subOrder);
            await _orderRepository.UpdateAsync(order);
        }

        private IQueryable<OrderAndSubOrder> CreateFilteredQuery(IQueryable<OrderAndSubOrder> query, GetCustomerServiceOrderInput input)
        {
            return query.WhereIf(!input.OrderNo.IsNullOrEmpty(), t => t.Order.OrderNo.Contains(input.OrderNo.Trim())
                                                                                                || t.Order.ChannelOrderNo.Contains(input.OrderNo.Trim())
                                                                                                || t.SubOrder.OrderNo.Contains(input.OrderNo.Trim()))
            .WhereIf(input.Channel != default, t => t.Order.ChannelId == input.Channel)
            .WhereIf(input.IsPay.HasValue, t => t.Order.IsPay == input.IsPay)
            .WhereIf(input.StartPayDate.HasValue, t => t.Order.PayTime > input.StartPayDate)
            .WhereIf(input.EndPayDate.HasValue, t => t.Order.PayTime < input.EndPayDate)
            .WhereIf(input.StartCreateDate.HasValue, t => t.Order.CreationTime > input.StartCreateDate)
            .WhereIf(input.EndCreateDate.HasValue, t => t.Order.CreationTime < input.EndCreateDate)
            .WhereIf(input.Type.HasValue, t => t.SubOrder.OrderType == input.Type)
            .WhereIf(input.OrderStatus.HasValue, t => t.SubOrder.Status == input.OrderStatus)
            .WhereIf(input.Id != default, t => t.SubOrder.Id == input.Id)
            .WhereIf(!input.PayCode.IsNullOrEmpty(), t => t.PayLog.PayCode == input.PayCode)
            .WhereIf(input.PayType.HasValue, t => t.PayLog.PayType == (int)input.PayType);
            //.Where(x => x.PayLog.IsPaySuccess);
        }

        private IQueryable<OrderAndSubOrder> ApplySorting(IQueryable<OrderAndSubOrder> query, GetCustomerServiceOrderInput input)
        {
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                query = query.OrderBy(input.Sorting);
            }
            else
            {
                query = query.OrderByDescending(t => t.SubOrder.CreationTime);
            }
            return query;
        }

        private IQueryable<OrderAndSubOrder> ApplyPaging(IQueryable<OrderAndSubOrder> query, GetCustomerServiceOrderInput input)
        {
            return query.PageBy(input);
        }

        private class OrderAndSubOrder
        {
            public SubOrder SubOrder { get; set; }
            public Order Order { get; set; }
            public OrderPayLog PayLog { get; set; }
        }

        private CustomerServiceOrderDetailDto GetCustomerServiceOrderDetailDto(
            Order order,
            SubOrder subOrder,
            OrderCost orderCost,
            SubOrderFlow subOrderFlow,
            string orderItemDto,
            MemberInformationDetailDto member,
            OrderDelivery delivery,
            CustomerServiceDto customerServiceDto)
        {
            return new CustomerServiceOrderDetailDto
            {
                Id = subOrder.Id,
                OrderId = subOrder.OrderId,
                OrderNo = subOrder.OrderNo,
                Status = subOrder.Status.GetDescription(),
                CreationTime = subOrder.CreationTime,
                SellingPrice = subOrder.SellingPrice,
                ShipPrice = orderCost.ShipMoney,
                ProMoney = orderCost.ProMoney,
                TaxMoney = orderCost.TaxMoney,
                DiscountMoney = orderCost.DiscountMoney,
                CostPrice = subOrder.Cost,
                CompleteTime = subOrderFlow?.CreationTime,
                Boms = orderItemDto,
                CustomerRemark = order.CustomerRemark,
                CustomerServiceName = customerServiceDto.Name,
                MemberName = member.Name,
                PhoneNumber = member.PhoneNumber,
                ReceiverName = delivery?.ReceiverName,
                ReceiverTel = delivery?.ReceiverTel,
                ReceiverAddress = delivery?.ReceiverAddress,
                TrackingNo = order.TrackingNo,
                OrderName = order.OrderName,
                DeliveryDate = order.DeliveryDate,
                DeliveryDays = order.DeliveryDays,
                PayTime = order.PayTime,
            };
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="subOrder"><see cref="SubOrder"/></param>
        /// <param name="input"><see cref="CheckInput"/></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        private async Task CheckAsync(SubOrder subOrder, CheckInput input)
        {
            if (subOrder == null)
            {
                throw new UserFriendlyException("Oops!订单不存在");
            }
            await _subOrderManager.CheckAsync(subOrder, input.IsPassed, input.Remark);
        }

        /// <summary>
        /// 报价
        /// </summary>
        /// <param name="subOrder"><see cref="SubOrder"/></param>
        /// <param name="input"><see cref="OfferInput"/></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        private async Task OfferAsync(SubOrder subOrder, OfferInput input)
        {
            if (subOrder == null)
            {
                throw new UserFriendlyException("Oops!订单不存在");
            }
            await _subOrderManager.OfferAsync(subOrder, input.Cost, input.SellingPrice, input.ShipPrice, input.DiscountMoney, input.Remark);
        }

        /// <summary>
        /// 投产
        /// </summary>
        /// <param name="subOrder"><see cref="SubOrder"/></param>
        /// <param name="input"><see cref="ManufactureInput"/></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        private async Task ManufactureAsync(SubOrder subOrder, ManufactureInput input)
        {
            if (subOrder == null)
            {
                throw new UserFriendlyException("Oops!订单不存在");
            }
            await _subOrderManager.ManufactureAsync(subOrder, input.Remark);
        }

        private async Task CancelAsync(SubOrder subOrder, CancelInput input)
        {

            if (subOrder == null)
            {
                throw new UserFriendlyException("Oops!订单不存在");
            }

            await _subOrderManager.CancelAsync(subOrder, input.Rremark);
        }
        #endregion 私有
    }
}