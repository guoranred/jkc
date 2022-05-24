using AutoMapper;
using Jiepei.ERP.Orders.Admin.Application.Contracts.Materials.Dtos;
using Jiepei.ERP.Orders.Admin.Orders;
using Jiepei.ERP.Orders.Application.External;
using Jiepei.ERP.Orders.Application.External.Order.Models;
using Jiepei.ERP.Orders.Channels;
using Jiepei.ERP.Orders.Channels.Dtos;
using Jiepei.ERP.Orders.Materials;
using Jiepei.ERP.Orders.Materials.Dtos;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Orders.SubOrders;
using Jiepei.ERP.Orders.SubOrders.Dtos;
using Jiepei.ERP.Shared.Consumers.Orders;

namespace Jiepei.ERP.Orders.Admin
{
    public class OrdersAdminApplicationAutoMapperProfile : Profile
    {
        public OrdersAdminApplicationAutoMapperProfile()
        {
            CreateMap<SubOrderCncItem, OrderItemDto>();
            CreateMap<SubOrderMoldItem, OrderItemDto>();
            CreateMap<SubOrderInjectionItem, OrderItemDto>();
            CreateMap<SubOrderThreeDItem, OrderItemDto>();

            CreateMap<SubOrderThreeDItem, SubOrderThreeDItemDto>()
                .ForMember(dest => dest.Picture, opt => opt.MapFrom(src => src.Thumbnail))
                .ForMember(dest => dest.OrderNum, opt => opt.MapFrom(src => src.Count));
            CreateMap<SubOrderSheetMetalItem, OrderItemDto>();




            #region Orders
            CreateMap<Order, OrderDto>();
            CreateMap<CreateDeliveryDto, OrderDelivery>();
            CreateMap<CostDto, OrderCost>().ReverseMap();
            CreateMap<DeliveryDto, OrderDelivery>().ReverseMap();
            CreateMap<MQ_OrderTask_OrderDto, Order>();
            CreateMap<MQ_OrderTask_OrderCostDto, OrderCost>();
            CreateMap<MQ_OrderTask_OrderDeliveryDto, OrderDelivery>();
            CreateMap<OrderLog, OrderLogDto>();

            CreateMap<UpdateOrderStopInput, UpdateOrderStopExecDto>();
            CreateMap<UpdateOrderProductFileInput, UpdateOrderProductFileDto>();
            CreateMap<UpdateOrderPayStatusInput, UpdateOrderPayStatusDto>();
            CreateMap<CancelSheetInput, CancelOrderDto>();
            CreateMap<ApiHttpResponse, ApiHttpResponseDto>().ReverseMap(); ;

            #endregion

            #region SubOrder
            CreateMap<SubOrder, SubOrderDto>();
            CreateMap<SubOrderFlow, SubOrderFlowDto>();
            CreateMap<SubOrderMoldItem, SubOrderMoldItemDto>();
            CreateMap<SubOrderInjectionItem, SubOrderInjectionItemDto>();
            CreateMap<SubOrderCncItem, SubOrderCncItemDto>();
            CreateMap<CreateSubOrderThreeDItemDto, SubOrderThreeDItem>();

            CreateMap<SubOrderSheetMetalItem, SubOrderSheetMetalItemDto>();
            CreateMap<SubOrderCncItem, SubOrderCncItemDto>();
            CreateMap<SubOrderThreeDItem, OrderItemDto>();


            #endregion

            #region Channels
            CreateMap<Channel, ChannelDto>();
            CreateMap<CreateChannelDto, Channel>();
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

            #region Supplier
            CreateMap<MaterialSupplier, MaterialSupplierDto>().ReverseMap();
            #endregion
        }
    }
}
