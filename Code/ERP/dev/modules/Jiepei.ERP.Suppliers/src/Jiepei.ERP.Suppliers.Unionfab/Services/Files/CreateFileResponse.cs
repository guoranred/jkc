using Newtonsoft.Json;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Files
{
    public class CreateFileResponse
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        [JsonProperty("fileId")]
        public string FileId { get; set; }
        /// <summary>
        /// 处理ID(暂不需要使用)
        /// </summary>
        [JsonProperty("processId")]
        public string ProcessId { get; set; }
    }
}
