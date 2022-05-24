using Newtonsoft.Json;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Files
{
    public class StlPersistCredentials
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("uploadUrl")]
        public string UploadUrl { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }
    }
}
