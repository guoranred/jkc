using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace Jiepei.ERP.Orders.Orders
{
    [RemoteService(Name = OrderRemoteServiceConsts.RemoteServiceName)]
    [Route("api/orders/mold-order")]
    public class MoldOrderController : OrdersController
    {
        /*
        private readonly IMoldOrderAppService _moldOrderAppService;

        public MoldOrderController(IMoldOrderAppService moldOrderAppService)
        {
            _moldOrderAppService = moldOrderAppService;
        }

        /// <summary>
        /// 获取模具订单
        /// </summary>
        /// <param name="id">订单 Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<MoldOrderDto> GetAsync(Guid id)
        {
            return await _moldOrderAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取模具订单
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <returns></returns>
        [HttpGet("{orderNo}")]
        public async Task<List<MoldOrderDto>> GetByOrderNoAsync(string orderNo)
        {
            return await _moldOrderAppService.GetByOrderNoAsync(orderNo);
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<MoldOrderDto>> GetListAsync(GetMoldOrderListDto input)
        {
            return await _moldOrderAppService.GetListAsync(input);
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="id">订单 Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}/check")]
        public async Task<IActionResult> CheckAsync(Guid id, CheckInput input)
        {
            await _moldOrderAppService.PutCheckAsync(id, input);
            return NoContent();
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="id">订单 Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelAsync(Guid id, CancelInput input)
        {
            await _moldOrderAppService.PutCancelAsync(id, input);
            return NoContent();
        }

        /// <summary>
        /// 报价
        /// </summary>
        /// <param name="id">订单 Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}/offer")]
        public async Task<IActionResult> OfferAsync(Guid id, OfferInput input)
        {
            await _moldOrderAppService.PutOfferAsync(id, input);
            return NoContent();
        }

        /// <summary>
        /// 投产
        /// </summary>
        /// <param name="id">订单 Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}/manufacture")]
        public async Task<IActionResult> ManufactureAsync(Guid id, ManufactureInput input)
        {
            await _moldOrderAppService.PutManufactureAsync(id, input);
            return NoContent();
        }

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}/deliver")]
        public async Task<IActionResult> DeliverAsync(Guid id, DeliverInput input)
        {
            await _moldOrderAppService.PutDeliverAsync(id, input);
            return NoContent();
        }

        /// <summary>
        /// 订单完成
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> PutCompleteAsync(Guid id, CompleteInput input)
        {
            await _moldOrderAppService.PutCompleteAsync(id, input);
            return NoContent();
        }

        /// <summary>
        /// 设计变更
        /// </summary>
        [HttpPut("{id}/design-change")]
        public async Task<IActionResult> PutDesignChange(Guid id, DesignChangeInput input)
        {
            await _moldOrderAppService.PutDesignChange(id, input);
            return NoContent();
        }

        /// <summary>
        /// 获取模具操作记录
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <returns></returns>
        [HttpGet("mold-order-flows")]
        public async Task<List<MoldOrderFlowDto>> GetMoldOrderFlowsAsync(string orderNo)
        {
            return await _moldOrderAppService.GetMoldOrderFlowsAsync(orderNo);
        }

        /// <summary>
        /// 搜索模具信息
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        [HttpGet("mold-order-search")]
        public async Task<List<MoldOrderDto>> GetMoldOrderSearch(SearchInput input)
        {
            return await _moldOrderAppService.GetMoldOrderSearch(input);
        }
        */
    }
}
