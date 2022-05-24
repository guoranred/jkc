using Newtonsoft.Json;

namespace Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models
{
    public class UnionfabCommonResponseBase : IUnionfabResponse
    {
        [JsonProperty("status")]
        public ResponseStatus Status { get; set; }

        [JsonProperty("err")]
        public ErrorMessage ErrorMessage { get; set; }
    }
}
