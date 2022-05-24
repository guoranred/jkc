using System.Threading.Tasks;
using Jiepei.ERP.Orders.SubOrders.Dtos;
using Jiepei.ERP.DeliverCentersClient.Dto;
using Jiepei.ERP.DeliverCentersClient.Dto.SheetMetal;
using Jiepei.ERP.DeliverCentersClient.Enums;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Shared.Enums.SheetMetals;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using Volo.Abp;
using Jiepei.BizMO.DeliverCenters.PrecisionMetal.Enums;
using Jiepei.ERP.Members;
using Jiepei.ERP.Utilities;

namespace Jiepei.ERP.Orders.SubOrders
{
    public partial class SubOrderAppService : OrdersAppService, ISubOrderAppService
    {
        /// <summary>
        /// 同步订单
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private async Task SyncOrderInfoAsync_DeliverCenters(DC_CreateDto order, EnumOrderType orderType)
        {
            try
            {
                switch (orderType)
                {
                    case EnumOrderType.Mold:
                        break;
                    case EnumOrderType.Injection:
                        break;
                    case EnumOrderType.Cnc:
                        await _cncApi.CreateAsync(order);
                        break;
                    case EnumOrderType.Print3D:
                        break;
                    case EnumOrderType.SheetMetal:
                        await _sheetMetalApi.CreateAsync(order);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("（下单）订单同步交付中台失败 OrderNo:" + order.Order.ChannelOrderNo);
            }

        }
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="input"></param>
        /// <param name="orderType"></param>
        /// <returns></returns> 
        private async Task UpdateStatusAsync_DeliverCenters(string orderNo, CancelInput input, EnumOrderType orderType)
        {
            try
            {
                var cancelOrderDto = new DC_CancelOrderDto()
                {
                    CancelReason = input.Rremark,
                };
                switch (orderType)
                {
                    case EnumOrderType.Mold:
                        break;
                    case EnumOrderType.Injection:
                        break;
                    case EnumOrderType.Cnc:
                        await _cncApi.CancelAsync(orderNo, cancelOrderDto);
                        break;
                    case EnumOrderType.Print3D:
                        break;
                    case EnumOrderType.SheetMetal:
                        await _sheetMetalApi.CancelAsync(orderNo, cancelOrderDto);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                throw new UserFriendlyException("（取消）订单同步交付中台失败 OrderNo:" + orderNo);
            }
        }


        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        private async Task FinishOrder_DeliverCenters(string orderNo, EnumOrderType orderType)
        {
            try
            {
                switch (orderType)
                {
                    case EnumOrderType.Mold:
                        break;
                    case EnumOrderType.Injection:
                        break;
                    case EnumOrderType.Cnc:
                        await _cncApi.ReceivingAsync(orderNo);
                        break;
                    case EnumOrderType.Print3D:
                        break;
                    case EnumOrderType.SheetMetal:
                        await _sheetMetalApi.ReceivingAsync(orderNo);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("（收货）订单同步交付中台失败 OrderNo:" + orderNo);
            }
        }

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="input"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private async Task PaymentAsync_DeliverCenters(string orderNo, DC_PaymentDto input, EnumOrderType orderType)
        {
            try
            {
                switch (orderType)
                {
                    case EnumOrderType.Mold:
                        break;
                    case EnumOrderType.Injection:
                        break;
                    case EnumOrderType.Cnc:
                        await _cncApi.PaymentAsync(orderNo, input);
                        break;
                    case EnumOrderType.Print3D:
                        break;
                    case EnumOrderType.SheetMetal:
                        await _sheetMetalApi.PaymentAsync(orderNo, input);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                throw new UserFriendlyException("（支付）订单同步交付中台失败 OrderNo:" + orderNo + " PaidMoney:" + input.PaidMoney);
            }
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

        #region 钣金
        private DC_CreateProductDto CreateDeliverCenterProduct(CreateSubOrderSheetMetalItemDto sheetMetalItem)
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
                Bom = CreateDeliverCenterBom(boms),
                Remark = sheetMetalItem.ProductRemark
            };
        }

        private List<DC_CreateBomDto> CreateDeliverCenterBom(Rootobject boms)
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

        private DC_CreateDto CreateDeliverCentersSheetMetalOrderAsync(Order order, SubOrder subOrder, ItemDto item, string salesman, string customerService)
        {
            var deliverCentersOrder = new DC_CreateDto()
            {
                Order = new DC_CreateOrderDto()
                {
                    OrderName = order.OrderName,
                    ChannelNo = "jmjs_jkc",
                    ChannelOrderNo = order.OrderNo,
                    ProType = "BJ",
                    OrderType = EnumDeliverCenterOrderType.SingleLine,
                    IsCustSupply = 0,
                    Salesman = salesman,
                    CustomerService = customerService,
                    MemberRemark = order.CustomerRemark,
                    IsTaoBao = 0,
                }
            };
            return deliverCentersOrder;
        }

        #endregion

        #region Cnc
        private DC_CreateProductDto CreateDeliverCenterProduct_Cnc(CreateSubOrderCncItemDto cncItem)
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
                Bom = CreateDeliverCenterBom_Cnc(cncItem),
                Remark = ""
            };
        }
        private List<DC_CreateBomDto> CreateDeliverCenterBom_Cnc(CreateSubOrderCncItemDto cncItem)
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
                    Num = 1,
                    Remark = "",
                    Attribute = CreateDeliverCenterAttribute_Cnc(cncItem)
                };
                bomList.Add(createBom);
                return bomList;
            }
            return null;
        }

        private List<DC_CreateAttributeDto> CreateDeliverCenterAttribute_Cnc(CreateSubOrderCncItemDto cncItem)
        {
            var attributeList = new List<DC_CreateAttributeDto>();
            attributeList.Add(CreateAttribute("表面处理", cncItem.SurfaceName, cncItem.Surface.ToString()));
            attributeList.Add(CreateAttribute("应用领域", cncItem.ApplicationArea.GetDescription(), cncItem.ApplicationArea.ToString()));
            attributeList.Add(CreateAttribute("表面等级", cncItem.SurfaceLevelName, cncItem.SurfaceLevel.ToString()));
            return attributeList;
        }

        private DC_CreateAttributeDto CreateAttribute(string name, string value, string valueId)
        {
            var createAttribute = new DC_CreateAttributeDto()
            {
                AttributeName = name,
                AttributeValue = value,
                AttributeValueId = valueId,
            };
            return createAttribute;

        }
        private DC_CreateDto CreateDeliverCentersCncOrderAsync(Order order, SubOrder subOrder, ItemDto item, string salesman, string customerService)
        {
            var deliverCentersOrder = new DC_CreateDto()
            {
                Order = new DC_CreateOrderDto()
                {
                    OrderName = order.OrderName,
                    ChannelNo = "jmjs_jkc",
                    ChannelOrderNo = order.OrderNo,
                    ProType = "CNC",
                    OrderType = EnumDeliverCenterOrderType.SingleLine,
                    IsCustSupply = 0,
                    Salesman = salesman,
                    CustomerService = customerService,
                    MemberRemark = order.CustomerRemark,
                    IsTaoBao = 0,
                }
            };
            return deliverCentersOrder;
        }

        #endregion


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
        //private DC_CreateOrderExtraDto CreateDeliverCenterOrderExtra(decimal weight, string remark)
        //{
        //    return new DC_CreateOrderExtraDto()
        //    {
        //        ApplicationArea = EnumDeliverCenterApplicationArea.Other,
        //        IsTaoBao = 0,
        //        Usage = EnumDeliverCenterUsage.Other,
        //        Weight = weight,
        //        MemberRemark = remark
        //    };
        //}
        private DC_CreateOrderMemberDemandDto CreateDeliverCenterOrderMemberDemand(Guid memberId, string remark, MemberBaseInfoOutputDto member)
        {
            return new DC_CreateOrderMemberDemandDto()
            {
                MemberName = member.Name,
                MemberNo = member.Code,
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
                ShipMoney = orderCost.ShipMoney,
                PreferentialMoney = 0,
                BaitiaoPoint = 0,
            };
        }

        private DC_CreateOrderProcessDto CreateDeliverCenterOrderProcess(Order order) {
            return new DC_CreateOrderProcessDto()
            {
                OrderPlacedTime = order.CreationTime
            };
        }
    }
}
