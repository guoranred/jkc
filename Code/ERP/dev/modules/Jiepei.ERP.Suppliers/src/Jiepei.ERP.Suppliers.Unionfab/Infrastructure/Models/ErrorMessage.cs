using Newtonsoft.Json;

namespace Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models
{
    public class ErrorMessage
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
