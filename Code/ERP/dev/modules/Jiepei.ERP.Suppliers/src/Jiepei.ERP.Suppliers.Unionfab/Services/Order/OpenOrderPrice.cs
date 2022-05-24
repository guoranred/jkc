using Newtonsoft.Json;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Order
{
    public class OpenOrderPrice
    {
        /// <summary>
        /// 后处理费用
        /// </summary>
        [JsonProperty("handleFee")]
        public decimal? HandleFee { get; set; }

        /// <summary>
        /// 邮费
        /// </summary>
        [JsonProperty("postage")]
        public decimal? Postage { get; set; }

        /// <summary>
        /// 包装费
        /// </summary>
        [JsonProperty("packingFee")]
        public decimal? PackingFee { get; set; }

        /// <summary>
        /// 附加费
        /// </summary>
        [JsonProperty("surcharge")]
        public decimal? Surcharge { get; set; }

        /// <summary>
        /// 材料费
        /// </summary>
        [JsonProperty("materialFee")]
        public decimal? MaterialFee { get; set; }

        /// <summary>
        /// 税率(千分税率)
        /// </summary>
        [JsonProperty("taxRate")]
        public int? TaxRate { get; set; }

        /// <summary>
        /// 含税价格
        /// </summary>
        [JsonProperty("totalPriceWithTax")]
        public decimal? TotalPriceWithTax { get; set; }

        /// <summary>
        /// 不含税价格
        /// </summary>
        [JsonProperty("totalPriceWithoutTax")]
        public decimal? TotalPriceWithoutTax { get; set; }

        /// <summary>
        /// 附加费备注
        /// </summary>
        [JsonProperty("surchargeRemark")]
        public string SurchargeRemark { get; set; }
    }
}
