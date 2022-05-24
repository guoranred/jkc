using Jiepei.ERP.Orders.Orders.Dtos;
using Jiepei.ERP.Orders.SubOrders.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Orders.Orders
{
    public interface IOrderAppService : IApplicationService
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OrderBaseDto> CreateAsync(CreateOrderExtraDto input);

        Task<List<OrderDto>> GetListAsync(List<Guid> ids);


        Task<PagedResultDto<CustomerOrderListDto>> GetCustomerSheetMatelOrderListAsync(GetCustomerSheetMatelOrderListInput input);

        Task<PagedResultDto<CustomerOrderListDto>> GetCustomerCncOrderListAsync(GetCustomerCncOrderListInput input);

        Task<PagedResultDto<CustomerOrderListDto>> GetCustomer3DOrderListAsync(GetCustomer3DOrderListInput input);
        Task<CustomerOrderDetialListDto> GetCustomerSheetMetalOrderDetailListAsync(Guid id);
        Task<CustomerOrderDetialListDto> GetCustomer3DOrderDetailListAsync(Guid id);
        Task<CustomerOrderDetialListDto> GetCustomerCncOrderDetailListAsync(Guid id);

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task CancelAsync(Guid id);

        /// <summary>
        /// 收货
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task CompleteAsync(Guid id);

        /// <summary>
        /// 订单数量统计
        /// </summary>
        /// <returns></returns>
        Task<CustomerOrderCountDto> GetCustomerOrderCountAsync(EnumOrderType enumOrderType);

        /// <summary>
        /// 修改订单（重新上传文件）
        /// </summary>
        /// <returns></returns>
        Task<bool> UpdateOrderFileAsync(UpdateOrderInput input);
        Task PayNotifyAsync(string orderNo);
    }
}
