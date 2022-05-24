using Jiepei.ERP.DeliverCentersClient.Dto;
using Jiepei.ERP.Orders.Admin.Application.Contracts.Orders.Dtos;
using Jiepei.ERP.Orders.Admin.Application.Contracts.Orders.Dtos.DeliverCenters;
using Jiepei.ERP.Orders.Admin.Orders;
using Jiepei.ERP.Orders.SubOrders.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Admin.HttpApi.Controllers
{
    /// <summary>
    /// 订单管理
    /// </summary>
    [RemoteService(Name = OrdersAdminRemoteServiceConsts.RemoteServiceName)]
    [Area("OrdersAdmin")]
    [Route("api/admin/orders")]
    public class OrderManagementController : OrdersAdminController
    {
        private readonly IOrderManagementAppService _orderManagementAppService;
        private readonly IThreeDPrintLogisticsAppService _threeDPrintLogisticsAppService;

        public OrderManagementController(IOrderManagementAppService orderManagementAppService, IThreeDPrintLogisticsAppService threeDPrintLogisticsAppService)
        {
            _orderManagementAppService = orderManagementAppService;
            _threeDPrintLogisticsAppService = threeDPrintLogisticsAppService;
        }

        /// <summary>
        /// 获取客服订单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("customer-service/orders")]
        public async Task<PagedResultDto<CustomerServiceOrderDto>> ListAsync(GetCustomerServiceOrderInput input)
        {
            return await _orderManagementAppService.GetCustomerServiceOrderListAsync(input);
        }

        /// <summary>
        /// 获取客服订单详情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        [HttpGet("customer-service/orders/{orderType}/{id}")]
        public async Task<CustomerServiceOrderDetailDto> GetCustomerServiceOrderDetail(Guid id, EnumOrderType orderType)
        {
            return await _orderManagementAppService.GetCustomerServiceOrderDetail(id, orderType);
        }

        [HttpGet("customer-service/orders/{supplierOrderCode}")]
        public async Task<PagedResultDto<CustomerServiceOrderDto>> GetOrdersBySupplierCode(string supplierOrderCode)
        {
            return await _orderManagementAppService.GetOrdersBySupplierCode(supplierOrderCode);
        }

        /// <summary>
        /// 获取3d订单物流信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("threed-print/logistics")]
        public async Task<PagedResultDto<ThreeDPrintLogisticsDto>> GetThreeDPrintLogisticsListAsync(GetThreeDPrintLogisticsInput input)
        {
            return await _threeDPrintLogisticsAppService.GetThreeDPrintLogisticsListAsync(input);
        }

        /// <summary>
        /// 获取状态统计3d订单物流信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("threed-print/logistics/count-by-status")]
        public async Task<Dictionary<string, long>> GetThreeDPrintLogisticsCountByStausAsync()
        {
            return await _threeDPrintLogisticsAppService.GetThreeDPrintLogisticsCountByStausAsync();
        }

        /// <summary>
        /// 根据订单号获取
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        [HttpGet("threed-print/{orderNo}/boms")]
        public async Task<IEnumerable<SubOrderThreeDItemDto>> GetThreeDPrintBomListAsync(string orderNo)
        {
            return await _threeDPrintLogisticsAppService.GetThreeDPrintBomList(orderNo);
        }

        /// <summary>
        /// 编辑客服备注
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}/edit-note")]
        public async Task<object> EditNoteAsync(Guid id, EditNoteInput input)
        {
            return await _orderManagementAppService.EditNoteAsync(id, input);
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="id">子订单 Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task CheckAsync(Guid id, CheckInput input)
        {
            await _orderManagementAppService.CheckAsync(id, input);
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("cancel/{id}")]
        public async Task CancelAsync(Guid id, CancelInput input)
        {
            await _orderManagementAppService.CancelAsync(id, input);
        }
        /// <summary>
        /// 报价
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("offer/{id}")]
        public async Task OfferAsync(Guid id, OfferInput input)
        {
            await _orderManagementAppService.OfferAsync(id, input);
        }

        /// <summary>
        /// 投产
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("manufacture/{id}")]
        public async Task ManufactureAsync(Guid id, ManufactureInput input)
        {
            await _orderManagementAppService.ManufactureAsync(id, input);
        }
        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("complete/{id}")]
        public async Task CompleteAsync(Guid id, CompleteInput input)
        {
            await _orderManagementAppService.CompleteAsync(id, input);
        }

        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpPut("inbound")]
        public async Task ChangeInboundNumAsync(List<ChangeInboundNumInput> inputs)
        {
            await _threeDPrintLogisticsAppService.ChangeInboundNumAsync(inputs);
        }

        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="id">子订单 Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("outbound/{id}")]
        public async Task DeliverAsync(Guid id, DeliverDto input)
        {
            await _threeDPrintLogisticsAppService.DeliverAsync(id, input);
        }

        /// <summary>
        /// 订单日志
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        [HttpGet("order-logs/{orderNo}")]
        public async Task<List<OrderLogDto>> GetOrderLogsAsync(string orderNo)
        {
            return await _orderManagementAppService.GetOrderLogsAsync(orderNo);
        }

        /// <summary>
        /// 获取供应商报价
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("suppliercost/{id}")]
        public async Task GetSupplierCost(Guid id)
        {
            await _orderManagementAppService.GetSupplierCost(id);
        }

        /// <summary>
        /// 同步订单至订单中心
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <returns></returns>
        [HttpGet("jkc/{id}")]
        public async Task<ApiHttpResponseDto> SyncOrderInfoAsync(Guid id)
        {
            return await _orderManagementAppService.SyncOrderInfoAsync(id);
        }

        /// <summary>
        /// 根据订单编号从订单中心获取订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("jkc/{id}/orderlist")]
        public async Task<ApiHttpResponseDto> GetOrderInfoByNoAsync(Guid id)
        {
            return await _orderManagementAppService.GetOrderInfoByNoAsync(id);
        }

        /// <summary>
        ///  根据订单编号从订单中心获取订单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("jkc/{id}/orderDetail")]
        public async Task<object> GetOrderInfoDetailByNoAsync(Guid id)
        {
            return await _orderManagementAppService.GetOrderInfoDetailByNoAsync(id);
        }

        /// <summary>
        /// 修改订单执行状态   确认下单之后,应该是正常执行还是暂停执行(暂停执行则订单生产会暂停)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("jkc/{id}/order-execution-status")]
        public async Task<ApiHttpResponseDto> ModifyOrderExecutionStatusAsync(Guid id, UpdateOrderStopInput input)
        {
            return await _orderManagementAppService.ModifyOrderExecutionStatusAsync(id, input);
        }


        /// <summary>
        /// 订单支付确认
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("jkc/{id}/order-execution-pay")]
        public async Task<ApiHttpResponseDto> ModifyOrderPayStatusAsync(Guid id, UpdateOrderPayStatusInput input)
        {
            return await _orderManagementAppService.ModifyOrderPayStatusAsync(id, input);

        }

        /// <summary>
        /// 修改订单文件信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("jkc/{id}/order-execution-file")]
        public async Task<ApiHttpResponseDto> ModifyOrderProductFileAsync(Guid id, UpdateOrderProductFileInput input)
        {
            return await _orderManagementAppService.ModifyOrderProductFileAsync(id, input);
        }

        /// <summary>
        /// 取消订单中心的订单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("jkc/{id}/cancel")]
        public async Task<ApiHttpResponseDto> CancelOrderInfoAsync(Guid id, CancelSheetInput input)
        {
            return await _orderManagementAppService.CancelOrderInfoAsync(id, input);
        }

        /// <summary>
        ///第三方调用服务分发
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("jkc/dispatcher")]
        [AllowAnonymous]

        public async Task<ApiHttpResponseDto> SheetMetalDispatcher(ApiSheetMetalDispatcherInput input)
        {
            return await _orderManagementAppService.SheetMetalDispatcher(input);
        }

        /// <summary>
        ///同步交付中台收货
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("delivercenters/{id}/complete")]
        public async Task FinishOrder_DeliverCenters(Guid id)
        {
            await _orderManagementAppService.FinishOrder_DeliverCenters(id);
        }
        /// <summary>
        ///同步交付中台支付
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("delivercenters/{id}/pay")]
        public async Task PaymentAsync_DeliverCenters(Guid id)
        {
            await _orderManagementAppService.PaymentAsync_DeliverCenters(id);
        }
        /// <summary>
        ///同步交付中台下单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("delivercenters/{id}/syncorder")]
        public async Task SyncOrderInfoAsync_DeliverCenters(Guid id)
        {
            await _orderManagementAppService.SyncOrderInfoAsync_DeliverCenters(id);
        }
        /// <summary>
        ///同步交付中台取消
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("delivercenters/{id}/cancel")]
        public async Task CancelAsync_DeliverCenters(Guid id)
        {
            await _orderManagementAppService.CancelAsync_DeliverCenters(id);
        }

        [HttpPut("{id}/offline-payment")]
        public async Task OffinePaymentAsync(Guid id, OfflinePaymentDto input)
        {
            await _orderManagementAppService.OffinePaymentAsync(id, input);
        }
    }
}
