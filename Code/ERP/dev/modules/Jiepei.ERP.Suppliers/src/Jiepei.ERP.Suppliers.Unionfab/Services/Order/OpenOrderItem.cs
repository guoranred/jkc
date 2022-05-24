using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Order
{
    public class OpenOrderItem
    {
        public string Id { get; set; }
        /// <summary>
        /// 使用的SPU
        /// </summary>
        [JsonProperty("spuId")]
        public string SpuId { get; set; }

        /// <summary>
        /// 条目价格
        /// </summary>
        [JsonProperty("priceCorrection")]
        public decimal? PriceCorrection { get; set; }

        /// <summary>
        /// 条目打印的文件ID(一个条目只允许传一个OpenOrderFileData)
        /// </summary>
        [JsonProperty("printFiles")]
        public List<OpenOrderFileData> PrintFiles { get; set; }
    }
}
