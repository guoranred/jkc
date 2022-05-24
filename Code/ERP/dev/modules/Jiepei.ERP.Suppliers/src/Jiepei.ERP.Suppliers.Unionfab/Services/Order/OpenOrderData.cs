using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Order
{
    /// <summary>
    /// 订单信息
    /// </summary>
    public class OpenOrderData
    {
        /// <summary>
        /// 订单条目
        /// </summary>
        [JsonProperty("items")]
        public List<OpenOrderItemData> Items { get; set; }

        /// <summary>
        /// 订单级备注
        /// </summary>
        [JsonProperty("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 订单备注附件列表
        /// </summary>
        [JsonProperty("remarkFileIds")]
        public List<string> RemarkFileIds { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        [JsonProperty("payMethod")]
        public string PayMethod { get; set; }

        /// <summary>
        /// 订单其他属性
        /// </summary>
        [JsonProperty("attr")]
        public Dictionary<string, object> Attr { get; set; }
    }
}
