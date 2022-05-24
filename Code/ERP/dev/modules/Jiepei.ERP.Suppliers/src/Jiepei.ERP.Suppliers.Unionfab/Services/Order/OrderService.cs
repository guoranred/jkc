using Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Order
{
    public class OrderService : CommonService
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UnionfabCommonResponseBase> CreateAsync(CreateOrderRequest request)
        {
            string targetUrl = "/order";
            var response = await SupplierUnionfabApiRequester.RequestAsync<UnionfabCommonResponseBase>(targetUrl, HttpMethod.Post, request);
            if (response.Status == ResponseStatus.Error)
                throw new UserFriendlyException("请求优联创建订单接口出错",
                                                response.ErrorMessage.Code,
                                                response.ErrorMessage.Reason);
            return response;
        }

        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UnionfabCommonResponseBase> CloseAsync(CloseOrderRequest request)
        {
            string targetUrl = $"/order/{request.Code}/close";
            var response = await SupplierUnionfabApiRequester.RequestAsync<UnionfabCommonResponseBase>(targetUrl, HttpMethod.Post);
            if (response.Status == ResponseStatus.Error)
                throw new UserFriendlyException("请求优联关闭订单接口出错",
                                                response.ErrorMessage.Code,
                                                response.ErrorMessage.Reason);
            return response;
        }

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UnionfabCommonResponse<OpenOrder>> GetAsync(GetOrderRequest request)
        {
            string targetUrl = $"/order/{request.Code}";
            var response = await SupplierUnionfabApiRequester.RequestAsync<UnionfabCommonResponse<OpenOrder>>(targetUrl, HttpMethod.Get, request);
            if (response.Status == ResponseStatus.Error)
                throw new UserFriendlyException("请求优联获取订单接口出错",
                                                response.ErrorMessage.Code,
                                                response.ErrorMessage.Reason);
            return response;
        }

        /// <summary>
        /// 确认订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UnionfabCommonResponseBase> ConfirmAsync(ConfirmOrderRequest request)
        {
            string targetUrl = $"/order/{request.Code}/confirm";
            var response = await SupplierUnionfabApiRequester.RequestAsync<UnionfabCommonResponse<OpenOrder>>(targetUrl, HttpMethod.Post);
            if (response.Status == ResponseStatus.Error)
                throw new UserFriendlyException("请求优联确认订单接口出错",
                                                response.ErrorMessage.Code,
                                                response.ErrorMessage.Reason);
            return response;
        }
    }
}

