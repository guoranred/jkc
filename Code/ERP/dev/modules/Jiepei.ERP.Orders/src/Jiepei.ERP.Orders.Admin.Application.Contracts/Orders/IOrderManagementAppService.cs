using Jiepei.BizMO.DeliverCenters.Orders.Orders.Dtos;
using Jiepei.ERP.DeliverCentersClient.Dto;
using Jiepei.ERP.Orders.Admin.Application.Contracts.Orders.Dtos;
using Jiepei.ERP.Orders.Admin.Application.Contracts.Orders.Dtos.DeliverCenters;
using Jiepei.ERP.Orders.Admin.Orders;
using Jiepei.ERP.Orders.SubOrders.Dtos;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Orders.Admin
{
    public interface IOrderManagementAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerServiceOrderDto>> GetCustomerServiceOrderListAsync(GetCustomerServiceOrderInput input);

        Task<PagedResultDto<CustomerServiceOrderDto>> GetOrdersBySupplierCode(string supplierOrderCode);

        Task<CustomerServiceOrderDetailDto> GetCustomerServiceOrderDetail(Guid id, EnumOrderType orderType);

        Task<object> EditNoteAsync(Guid id, EditNoteInput input);

        Task CheckAsync(Guid id, CheckInput input);

        Task CancelAsync(Guid id, CancelInput input);

        Task OfferAsync(Guid id, OfferInput input);

        Task ManufactureAsync(Guid id, ManufactureInput input);

        Task CompleteAsync(Guid id, CompleteInput input);

        Task<List<OrderLogDto>> GetOrderLogsAsync(string orderNo);

        /// <summary>
        /// 获取供应商报价
        /// </summary>
        /// <param name="subOrderId"></param>
        /// <returns></returns>
        Task GetSupplierCost(Guid subOrderId);

        /// <summary>
        /// 同步订单至订单中心
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <returns></returns>
        Task<ApiHttpResponseDto> SyncOrderInfoAsync(Guid id);

        /// <summary>
        /// 根据订单编号从订单中心获取订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiHttpResponseDto> GetOrderInfoByNoAsync(Guid id);

        /// <summary>
        /// 根据订单编号从订单中心获取订单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<object> GetOrderInfoDetailByNoAsync(Guid id);

        /// <summary>
        /// 修改订单执行状态   确认下单之后,应该是正常执行还是暂停执行(暂停执行则订单生产会暂停)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ApiHttpResponseDto> ModifyOrderExecutionStatusAsync(Guid id, UpdateOrderStopInput input);

        /// <summary>
        /// 订单支付确认
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ApiHttpResponseDto> ModifyOrderPayStatusAsync(Guid id, UpdateOrderPayStatusInput input);

        /// <summary>
        /// 修改订单文件信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ApiHttpResponseDto> ModifyOrderProductFileAsync(Guid id, UpdateOrderProductFileInput input);

        /// <summary>
        /// 取消订单中心的订单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ApiHttpResponseDto> CancelOrderInfoAsync(Guid id, CancelSheetInput input);

        /// <summary>
        /// 第三方调用服务分发
        /// </summary>
        /// <param name="apiSheetMetalDispatcher"></param>
        /// <returns></returns>
        Task<ApiHttpResponseDto> SheetMetalDispatcher(ApiSheetMetalDispatcherInput apiSheetMetalDispatcher);

        /// <summary>
        /// 支付回调
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        Task PayNotifyAsync(string orderNo);

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="orderNo">子订单号</param>
        /// <param name="input"><see cref="CheckInput"/></param>
        /// <returns></returns>
        Task CheckAsync(string orderNo, CheckInput input);

        /// <summary>
        /// 报价
        /// </summary>
        /// <param name="orderNo">子订单号</param>
        /// <param name="input"><see cref="OfferInput"/></param>
        /// <returns></returns>
        Task OfferAsync(string orderNo, OfferInput input);

        /// <summary>
        /// 投产
        /// </summary>
        /// <param name="orderNo">子订单号</param>
        /// <param name="input"><see cref="ManufactureInput"/></param>
        /// <returns></returns>
        Task ManufactureAsync(string orderNo, ManufactureInput input);

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="orderNo">子订单号</param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeliverAsync(string orderNo, DeliverDto input);

        Task Orderdelivery(string orderNo, OrderDeliveryDto input);

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderNo">子订单号</param>
        /// <param name="input"><see cref="CancelInput"/></param>
        /// <returns></returns>
        Task CancelAsync(string orderNo, CancelInput input);

        /// <summary>
        /// 设置交期
        /// </summary>
        /// <param name="orderNo">子订单号</param>
        /// <param name="input"><see cref="SetDeliveryDayDto"/></param>
        /// <returns></returns>
        Task SetDeliveryDaysAsync(string orderNo, SetDeliveryDayDto input);
        Task CompleteAsync(string orderNo);
        /// <summary>
        /// 同步订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task SyncOrderInfoAsync_DeliverCenters(Guid id);
        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task FinishOrder_DeliverCenters(Guid id);
        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task PaymentAsync_DeliverCenters(Guid id);
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        Task CancelAsync_DeliverCenters(Guid id);
        Task OffinePaymentAsync(Guid id, OfflinePaymentDto input);
        Task<SubOrderDto> GetSubOrderByOrderNo(string orderNo);
    }
}