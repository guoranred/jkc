using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Order
{
    public class OpenOrder
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 询价单详情数据信息
        /// </summary>
        [JsonProperty("items")]
        public List<OpenOrderItem> Items { get; set; }

        /// <summary>
        /// 询价单交付
        /// </summary>
        [JsonProperty("deliver")]
        public OpenOrderDeliver Deliver { get; set; }

        /// <summary>
        /// 订单价格信息
        /// </summary>
        [JsonProperty("price")]
        public OpenOrderPrice Price { get; set; }

        /// <summary>
        /// 订单名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 订单单号
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// 订单备注
        /// </summary>
        [JsonProperty("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 订单备注附件列表
        /// </summary>
        [JsonProperty("remarkFileIds")]
        public List<string> RemarkFileIds { get; set; }

        /// <summary>
        /// 订单提交时间
        /// </summary>
        [JsonProperty("submitAt")]
        public DateTime? SubmitAt { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [JsonProperty("status")]
        public InquiryOrderStatus Status { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        [JsonProperty("payMethod")]
        public string PayMethod { get; set; }

        /// <summary>
        /// 当前生产进度
        /// </summary>
        [JsonProperty("progress")]
        public decimal? Progress { get; set; }

        /// <summary>
        /// 当前生产剩余时间
        /// </summary>
        [JsonProperty("estimatedTime")]
        public long? EstimatedTime { get; set; }

        /// <summary>
        /// 其他属性
        /// </summary>
        [JsonProperty("attr")]
        public Dictionary<string, object> Attr { get; set; }

        /// <summary>
        /// 订单交货时间
        /// </summary>
        [JsonProperty("deliveredAt")]
        public DateTime? DeliveredAt { get; set; }
    }
}
