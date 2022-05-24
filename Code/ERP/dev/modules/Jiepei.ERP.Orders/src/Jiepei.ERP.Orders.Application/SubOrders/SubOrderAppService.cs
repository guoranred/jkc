
using Jiepei.ERP.DeliverCentersClient.DeliverCenterClients;
using Jiepei.ERP.DeliverCentersClient.Dto.SheetMetal;
using Jiepei.ERP.DeliverCentersClient.Enums;
using Jiepei.ERP.Members;
using Jiepei.ERP.Members.CustomerServices;
using Jiepei.ERP.Orders.Application.External.Order;
using Jiepei.ERP.Orders.Application.External.Order.Models;
using Jiepei.ERP.Orders.Channels;
using Jiepei.ERP.Orders.Materials;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Orders.Orders.Dtos;
using Jiepei.ERP.Orders.SubOrders.Dtos;
using Jiepei.ERP.SubOrders;
using Jiepei.ERP.Utilities;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;
using static AutoMapper.Internal.ExpressionFactory;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace Jiepei.ERP.Orders.SubOrders
{
    [Authorize]
    public partial class SubOrderAppService : OrdersAppService, ISubOrderAppService
    {
        private readonly ISubOrderManager _subOrderManager;
        private readonly ISubOrderRepository _subOrderRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IRepository<OrderDelivery> _orderDeliveryRepository;
        private readonly IRepository<OrderCost> _orderCostRepository;
        private readonly IRepository<OrderLog> _orderLogRepository;
        private readonly IRepository<SubOrderFlow> _subOrderFlowRepository;
        private readonly ISubOrderThreeDItemRepository _subOrderThreeDItemRepository;
        private readonly IMaterialManager _materialManager;
        private readonly ID3MaterialRepository _d3MaterialRepository;
        private readonly IMaterialPriceRepository _materialPriceRepository;
        private readonly IMaterialAppService _materialAppService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IMemberAppService _memberAppService;
        private readonly ISubOrderSheetMetalItemRepository _subOrderSheetMetalItemRepository;
        private readonly ISubOrderCncItemRepository _subOrderCncItemRepository;
        private readonly IChannelAppService _channelAppService;
        private readonly ISheetMetalApi _sheetMetalApi;
        private readonly ICncApi _cncApi;
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly OrderExternalService _orderExternalService;
        static Object locker = new object();

        public SubOrderAppService(ISubOrderManager subOrderManager
            , IOrderRepository orderRepository
            , IRepository<OrderDelivery> orderDeliveryRepository
            , IRepository<OrderCost> orderCostRepository
            , IRepository<SubOrderFlow> subOrderFlowRepository
            , ISubOrderThreeDItemRepository subOrderThreeDItemRepository
            , IMaterialManager materialManager
            , ID3MaterialRepository d3MaterialRepository
            , IMaterialPriceRepository materialPriceRepository
            , ISubOrderRepository subOrderRepository
            , IRepository<OrderLog> orderLogRepository
            , IMaterialAppService materialAppService
            , IUnitOfWorkManager unitOfWorkManager
            , IMemberAppService memberAppService
            , ISubOrderSheetMetalItemRepository subOrderSheetMetalItemRepository
            , IChannelAppService channelAppService
            , ISubOrderCncItemRepository subOrderCncItemRepository
            , OrderExternalService orderExternalService
            , ISheetMetalApi sheetMetalApi
            , ICncApi cncApi)
        {
            _subOrderManager = subOrderManager;
            _orderRepository = orderRepository;
            _orderDeliveryRepository = orderDeliveryRepository;
            _orderCostRepository = orderCostRepository;
            _subOrderFlowRepository = subOrderFlowRepository;
            _subOrderThreeDItemRepository = subOrderThreeDItemRepository;
            _materialManager = materialManager;
            _d3MaterialRepository = d3MaterialRepository;
            _materialPriceRepository = materialPriceRepository;
            _subOrderRepository = subOrderRepository;
            _orderLogRepository = orderLogRepository;
            _materialAppService = materialAppService;
            _unitOfWorkManager = unitOfWorkManager;
            _memberAppService = memberAppService;
            _subOrderSheetMetalItemRepository = subOrderSheetMetalItemRepository;
            _channelAppService = channelAppService;
            _subOrderCncItemRepository = subOrderCncItemRepository;
            _orderExternalService = orderExternalService;
            _sheetMetalApi = sheetMetalApi;
            _cncApi = cncApi;
        }


        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RemoteService(false)]
        public async Task<OrderBaseDto> CreateThreeDAsync(CreateOrderExtraDto input)
        {

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);

            // var orderNo = OrderHelper.CreateOrderNo(EnumOrderType.Mold);


            var orderid = GuidGenerator.Create();
            var channel = await _channelAppService.GetAsync(input.ChannelId);

            // 创建订单
            var orderEntiy = await CreateOrderAsync(input, "", orderid, 0);

            //根据自增值生成orderno
            var n =await _memberAppService.GetCodeGeneration();
            var orderNo = OrderHelper.GetOrderType(EnumOrderType.Print3D)+"-" + (1000 + n.Id).ToString();

            // 创建子订单
            var subOrderEntiy = CreateSubOrderAsync(input, orderNo, orderid);
            //创建Bom
            var deliveryInfo = await CreateThreeDItemAsync(input.ExtraProperties, subOrderEntiy.Id, input.ChannelId);
            var sellprice = deliveryInfo.SumPrice; //总价
            var maxDay = deliveryInfo.MaxDay; //交期
            var weight = deliveryInfo.Weight; //重量

            // 创建配送信息
            await CreateDelivery(input.DeliveryInfo, orderNo, weight);

            //运费  
            var shipMoney = await _materialAppService.GetShipMoney(weight, input.DeliveryInfo.ProvinceCode);
            var orderCost = await CreateOrderCostAsync(orderNo, sellprice, shipMoney);

            //更新销售价
            var TotalPrice = sellprice * 1.08m + shipMoney;
            orderEntiy.SetSellingPrice(TotalPrice);
            subOrderEntiy.SetSellingPrice(TotalPrice);


            orderEntiy.SetOrderNo(orderNo);
            orderEntiy.SetDeliveryDate(await _materialManager.Calculation3DDeliveryDays(maxDay));
            orderEntiy.SetDeliveryDays(maxDay);

            await _orderRepository.InsertAsync(orderEntiy, true);
            await _subOrderRepository.InsertAsync(subOrderEntiy, true);

            // 创建日志
            //  await CreateOperatorLog(orderNo, "用户下单", "创建订单");
            await CreateOrderLog(orderNo, "创建订单");
            await CreateSubOrderFlow(subOrderEntiy.OrderNo, "用户下单", "创建订单");

            await uow.CompleteAsync();

            return new OrderBaseDto { Id = orderEntiy.Id, OrderNo = orderNo };
        }


        public async Task<OrderBaseDto> CreateCncAsync(CreateOrderExtraDto input)
        {

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);

            //var orderNo = OrderHelper.CreateOrderNo(EnumOrderType.Mold);
            // var orderNo = OrderHelper.CreateOrderNo();
            // var orderNo = OrderHelper.GetOrderType(EnumOrderType.Print3D) + (1000 + _orderRepository.Count()).ToString();
            var orderid = GuidGenerator.Create();
            var channel = await _channelAppService.GetAsync(input.ChannelId);

            // 创建订单
            var orderEntiy = await CreateOrderAsync(input, "", orderid, 0);


            //根据自增值生成orderno
            var n = await _memberAppService.GetCodeGeneration();
            var orderNo = OrderHelper.GetOrderType(EnumOrderType.Cnc) + "-" + (1000 + n.Id).ToString();

            // 创建子订单
            var subOrderEntiy = CreateSubOrderAsync(input, orderNo, orderid);
            //创建Bom
            var cncItem = await CreateCncItemAsync(input.ExtraProperties, subOrderEntiy.Id, input.ChannelId);

            var member = await _memberAppService.GetAsync((Guid)CurrentUser.Id);
            // 创建配送信息
            var orderDelivery = await CreateDelivery(input.DeliveryInfo, orderNo, 0m);

            //更新价格
            var orderCost = await CreateOrderCostAsync(orderNo, 0, 0);

            orderEntiy.SetOrderNo(orderNo);
            //更新销售价
            orderEntiy.SetSellingPrice(0);
            subOrderEntiy.SetSellingPrice(0);

            await _orderRepository.InsertAsync(orderEntiy, true);
            await _subOrderRepository.InsertAsync(subOrderEntiy, true);

            // 创建日志
            //  await CreateOperatorLog(orderNo, "用户下单", "创建订单");
            await CreateOrderLog(orderNo, "创建订单");
            await CreateSubOrderFlow(subOrderEntiy.OrderNo, "用户下单", "创建订单");

            await uow.CompleteAsync();

            var itemDto = new ItemDto()
            {
                FileName = cncItem.FileName,
                FilePath = cncItem.FilePath,
                NeedDesign = false,
                PurchasedParts = 0
            };

            Guid.TryParse(orderEntiy.Engineer.ToString(), out var resultSalesmanID);
            var salesman = await _memberAppService.GetCustomerServiceAsync(resultSalesmanID);
            Guid.TryParse(orderEntiy.CustomerService.ToString(), out var resultcustomerServiceID);
            var customerService = await _memberAppService.GetCustomerServiceAsync(resultcustomerServiceID);

            //甲壳虫同步订单到第三方
            var orderInfo = CreateDeliverCentersCncOrderAsync(orderEntiy, subOrderEntiy, itemDto, salesman?.JobNumber, customerService?.JobNumber);
            orderInfo.Order.OrderDelivery = CreateDeliverCenterOrderDelivery(orderDelivery);
            orderInfo.Order.OrderMemberDemand = CreateDeliverCenterOrderMemberDemand((Guid)CurrentUser.Id, orderEntiy.CustomerRemark, member);
            orderInfo.Order.OrderCost = CreateDeliverCenterOrderCost(orderCost);
            orderInfo.Order.OrderProcess = CreateDeliverCenterOrderProcess(orderEntiy);
            orderInfo.Items = CreateDeliverCenterProduct_Cnc(cncItem);

            _ = SyncOrderInfoAsync_DeliverCenters(orderInfo, orderEntiy.OrderType);

            return new OrderBaseDto { Id = orderEntiy.Id, OrderNo = orderNo };
        }

        public async Task<OrderBaseDto> CreateSheetMetalAsync(CreateOrderExtraDto input)
        {

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);

            //var orderNo = OrderHelper.CreateOrderNo(EnumOrderType.Mold);
            //var orderNo = OrderHelper.CreateOrderNo();
            //var orderNo = OrderHelper.GetOrderType(EnumOrderType.Print3D) + (1000 + _orderRepository.Count()).ToString();
            var orderid = GuidGenerator.Create();
            var channel = await _channelAppService.GetAsync(input.ChannelId);

            // 创建订单
            var orderEntiy = await CreateOrderAsync(input, "", orderid, 0);
            //根据自增值生成orderno
            var n = await _memberAppService.GetCodeGeneration();
            var orderNo = OrderHelper.GetOrderType(EnumOrderType.SheetMetal) + "-" + (1000 + n.Id).ToString();


            // 创建子订单
            var subOrderEntiy = CreateSubOrderAsync(input, orderNo, orderid);
            //创建Bom
            var sheetMetalItem = await CreateSheetMetalItemAsync(input.ExtraProperties, subOrderEntiy.Id, input.ChannelId);

            var member = await _memberAppService.GetAsync((Guid)CurrentUser.Id);

            // 创建配送信息
            var orderDelivery = await CreateDelivery(input.DeliveryInfo, orderNo, 0m);

            //更新价格
            var orderCost = await CreateOrderCostAsync(orderNo, 0, 0);

            orderEntiy.SetOrderNo(orderNo);
            //更新销售价
            orderEntiy.SetSellingPrice(0);
            subOrderEntiy.SetSellingPrice(0);


            await _orderRepository.InsertAsync(orderEntiy, true);
            await _subOrderRepository.InsertAsync(subOrderEntiy, true);

            // 创建日志
            //  await CreateOperatorLog(orderNo, "用户下单", "创建订单");
            await CreateOrderLog(orderNo, "创建订单");
            await CreateSubOrderFlow(subOrderEntiy.OrderNo, "用户下单", "创建订单");

            await uow.CompleteAsync();

            var itemDto = new ItemDto()
            {
                FileName = sheetMetalItem.FileName,
                FilePath = sheetMetalItem.FilePath,
                NeedDesign = sheetMetalItem.NeedDesign,
                PurchasedParts = sheetMetalItem.PurchasedParts
            };

            Guid.TryParse(orderEntiy.Engineer.ToString(), out var resultSalesmanID);
            var salesman = await _memberAppService.GetCustomerServiceAsync(resultSalesmanID);
            Guid.TryParse(orderEntiy.CustomerService.ToString(), out var resultcustomerServiceID);
            var customerService = await _memberAppService.GetCustomerServiceAsync(resultcustomerServiceID);

            //甲壳虫同步订单到第三方
            var orderInfo = CreateDeliverCentersSheetMetalOrderAsync(orderEntiy, subOrderEntiy, itemDto, salesman?.JobNumber, customerService?.JobNumber);
            orderInfo.Order.OrderDelivery = CreateDeliverCenterOrderDelivery(orderDelivery);
            orderInfo.Order.OrderMemberDemand = CreateDeliverCenterOrderMemberDemand((Guid)CurrentUser.Id, orderEntiy.CustomerRemark, member);
            orderInfo.Order.OrderCost = CreateDeliverCenterOrderCost(orderCost);
            orderInfo.Order.OrderProcess = CreateDeliverCenterOrderProcess(orderEntiy);

            orderInfo.Items = CreateDeliverCenterProduct(sheetMetalItem);

            _ = SyncOrderInfoAsync_DeliverCenters(orderInfo, orderEntiy.OrderType);

            return new OrderBaseDto { Id = orderEntiy.Id, OrderNo = orderNo };
        }




        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="id">主订单 Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CancelAsync(Guid id, CancelInput input)
        {
            await _subOrderManager.CancelAsync(id, input.Rremark);
            var subOrder = await _subOrderRepository.FindAsync(t => t.Id == id);
            var order = await _orderRepository.FindAsync(t => t.Id == subOrder.OrderId);
            if (order.OrderType == EnumOrderType.Cnc)
                _ = UpdateStatusAsync(subOrder.OrderNo, input);
            if (order.OrderType == EnumOrderType.SheetMetal)
                _ = UpdateStatusAsync_DeliverCenters(order.OrderNo, input, order.OrderType);
        }


        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="id">主订单 Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CompleteAsync(Guid id, CompleteInput input)
        {
            var subOrder = await _subOrderManager.CompleteAsync(id, input.Remark);
            // var subOrder = await _subOrderRepository.FindAsync(t => t.Id == id);
            var order = await _orderRepository.FindAsync(t => t.Id == subOrder.OrderId);
            // if (order.OrderType == EnumOrderType.Cnc)
            //   _ = FinishOrder(subOrder.OrderNo);
            if (order.OrderType == EnumOrderType.SheetMetal || order.OrderType == EnumOrderType.Cnc)
                _ = FinishOrder_DeliverCenters(order.OrderNo, order.OrderType);
        }

        /// <summary>
        /// 修改订单(重新上传文件)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOrderFileAsync(UpdateOrderInput input)
        {
            var UserId = CurrentUser.Id;
            var OldOrder = await _orderRepository.FirstOrDefaultAsync(e => e.Id == input.Id);
            if (OldOrder == null)
                throw new UserFriendlyException("当前订单不存在或已被删除");

            //审核不通过或者待审核的时候才可以重新上传文件
            if (OldOrder.Status == EnumOrderStatus.CheckedNoPass || OldOrder.Status == EnumOrderStatus.WaitCheck)
            {
                using var uow = _unitOfWorkManager.Begin(isTransactional: true);

                var subOrder = await _subOrderRepository.FindAsync(t => t.OrderId == OldOrder.Id);
                var subOrderSheetMetalItem = await _subOrderSheetMetalItemRepository.FindAsync(t => t.SubOrderId == subOrder.Id);
                subOrderSheetMetalItem.FileName = input.ProductFileName;
                subOrderSheetMetalItem.FilePath = input.ProductFilePath;
                subOrderSheetMetalItem.PreviewUrl = input.PreviewUrl;
                subOrderSheetMetalItem.Thumbnail = input.Thumnail;
                await _subOrderSheetMetalItemRepository.UpdateAsync(subOrderSheetMetalItem);

                OldOrder.LastModifierId = UserId;
                OldOrder.Status = EnumOrderStatus.WaitCheck;
                await _orderRepository.UpdateAsync(OldOrder);
                await uow.CompleteAsync();

                await UpdateAsync(new UpdateOrderProductFileDto(OldOrder.OrderNo, input.ProductFileName, input.ProductFilePath));
            }
            else
            {
                throw new UserFriendlyException("当前订单不可重新上传文件");
            }

            return true;
        }


        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task PaymentAsync(OrderDto order)
        {
            var payDto = new DC_PaymentDto()
            {
                PaidMoney = order.PaidMoney,
                PayTime = order.PayTime,
                PayType = EnumDeliverCenterPayType.Cash,
                PayChannelMoney = 0,
                ShipMoney=0
            };
            _ = PaymentAsync_DeliverCenters(order.OrderNo, payDto, order.OrderType);
        }

   

        private async Task<Order> CreateOrderAsync(CreateOrderExtraDto input, string orderNo, Guid orderid, int deliveryDays)
        {
            DateTime? deliveryDate = null;

            //计算交期日期



            var orderEntity = new Order(
                id: orderid,
                orderNo: orderNo,
                status: EnumOrderStatus.WaitCheck,
                channel: input.ChannelId,
                channelOrderNo: null,// input.ChannelOrderNo,
                channelUserId: CurrentUser.Id.ToString(),// input.CustomerId,
                customerRemark: input.Remark,
                orderType: input.OrderType,
                orderName: input.OrderName,
                deliveryDate: deliveryDate,
                deliveryDays: deliveryDays);

            var member = await _memberAppService.GetAsync((Guid)CurrentUser.Id);
            orderEntity.SetCustomerService(member?.CustomerServiceId);
            orderEntity.SetEngineer(member?.SalesmanId);

            return orderEntity;
        }

        private async Task<OrderDelivery> CreateDelivery(CreateDeliveryDto input, string orderNo, decimal weight)
        {
            var deliveryEntity = ObjectMapper.Map<CreateDeliveryDto, OrderDelivery>(input);
            var memberInfor = await _memberAppService.GetAsync(CurrentUser.Id.Value);
            deliveryEntity.SetOrderNo(orderNo);
            deliveryEntity.SetWeight(weight);
            if (CurrentUser.Id.HasValue)
            {
                deliveryEntity.SetOrderContactName(memberInfor.Name);
                deliveryEntity.SetOrderContactMobile(memberInfor.PhoneNumber);
                deliveryEntity.SetOrderContactQQ(memberInfor.QQ);

            }
            return await _orderDeliveryRepository.InsertAsync(deliveryEntity);
        }

        private async Task<OrderCost> CreateOrderCostAsync(string orderNo, decimal price, decimal shipMoney)
        {

            var entity = new OrderCost(
             GuidGenerator.Create(),
             orderNo,
             price,
             price * 0.08m,
             0.08m,
             shipMoney);
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

        private SubOrder CreateSubOrderAsync(CreateOrderExtraDto input, string orderNo, Guid orderId)
        {
            var subOrderId = GuidGenerator.Create();
            var subOrderEntity = new SubOrder(
                  id: subOrderId
                , orderId: orderId
                , orderNo: orderNo + "-" + OrderHelper.GetOrderType(input.OrderType)
                , channel: input.ChannelId
                , channelOrderNo: null// createOrderExtraDto.ChannelOrderNo
                , channelUserId: CurrentUser.Id.ToString()// createOrderExtraDto.CustomerId
                , cost: 0m
                , sellingPrice: 0m
                , orderType: input.OrderType
                , organizationUnitId: null
                , status: EnumSubOrderStatus.WaitCheck
                , remark: ""
            );
            return subOrderEntity;
        }

        private OrderInfoDto CreateOrderInfoDtoAsync(Order order, SubOrder subOrder, ItemDto item)
        {
            var orderInfoDto = new OrderInfoDto();
            orderInfoDto.OrderNo = subOrder.OrderNo;
            //orderInfoDto.OrderChannel = "JKC";
            orderInfoDto.MemberId = 0;
            orderInfoDto.GroupNo = order.OrderNo;
            //orderInfoDto.OrderChannelName = "甲壳虫";
            //orderInfoDto.DeliveryDate = DateTime.Now.AddDays(5);
            orderInfoDto.FollowAdminName = "";
            orderInfoDto.FrontCalcPrice = 0;
            orderInfoDto.MemberName = CurrentUser.Name;
            orderInfoDto.OrderName = "";
            //orderInfoDto.OrderType=
            orderInfoDto.ProductAssembleType = false;
            orderInfoDto.ProductFileName = item.FileName;
            orderInfoDto.ProductFilePath = item.FilePath;
            //orderInfoDto.ProductFittingFilePath
            orderInfoDto.ProductFittingSourceTypeStr = item.PurchasedParts;
            orderInfoDto.ProductNeedDesign = item.NeedDesign;
            orderInfoDto.ProductNum = 1;
            orderInfoDto.ProductRemark = order.CustomerRemark;
            orderInfoDto.ProductType = order.OrderType.GetDescription();
            return orderInfoDto;
        }
        private OrderGroupDto CreateOrderGroupDtoAsync(Order order, OrderDelivery orderDelivery)
        {
            var orderGroupDto = new OrderGroupDto();
            orderGroupDto.GroupNo = order.OrderNo;
            orderGroupDto.MbId = 0;
            orderGroupDto.ProNum = 1;
            orderGroupDto.TotalWeight = 0;
            orderGroupDto.OrderMoney = order.SellingPrice;
            orderGroupDto.OrderPayMoney = order.PaidMoney;
            orderGroupDto.ProMoney = order.SellingPrice;
            orderGroupDto.ShipMoney = 0;
            orderGroupDto.PreferentialMoney = 0;
            orderGroupDto.CreateTime = DateTime.Now;
            orderGroupDto.Note = order.CustomerRemark;
            orderGroupDto.ReceiverName = orderDelivery.ReceiverName;
            orderGroupDto.ReceiverCompany = orderDelivery.ReceiverCompany;
            orderGroupDto.ReceiverAddress = orderDelivery.ReceiverAddress;
            orderGroupDto.ReceiverTel = orderDelivery.ReceiverTel;
            orderGroupDto.ReceiverState = orderDelivery.CityName;
            orderGroupDto.OrderContactName = orderDelivery.OrderContactName;
            orderGroupDto.OrderContactMobile = orderDelivery.OrderContactMobile;
            orderGroupDto.OrderContactQQ = orderDelivery.OrderContactQQ;
            return orderGroupDto;
        }

        private async Task<CreateSubOrderSheetMetalItemDto> CreateSheetMetalItemAsync(ExtraPropertyDictionary input, Guid subOrderId, Guid channelId)
        {
            var entiy = JsonConvert.DeserializeObject<CreateSubOrderSheetMetalItemDto>(System.Text.Json.JsonSerializer.Serialize(input));
            var subOrderSheetMetalItem = ObjectMapper.Map<CreateSubOrderSheetMetalItemDto, SubOrderSheetMetalItem>(entiy);
            subOrderSheetMetalItem.SubOrderId = subOrderId;
            await _subOrderSheetMetalItemRepository.InsertAsync(subOrderSheetMetalItem);
            return entiy;
        }

        private async Task<CreateSubOrderCncItemDto> CreateCncItemAsync(ExtraPropertyDictionary input, Guid subOrderId, Guid channelId)
        {
            var entiy = JsonConvert.DeserializeObject<CreateSubOrderCncItemDto>(System.Text.Json.JsonSerializer.Serialize(input));
            var subOrderCncItem = ObjectMapper.Map<CreateSubOrderCncItemDto, SubOrderCncItem>(entiy);
            subOrderCncItem.SubOrderId = subOrderId;
            await _subOrderCncItemRepository.InsertAsync(subOrderCncItem);
            return entiy;
        }

        private async Task<OrderValuationDto> CreateThreeDItemAsync(ExtraPropertyDictionary input, Guid subOrderId, Guid channelId)
        {
            var orderValuation = new OrderValuationDto();
            orderValuation.SumPrice = 0m;
            orderValuation.MaxDay = 0;
            orderValuation.Weight = 0m;
            //var sumPrice = 0m;//总价
            //var maxDay = 0;//交期
            //var weight = 0m;//重量


            var extraOrder = JsonConvert.DeserializeObject<CreateSubOrderThreeDItemListDto>(System.Text.Json.JsonSerializer.Serialize(input));

            var subOrderThreeDItems = extraOrder?.SubOrderThreeDItemDtos;
            var materialIds = subOrderThreeDItems?.Select(x => x.MaterialId)?.ToList();
            var d3MaterialList = await Getd3MaterialListAsync(channelId, materialIds);

            var orderBomList = new List<SubOrderThreeDItem>();
            foreach (var item in subOrderThreeDItems)
            {
                var d3MaterialEntity = d3MaterialList?.FirstOrDefault(x => x.Item1.Id == item.MaterialId).Item1;
                var materialPriceEntity = d3MaterialList?.FirstOrDefault(x => x.Item2.MaterialId == item.MaterialId).Item2;

                var orderBomEntity = ObjectMapper.Map<CreateSubOrderThreeDItemDto, SubOrderThreeDItem>(item);
                //orderBomEntity.FileMD5 = Base64Decode(orderBomEntity.FileMD5);

                var volume = orderBomEntity.Volume;
                var density = Convert.ToDecimal(d3MaterialEntity.Density);

                //单价
                var unitPrice = _materialManager.GetUnitPrice(volume, density, materialPriceEntity.Price, materialPriceEntity.UnitStartPrice);

                orderValuation.Weight += _materialManager.GetWeight(volume, density, orderBomEntity.Count);

                //后处理价格
                var handleMethodDescPrices = _materialManager.GetHandleMethodDescPrice(orderBomEntity.HandleMethod, orderBomEntity.HandleMethodDesc.ToString()) * orderBomEntity.Count;

                //材料费
                var orginalMoney = materialPriceEntity.Discount * unitPrice * orderBomEntity.Count;

                //销售价
                var sellingPrice = Math.Ceiling(orginalMoney + handleMethodDescPrices);
                //对比起步价
                sellingPrice = sellingPrice < materialPriceEntity.StartPrice ? materialPriceEntity.StartPrice : sellingPrice;

                //交期天数
                var deliveryNum = await _materialManager.Calculation3DDelivery(
                     channelId
                   , orderBomEntity.MaterialId
                   , orderBomEntity.HandleMethod
                   , orderBomEntity.HandleMethodDesc);
                if (orderValuation.MaxDay < deliveryNum)
                    orderValuation.MaxDay = deliveryNum;

                orderValuation.SumPrice += sellingPrice;
                orderBomEntity.SetSubOrderId(subOrderId);
                orderBomEntity.SetHandleFee(handleMethodDescPrices);
                orderBomEntity.SetPrice(Math.Ceiling(unitPrice));
                orderBomEntity.SetOrginalMoney(sellingPrice);
                orderBomEntity.SetDeliveryDays(deliveryNum);

                orderBomList.Add(orderBomEntity);
                // await _subOrderThreeDItemRepository.InsertAsync(orderBomEntity);
            }

            await _subOrderThreeDItemRepository.InsertManyAsync(orderBomList);
            return orderValuation;
        }

        private async Task<List<(D3Material, MaterialPrice)>> Getd3MaterialListAsync(Guid channelId, List<Guid> materialIds)
        {
            var list = new List<(D3Material, MaterialPrice)>();
            var d3MaterialEntiy = await _d3MaterialRepository.GetQueryableAsync();
            var materialPriceEntiy = await _materialPriceRepository.GetQueryableAsync();
            var query = d3MaterialEntiy
                .Join(materialPriceEntiy, e => e.Id, o => o.MaterialId, (e, o) => new { e, o })
                .Where(t => materialIds.Contains(t.o.MaterialId))
                .Where(t => t.o.ChannelId == channelId);

            var listAsync = await AsyncExecuter.ToListAsync(query);
            foreach (var item in listAsync)
            {
                var d3Material = item.e;
                var materialPrice = item.o;
                list.Add((d3Material, materialPrice));
            }
            return list;
        }

        private string Base64Decode(string Message)
        {
            if (string.IsNullOrEmpty(Message))
                return null;
            return BitConverter.ToString(Convert.FromBase64String(Message)).Replace("-", "");
        }


        #region 第三方订单同步


        #endregion



    }
}
