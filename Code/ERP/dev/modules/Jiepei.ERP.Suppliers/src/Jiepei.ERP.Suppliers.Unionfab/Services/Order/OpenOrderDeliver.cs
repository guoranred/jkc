using Newtonsoft.Json;
using System;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Order
{
    public class OpenOrderDeliver
    {
        /// <summary>
        /// 客户名称
        /// </summary>
        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        /// <summary>
        /// 客户代表
        /// </summary>
        [JsonProperty("representativeName")]
        public string RepresentativeName { get; set; }

        /// <summary>
        /// 客户代表联系方式
        /// </summary>
        [JsonProperty("representativeContactInfo")]
        public string RepresentativeContactInfo { get; set; }

        /// <summary>
        /// 客户地址
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        [JsonProperty("receiverAddress")]
        public string ReceiverAddress { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        [JsonProperty("recipient")]
        public string Recipient { get; set; }

        /// <summary>
        /// 收货人联系电话
        /// </summary>
        [JsonProperty("contactInfo")]
        public string ContactInfo { get; set; }

        /// <summary>
        /// 物流单号
        /// </summary>
        [JsonProperty("expressNumber")]
        public string ExpressNumber { get; set; }

        /// <summary>
        /// 发货人
        /// </summary>
        [JsonProperty("consignorName")]
        public string ConsignorName { get; set; }

        /// <summary>
        /// 物流公司
        /// </summary>
        [JsonProperty("expressCompany")]
        public string ExpressCompany { get; set; }

        /// <summary>
        /// 发货日期
        /// </summary>
        [JsonProperty("deliveredAt")]
        public DateTime? DeliveredAt { get; set; }
        /// <summary>
        /// 预计运送时间(天)
        /// </summary>
        [JsonProperty("transportDays")]
        public decimal? TransportDays { get; set; }

        /// <summary>
        /// 询价单ID
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// 最晚发货时间
        /// </summary>
        [JsonProperty("lastDeliveredAt")]
        public DateTime? LastDeliveredAt { get; set; }
    }
}
