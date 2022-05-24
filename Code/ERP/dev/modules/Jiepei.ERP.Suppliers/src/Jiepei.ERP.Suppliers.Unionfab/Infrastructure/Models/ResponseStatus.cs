using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ResponseStatus
    {
        [EnumMember(Value = "ok")]
        OK,
        [EnumMember(Value = "error")]
        Error
    }
}
