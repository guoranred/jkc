using Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Files
{
    public class CreateFileRequest : UnionfabCommonRequest
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        [JsonIgnore]
        public string Code { get; protected set; }

        /// <summary>
        /// 文件URL，公网可访问，在订单周期内可访问
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; protected set; }

        /// <summary>
        /// MODEL: 模型文件；IMAGE：图片；DOC：备注类文件
        /// </summary>
        [JsonProperty("fileType")]
        public string FileType { get; protected set; }
        /// <summary>
        /// 文件名
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; protected set; }
        /// <summary>
        /// 文件 md5，用于文件处理校验
        /// </summary>
        [JsonProperty("md5")]
        public string Md5 { get; protected set; }
        /// <summary>
        /// 文件格式 stl, obj, stp, iges...
        /// </summary>
        [JsonProperty("format")]
        public string Format { get; protected set; }
        /// <summary>
        /// 文件其他属性，可为空，比如： volume 体积/md5/文件类型/缩略图等等
        /// </summary>
        [JsonProperty("attributes")]
        public Dictionary<string, object> Attributes { get; protected set; }
        /// <summary>
        /// 可选
        /// </summary>
        [JsonProperty("stlPersistCredentials")]
        public StlPersistCredentials StlPersistCredentials { get; protected set; }
        /// <summary>
        /// 可选
        /// </summary>
        [JsonProperty("previewUrlPersistCredentials")]
        public PreviewUrlPersistCredentials PreviewUrlPersistCredentials { get; protected set; }

        protected CreateFileRequest() { }

        public CreateFileRequest(string code,
                                 string url,
                                 string fileType,
                                 string name,
                                 string md5,
                                 string format,
                                 Dictionary<string, object> attributes = null,
                                 StlPersistCredentials stlPersistCredentials = null,
                                 PreviewUrlPersistCredentials previewUrlPersistCredentials = null)
        {
            Code = code;
            Url = url;
            FileType = fileType;
            Name = name;
            Md5 = md5;
            Format = format;
            Attributes = attributes;
            StlPersistCredentials = stlPersistCredentials;
            PreviewUrlPersistCredentials = previewUrlPersistCredentials;
        }
    }
}
