using Newtonsoft.Json;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Order
{
    /// <summary>
    /// 订单条目文件数据模型
    /// </summary>
    public class OpenOrderFileData
    {
        /// <summary>
        /// 打印文件ID
        /// </summary>
        [JsonProperty("fileId")]
        public string FileId { get; set; }

        /// <summary>
        /// 预览文件ID
        /// </summary>
        [JsonProperty("preViewId")]
        public string PreViewId { get; set; }

        /// <summary>
        /// 打印数量
        /// </summary>
        [JsonProperty("count")]
        public int? Count { get; set; }

        /// <summary>
        /// 文件体积(立方毫米)(可选参数，填写此参数后，以此参数计算材料费)
        /// </summary>
        [JsonProperty("volume")]
        public decimal? Volume { get; set; }

        /// <summary>
        /// 支撑体积(立方毫米)(可选参数，填写此参数后，以此参数计算材料费)
        /// </summary>
        [JsonProperty("supportVolume")]
        public decimal? SupportVolume { get; set; }

        /// <summary>
        /// 后处理方式(多个方式请使用,隔开)
        /// </summary>
        [JsonProperty("handleMethod")]
        public string HandleMethod { get; set; }

        /// <summary>
        /// 后处理描述
        /// </summary>
        [JsonProperty("handleMethodDesc")]
        public string HandleMethodDesc { get; set; }
    }
}
