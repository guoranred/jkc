using Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Order
{
    public class GetOrderRequest : UnionfabCommonRequest
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Code { get; protected set; }

        protected GetOrderRequest() { }
        public GetOrderRequest(string code)
        {
            Code = code;
        }
    }
}
