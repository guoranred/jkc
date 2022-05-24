using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Order
{
    /// <summary>
    /// 订单条目数据模型
    /// </summary>
    public class OpenOrderItemData
    {
        /// <summary>
        /// 使用的SPU
        /// </summary>
        [JsonProperty("spuId")]
        public string SpuId { get; set; }

        /// <summary>
        /// 条目价格
        /// </summary>
        [JsonProperty("priceCorrection")]
        public decimal PriceCorrection { get; set; }

        /// <summary>
        /// 条目备注
        /// </summary>
        [JsonProperty("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 条目打印的文件ID(一个条目只允许传一个OpenOrderFileData)
        /// </summary>
        [JsonProperty("printFiles")]
        public List<OpenOrderFileData> PrintFiles { get; set; }
    }
}
