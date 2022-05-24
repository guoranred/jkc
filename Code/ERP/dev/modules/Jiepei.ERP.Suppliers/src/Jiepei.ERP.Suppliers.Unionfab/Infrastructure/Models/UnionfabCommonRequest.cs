using Newtonsoft.Json;

namespace Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models
{
    public class UnionfabCommonRequest : IUnionfabRequest
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
