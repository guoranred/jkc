using Jiepei.ERP.Orders.Admin.Orders;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Orders.Admin
{
    public interface ISheetMetalOrderGroupService : IApplicationService
    {
        Task<Tuple<ApiHttpResponseDto, MethodBaseInfo>> UpdateOrderGroupPrice(string data);
        Task<Tuple<ApiHttpResponseDto, MethodBaseInfo>> UpdateOrderMainReceiver(string data);
        Task<Tuple<ApiHttpResponseDto, MethodBaseInfo>> UpdateOrderDetailDeliveryDay(string data);
        Task<Tuple<ApiHttpResponseDto, MethodBaseInfo>> DeliverProducts(string data);
        Task<Tuple<ApiHttpResponseDto, MethodBaseInfo>> UpdateOrderDetailStatus(string data);
        Task<Tuple<ApiHttpResponseDto, MethodBaseInfo>> UpdateOrderDetailTotalMoney(string data);
        Task<Tuple<ApiHttpResponseDto, MethodBaseInfo>> UpdateOrderProductNum(string data);
    }
}
