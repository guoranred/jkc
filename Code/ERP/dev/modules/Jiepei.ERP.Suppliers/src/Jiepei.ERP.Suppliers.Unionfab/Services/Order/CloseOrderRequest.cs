using Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Order
{
    public class CloseOrderRequest : UnionfabCommonRequest
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Code { get; protected set; }

        protected CloseOrderRequest() { }
        public CloseOrderRequest(string code)
        {
            Code = code;
        }
    }
}
