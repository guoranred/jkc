using AutoMapper;
using Jiepei.ERP.Orders.Application.External;
using Jiepei.ERP.Orders.Application.External.Order.Models;
using Jiepei.ERP.Orders.Channels;
using Jiepei.ERP.Orders.Channels.Dtos;
using Jiepei.ERP.Orders.Materials;
using Jiepei.ERP.Orders.Materials.Dtos;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Orders.Orders.Dtos;
using Jiepei.ERP.Orders.Pays;
using Jiepei.ERP.Orders.Pays.Dtos;
using Jiepei.ERP.Orders.SubOrders;
using Jiepei.ERP.Orders.SubOrders.Dtos;
using Jiepei.ERP.Shared.Consumers.Orders;
using System;

namespace Jiepei.ERP.Orders
{
    public class OrdersApplicationAutoMapperProfile : Profile
    {
        public OrdersApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            #region Orders
            CreateMap<Order, OrderDto>();
            CreateMap<CreateDeliveryDto, OrderDelivery>();
            CreateMap<CostDto, OrderCost>().ReverseMap();
            CreateMap<DeliveryDto, OrderDelivery>().ReverseMap();
            CreateMap<MQ_OrderTask_OrderDto, Order>();
            CreateMap<MQ_OrderTask_OrderCostDto, OrderCost>();
            CreateMap<MQ_OrderTask_OrderDeliveryDto, OrderDelivery>();
            CreateMap<ProductPriceInputDto, ProductPriceInput>().ReverseMap();
            CreateMap<ProductPriceBomVO, ProductPriceBomVOInput>().ReverseMap();
            CreateMap<ProductPriceBomCraftVO, ProductPriceBomCraftVOInput>().ReverseMap();

            #endregion

            #region SubOrder          

            CreateMap<CreateSubOrderSheetMetalItemDto, SubOrderSheetMetalItem>();
            CreateMap<CreateSubOrderThreeDItemDto, SubOrderThreeDItem>();
            CreateMap<SubOrderThreeDItem, CustomerSubOrderThreeDItemDto>()
                .ForMember(t => t.Price, opt => opt.MapFrom(t => Math.Ceiling(t.Price)));
            CreateMap<SubOrderSheetMetalItem, CustomerSheetMetalOrderDto>();
            CreateMap<CreateSubOrderCncItemDto, SubOrderCncItem>();
            CreateMap<SubOrderSheetMetalItem, CustomerSubOrderSheetMetalItemDto>();
            CreateMap<SubOrderCncItem, CustomerSubOrderCncItemDto>();


            #endregion

            #region D3Material

            CreateMap<D3Material, D3MaterialDto>();
            CreateMap<CreateD3MaterialExtraDto, D3Material>();
            CreateMap<UpdateD3MaterialExtraDto, D3Material>();
            #endregion


            #region MaterialPrice
            CreateMap<MaterialPrice, MaterialPriceDto>();
            CreateMap<CreateMaterialPriceDto, MaterialPrice>();
            CreateMap<UpdateMaterialPriceDto, MaterialPrice>();
            #endregion

            #region Pays
            CreateMap<CreateOrderPayLogDto, OrderPayLog>();
            CreateMap<CreateOrderPayDetailLogDto, OrderPayDetailLog>();
            CreateMap<OrderPayLog, GetOrderPayLogDto>();
            CreateMap<OrderPayDetailLog, GetOrderPayDetailLogDto>();
            CreateMap<GetOrderPayLogDto, OrderPayLog>();
            CreateMap<UpdateOrderPayStatusInput, UpdateOrderPayStatusDto>();
            CreateMap<ApiHttpResponse, ApiHttpResponseDto>();

            
            #endregion

            #region Channels
            CreateMap<Channel, GetChannelDto>();
            #endregion


        }
    }
}