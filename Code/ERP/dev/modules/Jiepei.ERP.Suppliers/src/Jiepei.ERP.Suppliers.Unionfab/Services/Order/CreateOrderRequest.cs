using Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models;
using Newtonsoft.Json;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Order
{
    public class CreateOrderRequest : UnionfabCommonRequest
    {
        /// <summary>
        /// 订单交付信息
        /// </summary>
        [JsonProperty("deliver")]
        public OpenOrderDeliverData Deliver { get; protected set; }

        /// <summary>
        /// 订单信息
        /// </summary>
        [JsonProperty("order")]
        public OpenOrderData Order { get; protected set; }

        /// <summary>
        /// 订单号
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; protected set; }

        protected CreateOrderRequest() { }

        public CreateOrderRequest(OpenOrderDeliverData deliver, OpenOrderData order, string code)
        {
            Deliver = deliver;
            Order = order;
            Code = code;
        }
    }
}
