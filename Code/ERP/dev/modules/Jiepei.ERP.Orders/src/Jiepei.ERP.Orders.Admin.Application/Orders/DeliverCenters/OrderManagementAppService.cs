using Jiepei.BizMO.DeliverCenters.PrecisionMetal.Enums;
using Jiepei.ERP.DeliverCentersClient.Dto.SheetMetal;
using Jiepei.ERP.DeliverCentersClient.Enums;
using Jiepei.ERP.Members.Admin;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Orders.SubOrders;
using Jiepei.ERP.Shared.Enums.SheetMetals;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jiepei.ERP.Utilities;
using Jiepei.ERP.DeliverCentersClient.Dto;
using Jiepei.ERP.EventBus.Shared.Pays;
using static Jiepei.ERP.Shared.Consumers.RabbitMQConstant;
using System.Net.Http;
using Jiepei.ERP.Orders.Admin.Application.Contracts.Orders.Dtos.DeliverCenters;
using Volo.Abp;

namespace Jiepei.ERP.Orders.Admin
{
    public partial class OrderManagementAppService : OrdersAdminAppServiceBase, IOrderManagementAppService
    {
        #region 同步交付中台操作

        /// <summary>
        /// 同步订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task SyncOrderInfoAsync_DeliverCenters(Guid id)
        {
            var order = await _orderRepository.FindAsync(t => t.Id == id);
            if (order == null)
                throw new UserFriendlyException("（下单）订单同步交付中台失败查无订单:");

            var orderInfo = await BuildOrderAsync(id, order);

            HttpResponseMessage response = new HttpResponseMessage();

            switch (order.OrderType)
            {
                case EnumOrderType.SheetMetal:
                    response = await _sheetMetalApi.CreateAsync(orderInfo);
                    break;
                case EnumOrderType.Cnc:
                    response = await _cncApi.CreateAsync(orderInfo);
                    break;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new UserFriendlyException("（下单）订单同步交付中台失败 OrderNo:" + order.OrderNo);
            }
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task CancelAsync_DeliverCenters(Guid id)
        {
            var order = await _orderRepository.FindAsync(t => t.Id == id);
            if (order == null)
                throw new UserFriendlyException("（取消）订单同步交付中台失败查无订单:");

            var cancelDto = new DC_CancelOrderDto()
            {
                CancelReason = "客服主动取消订单",
            };

            HttpResponseMessage response = new HttpResponseMessage();

            switch (order.OrderType)
            {
                case EnumOrderType.SheetMetal:
                    response = await _sheetMetalApi.CancelAsync(order.OrderNo, cancelDto);
                    break;
                case EnumOrderType.Cnc:
                    response = await _cncApi.CancelAsync(order.OrderNo, cancelDto);
                    break;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new UserFriendlyException("（取消）订单同步交付中台失败 OrderNo:" + order.OrderNo);
            };
        }

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task FinishOrder_DeliverCenters(Guid id)
        {
            var order = await _orderRepository.FindAsync(t => t.Id == id);
            if (order == null)
                throw new UserFriendlyException("（收货）订单同步交付中台失败查无订单:");

            HttpResponseMessage response = new();

            switch (order.OrderType)
            {
                case EnumOrderType.SheetMetal:
                    response = await _sheetMetalApi.ReceivingAsync(order.OrderNo);
                    break;
                case EnumOrderType.Cnc:
                    response = await _cncApi.ReceivingAsync(order.OrderNo);
                    break;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new UserFriendlyException("（收货）订单同步交付中台失败 OrderNo:" + order.OrderNo);
            };
        }

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task PaymentAsync_DeliverCenters(Guid id)
        {
            var order = await _orderRepository.FindAsync(t => t.Id == id);
            if (order == null)
                throw new UserFriendlyException("（支付）订单同步交付中台失败查无订单:");

            var paymentDto = new DC_PaymentDto()
            {
                PaidMoney = order.PaidMoney,
                PayChannelMoney = 0,
                PayTime = order.PayTime,
                PayType = EnumDeliverCenterPayType.Cash
            };

            HttpResponseMessage response = new HttpResponseMessage();

            switch (order.OrderType)
            {
                case EnumOrderType.SheetMetal:
                    response = await _sheetMetalApi.PaymentAsync(order.OrderNo, paymentDto);
                    break;
                case EnumOrderType.Cnc:
                    response = await _cncApi.PaymentAsync(order.OrderNo, paymentDto);
                    break;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new UserFriendlyException("（支付）订单同步交付中台失败 OrderNo:" + order.OrderNo + " PaidMoney:" + order.PaidMoney);
            }
        }

        /// <summary>
        /// 线下付款
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task OffinePaymentAsync(Guid id, OfflinePaymentDto input)
        {
            var order = await _orderRepository.FindAsync(t => t.Id == id);
            if (order.Status < EnumOrderStatus.CheckedPass)
                throw new UserFriendlyException("状态不符合");
            if (input.PaidMoney < 0)
                throw new UserFriendlyException("金额不能小于0");
            if ( order.PendingMoney< input.PaidMoney)
                throw new UserFriendlyException("代付金额为"+ order.PendingMoney + "小于输入金额" + input.PaidMoney);

            await _localEventBus.PublishAsync(new OrderPayChangedEto
            {
                OrderNo = order.OrderNo,
                Amount = input.PaidMoney
            });

            var response = new HttpResponseMessage();

            var request = new OfflinePaymentRequest(input.PaidMoney, input.PayType);
            switch (order.OrderType)
            {
                case EnumOrderType.SheetMetal:
                    response = await _sheetMetalApi.OfflinePaymentAsync(order.OrderNo, request);
                    break;
                case EnumOrderType.Cnc:
                    response = await _cncApi.OfflinePaymentAsync(order.OrderNo, request);
                    break;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new UserFriendlyException("（支付）订单同步交付中台失败 OrderNo:" + order.OrderNo + " PaidMoney:" + input.PaidMoney);
            }
        }

        #endregion 同步交付中台操作

        #region private method

        #region 钣金
        private DC_CreateProductDto CreateDeliverCenterProductSheetMetal(SubOrderSheetMetalItem sheetMetalItem)
        {
            Rootobject boms = null;
            if (!string.IsNullOrWhiteSpace(sheetMetalItem.ProcessParameters))
                boms = JsonConvert.DeserializeObject<Rootobject>(sheetMetalItem.ProcessParameters);
            return new DC_CreateProductDto()
            {
                OrderFileName = sheetMetalItem.FileName,
                OrderFilePath = sheetMetalItem.FilePath,
                OrderFilePreview = sheetMetalItem.PreviewUrl,
                ProductFittingSourceType = GetPurchasedParts(sheetMetalItem.PurchasedParts),
                ProductAssembleType = sheetMetalItem.AssembleType,
                ProductNeedDesign = sheetMetalItem.NeedDesign,

                ProductBomNum = boms?.bomList?.Length ?? 0,
                ProductNum = sheetMetalItem.ProductNum,
                Bom = CreateDeliverCenterBomSheetMetal(boms),
                Remark = sheetMetalItem.ProductRemark
            };
        }

        private List<DC_CreateBomDto> CreateDeliverCenterBomSheetMetal(Rootobject boms)
        {
            if (boms != null)
            {
                // var boms = JsonConvert.DeserializeObject<Rootobject>(processParameters);
                var bomList = new List<DC_CreateBomDto>();
                foreach (var bom in boms.bomList)
                {
                    var createBom = new DC_CreateBomDto()
                    {
                        Width = decimal.Parse(bom.width),
                        Length = decimal.Parse(bom.length),
                        Height = decimal.Parse(bom.height),
                        BomName = bom.partName,
                        BomType = 0,
                        MaterialCategoryName = bom.materialCategoryName,
                        MaterialId = bom.materialId,
                        MaterialName = bom.materialName,
                        Num = int.Parse(bom.num),
                        Remark = bom.remark,
                        Attribute = CreateDeliverCenterAttribute(bom)
                    };
                    bomList.Add(createBom);
                }
                return bomList;
            }
            return null;
        }
        #endregion

        #region CNC

        private DC_CreateProductDto CreateDeliverCenterProductCnc(SubOrderCncItem cncItem)
        {
            return new DC_CreateProductDto()
            {
                OrderFileName = cncItem.FileName,
                OrderFilePath = cncItem.FilePath,
                OrderFilePreview = "",
                ProductFittingSourceType = EnumDeliverCenterProductFittingSourceType.Nothing,
                ProductAssembleType = false,
                ProductNeedDesign = false,

                ProductBomNum = 1,
                ProductNum = cncItem.Quantity,
                Bom = CreateDeliverCenterBomCnc(cncItem),
                Remark = ""
            };
        }
        private List<DC_CreateBomDto> CreateDeliverCenterBomCnc(SubOrderCncItem cncItem)
        {
            if (cncItem != null)
            {
                var bomList = new List<DC_CreateBomDto>();
                var size = cncItem.Size.Split('*');
                var createBom = new DC_CreateBomDto()
                {
                    Length = decimal.Parse(size[0]),
                    Width = decimal.Parse(size[1]),
                    Height = decimal.Parse(size[2]),
                    BomName = cncItem.ProductName,
                    BomType = 0,
                    MaterialCategoryName = "",
                    MaterialId = cncItem.Material.ToString(),
                    MaterialName = cncItem.MaterialName,
                    Num = cncItem.Quantity,
                    Remark = "",
                    Attribute = CreateDeliverCenterAttributeCnc(cncItem)
                };
                bomList.Add(createBom);
                return bomList;
            }
            return null;
        }

        #endregion

        private async Task<DC_CreateDto> BuildOrderAsync(Guid id, Order order)
        {
            var subOrder = await _subOrderRepository.FindAsync(t => t.OrderId == id);

            var member = await _memberManagementAppService.GetAsync(Guid.Parse(order.ChannelUserId));

            Guid.TryParse(order.Engineer.ToString(), out var resultSalesmanID);
            var salesman = await _consultantServiceManagementAppService.GetAsync(resultSalesmanID);
            Guid.TryParse(order.CustomerService.ToString(), out var resultcustomerServiceID);
            var customerService = await _consultantServiceManagementAppService.GetAsync(resultcustomerServiceID);

            var orderCost = await _orderCostRepository.FindAsync(t => t.OrderNo == order.OrderNo);
            var orderDelivery = await _orderDeliveryRepository.FindAsync(t => t.OrderNo == order.OrderNo);

            var proType = string.Empty;
            DC_CreateProductDto product = new();

            switch (order.OrderType)
            {
                case EnumOrderType.SheetMetal:
                    proType = "BJ";
                    product = CreateDeliverCenterProductSheetMetal(await _orderSheetMetalItemRepository.FindAsync(t => t.SubOrderId == subOrder.Id));
                    break;
                case EnumOrderType.Cnc:
                    proType = "CNC";
                    product = CreateDeliverCenterProductCnc(await _orderCncItemRepository.FindAsync(t => t.SubOrderId == subOrder.Id));
                    break;
            }

            var orderInfo = CreateDeliverCentersOrder(order, salesman?.JobNumber, customerService?.JobNumber, proType);
            orderInfo.Order.OrderDelivery = CreateDeliverCenterOrderDelivery(orderDelivery);
            orderInfo.Order.OrderMemberDemand = CreateDeliverCenterOrderMemberDemand(Guid.Parse(order.ChannelUserId), subOrder.Remark, member);
            orderInfo.Order.OrderCost = CreateDeliverCenterOrderCost(orderCost);
            orderInfo.Order.OrderProcess = CreateDeliverCenterOrderProcess(order);

            orderInfo.Items = product;

            return orderInfo;
        }

        private EnumDeliverCenterProductFittingSourceType GetPurchasedParts(EnumPurchasedParts enumPurchasedParts)
        {
            switch (enumPurchasedParts)
            {
                case EnumPurchasedParts.Unwanted:
                    return EnumDeliverCenterProductFittingSourceType.Nothing;

                case EnumPurchasedParts.Confession:
                    return EnumDeliverCenterProductFittingSourceType.SelfConfession;

                case EnumPurchasedParts.PurchasingAgent:
                    return EnumDeliverCenterProductFittingSourceType.Purchasing;

                default:
                    return EnumDeliverCenterProductFittingSourceType.Nothing;
            }
        }

        private List<DC_CreateAttributeDto> CreateDeliverCenterAttribute(Bomlist bom)
        {
            var attributeList = new List<DC_CreateAttributeDto>();
            foreach (var attribute in bom.bomCraftList)
            {
                var createAttribute = new DC_CreateAttributeDto()
                {
                    AttributeName = attribute.itemName,
                    AttributeValue = string.IsNullOrWhiteSpace(attribute.craftName) ? attribute.craftValue.ToString() : attribute.craftName,
                    AttributeValueId = "",
                };
                attributeList.Add(createAttribute);
            }
            return attributeList;
        }

        private DC_CreateDto CreateDeliverCentersOrder(Order order, string salesman, string customerService, string proType)
        {
            return new DC_CreateDto()
            {
                Order = new DC_CreateOrderDto()
                {
                    OrderName = order.OrderName,
                    ChannelNo = "jmjs_jkc",
                    ChannelOrderNo = order.OrderNo,
                    ProType = proType,
                    OrderType = EnumDeliverCenterOrderType.SingleLine,
                    IsCustSupply = 0,
                    Salesman = salesman,
                    CustomerService = customerService,
                    MemberRemark = order.CustomerRemark,
                    IsTaoBao = 0,
                }
            };
        }

        private DC_CreateOrderDeliveryDto CreateDeliverCenterOrderDelivery(OrderDelivery orderDelivery)
        {
            return new DC_CreateOrderDeliveryDto()
            {
                CityCode = orderDelivery.CityCode,
                CityName = orderDelivery.CityName,
                CountyCode = orderDelivery.CountyCode,
                CountyName = orderDelivery.CountyName,
                ReceiverAddress = orderDelivery.ReceiverAddress,
                ReceiverCompany = orderDelivery.ReceiverCompany,
                ReceiverName = orderDelivery.ReceiverName,
                ReceiverTel = orderDelivery.ReceiverTel,
                OrderContactMobile = orderDelivery.OrderContactMobile,
                OrderContactName = orderDelivery.OrderContactName,
                OrderContactQQ = orderDelivery.OrderContactQQ,
                ProvinceCode = orderDelivery.ProvinceCode,
                ProvinceName = orderDelivery.ProvinceName
            };
        }

        private DC_CreateOrderMemberDemandDto CreateDeliverCenterOrderMemberDemand(Guid memberId, string remark, MemberInformationDetailDto member)
        {
            return new DC_CreateOrderMemberDemandDto()
            {
                MemberName = member.Name,
                MemberNo =member.Code,
                MemberLevel = 0,
                MemberPhone = member.PhoneNumber,
                MemberCourierCompany = "",
                ApplicationArea = EnumDeliverCenterApplicationArea.Other,
                Usage = EnumDeliverCenterUsage.Other,
                Weight = 0,
                MemberRemark = remark
            };
        }

        private DC_CreateOrderCostDto CreateDeliverCenterOrderCost(OrderCost orderCost)
        {
            return new DC_CreateOrderCostDto()
            {
                TaxPoint = (int)(orderCost.TaxPoint * 100),
                ShipMoney = (int)(orderCost.ShipMoney * 1000),
            };
        }
        private DC_CreateOrderProcessDto CreateDeliverCenterOrderProcess(Order order)
        {
            return new DC_CreateOrderProcessDto()
            {
                OrderPlacedTime = order.CreationTime
            };
        }

        private List<DC_CreateAttributeDto> CreateDeliverCenterAttributeCnc(SubOrderCncItem cncItem)
        {
            var attributeList = new List<DC_CreateAttributeDto>();
            attributeList.Add(CreateAttribute("表面处理", cncItem.SurfaceName, cncItem.Surface.ToString()));
            attributeList.Add(CreateAttribute("应用领域", cncItem.ApplicationArea.GetDescription(), cncItem.ApplicationArea.ToString()));
            attributeList.Add(CreateAttribute("表面等级", cncItem.SurfaceLevelName, cncItem.SurfaceLevel.ToString()));
            return attributeList;
        }

        private DC_CreateAttributeDto CreateAttribute(string name, string value, string valueId)
        {
            return new DC_CreateAttributeDto()
            {
                AttributeName = name,
                AttributeValue = value,
                AttributeValueId = valueId,
            };

        }
        #endregion
    }
}