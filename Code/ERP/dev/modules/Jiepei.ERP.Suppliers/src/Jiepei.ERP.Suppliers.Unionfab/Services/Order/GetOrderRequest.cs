using Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Order
{
    public class ConfirmOrderRequest : UnionfabCommonRequest
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Code { get; protected set; }

        protected ConfirmOrderRequest() { }
        public ConfirmOrderRequest(string code)
        {
            Code = code;
        }
    }
}
