using Newtonsoft.Json;
using System;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Files
{
    public class GetFileResponse
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 文件MD5
        /// </summary>
        [JsonProperty("md5")]
        public string Md5 { get; set; }
        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; set; }
        /// <summary>
        /// 文件属性
        /// </summary>
        [JsonProperty("attr")]
        public FileAttr Attr { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
