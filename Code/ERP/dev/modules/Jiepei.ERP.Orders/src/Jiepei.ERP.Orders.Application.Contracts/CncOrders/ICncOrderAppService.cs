using Jiepei.ERP.Orders.CncOrders.Dtos;
using Jiepei.ERP.Orders.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Orders.CncOrders
{
    public interface ICncOrderAppService : IApplicationService
    {
        Task<List<CncOrderDto>> GetByOrderNo(string mainOrderNo);
        /// <summary>
        /// 审核订单
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task PutCheckAsync(Guid id, CheckInput input);
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task PutCancelAsync(Guid id, CancelInput input);
        /// <summary>
        /// 报价
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task PutOfferAsync(Guid id, OfferInput input);
        /// <summary>
        /// 订单投产
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task PutManufactureAsync(Guid id, ManufactureInput input);
        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task PutDeliverAsync(Guid id, DeliverInput input);
        /// <summary>
        /// 订单完成
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task PutCompleteAsync(Guid id, CompleteInput input);
        /// <summary>
        /// 分页获取订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<CncOrderDto>> GetAllAsync(GetCncOrderListDto input);
        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CncOrderDto> GetAsync(Guid id);

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OrderBaseDto> CreateAsync(CreateOrderExtraDto input);
        Task<List<CncOrderFlowDto>> GetCncOrderFlows(string orderNo);

    }
}
