using Jiepei.ErpConsumer.Business.Contracts;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jiepei.ErpConsumer.Business.Services
{
    public class OrderDataService : IOrderDataService
    {
        private readonly IHttpClientFactory _clientFactory;
        public OrderDataService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<bool> OrderTaskAsync<T>(T model, string url, EnumHttpClientType clientType)
        {            
            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await ResponseMessageAsync(url, httpContent, clientType);
            return response.IsSuccessStatusCode;
        }

        private async Task<HttpResponseMessage> ResponseMessageAsync(string url, HttpContent httpContent, EnumHttpClientType clientType)
        {
            var client = _clientFactory.CreateClient(ErpConsumerConsts.ErpClient);

            switch (clientType)
            {
                case EnumHttpClientType.Put:
                    return await client.PutAsync(url, httpContent);
                case EnumHttpClientType.Post:
                    return await client.PostAsync(url, httpContent);
                case EnumHttpClientType.Get:
                    return await client.GetAsync(url);
                case EnumHttpClientType.Delete:
                    return await client.DeleteAsync(url);
                default:
                    return await Task.FromResult(default(HttpResponseMessage));
            }
        }
    }
}
