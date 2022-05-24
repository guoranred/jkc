using Newtonsoft.Json;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Order
{
    /// <summary>
    /// 订单交付信息
    /// </summary>
    public class OpenOrderDeliverData
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
    }
}
