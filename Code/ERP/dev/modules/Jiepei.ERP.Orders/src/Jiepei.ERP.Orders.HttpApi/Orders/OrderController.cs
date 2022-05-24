using Jiepei.ERP.Orders.Orders.Dtos;
using Jiepei.ERP.Orders.SubOrders.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Orders
{
    [RemoteService(Name = OrderRemoteServiceConsts.RemoteServiceName)]
    [Route("api/orders/order")]
    public class OrderController : OrdersController
    {
        private readonly IOrderAppService _orderAppService;
        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        /// <summary>
        /// 前台创建订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<OrderBaseDto> CreateAsync(CreateOrderExtraDto input)
        {
            return await _orderAppService.CreateAsync(input);
        }

        /// <summary>
        /// 我的订单--3d订单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("customerorder-3d")]
        public async Task<PagedResultDto<CustomerOrderListDto>> GetCustomer3DOrderListAsync(GetCustomer3DOrderListInput input)
        {
            return await _orderAppService.GetCustomer3DOrderListAsync(input);
        }
        /// <summary>
        /// 我的订单--钣金订单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("customerorder-sheetmatel")]
        public async Task<PagedResultDto<CustomerOrderListDto>> GetCustomerSheetMatelOrderListAsync(GetCustomerSheetMatelOrderListInput input)
        {
            return await _orderAppService.GetCustomerSheetMatelOrderListAsync(input);
        }
        /// <summary>
        ///  我的订单--cnc订单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("customerorder-cnc")]
        public async Task<PagedResultDto<CustomerOrderListDto>> GetCustomerCncOrderListAsync(GetCustomerCncOrderListInput input)
        {
            return await _orderAppService.GetCustomerCncOrderListAsync(input);
        }
        /// <summary>
        /// 我的订单--3d订单列表详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("customerorder-3ddetial/{id}")]
        public async Task<CustomerOrderDetialListDto> GetCustomer3DOrderDetailListAsync(Guid id)
        {
            return await _orderAppService.GetCustomer3DOrderDetailListAsync(id);
        }
        /// <summary>
        /// 我的订单--钣金订单列表详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("customerorder-sheetdetial/{id}")]
        public async Task<CustomerOrderDetialListDto> GetCustomerSheetMetalOrderDetailListAsync(Guid id)
        {
            return await _orderAppService.GetCustomerSheetMetalOrderDetailListAsync(id);
        }
        /// <summary>
        /// 我的订单--cnc订单列表详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("customerorder-cncdetial/{id}")]
        public async Task<CustomerOrderDetialListDto> GetCustomerCncOrderDetailListAsync(Guid id)
        {
            return await _orderAppService.GetCustomerCncOrderDetailListAsync(id);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("cancel/{id}")]
        public async Task GetCancelOrder(Guid id)
        {
            await _orderAppService.CancelAsync(id);
        }

        /// <summary>
        /// 收货订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("complete/{id}")]
        public async Task CompleteAsync(Guid id)
        {
            await _orderAppService.CompleteAsync(id);
        }

        /// <summary>
        /// 我的订单-用户3d订单数量统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("d3ordercount")]
        public async Task<CustomerOrderCountDto> GetCustomer3DOrderCountAsync()
        {
            return await _orderAppService.GetCustomerOrderCountAsync(EnumOrderType.Print3D);
        }
        /// <summary>
        /// 我的订单-用户钣金订单数量统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("sheetmetalordercount")]
        public async Task<CustomerOrderCountDto> GetCustomerSheetMetalOrderCountAsync()
        {
            return await _orderAppService.GetCustomerOrderCountAsync(EnumOrderType.SheetMetal);
        }
        /// <summary>
        /// 我的订单-用户Cnc订单数量统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("cncordercount")]
        public async Task<CustomerOrderCountDto> GetCustomerCncOrderCountAsync()
        {
            return await _orderAppService.GetCustomerOrderCountAsync(EnumOrderType.Cnc);
        }

        /// <summary>
        /// 修改订单（重新上传文件）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("updatefile")]
        public async Task<bool> UpdateOrderFileAsync(UpdateOrderInput input)
        {
            return await _orderAppService.UpdateOrderFileAsync(input);
        }
    }
}
