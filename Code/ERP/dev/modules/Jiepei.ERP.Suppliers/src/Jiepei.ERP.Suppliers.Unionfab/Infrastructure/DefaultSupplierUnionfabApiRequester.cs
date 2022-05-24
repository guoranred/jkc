using Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Jiepei.ERP.Suppliers.Unionfab.Infrastructure
{
    [Dependency(TryRegister = true)]
    public class DefaultSupplierUnionfabApiRequester : ISupplierUnionfabApiRequester, ITransientDependency
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SupplierUnionfabOptions _options;

        public DefaultSupplierUnionfabApiRequester(IHttpClientFactory httpClientFactory, IOptions<SupplierUnionfabOptions> optionsAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _options = optionsAccessor.Value;
        }

        public async Task<TResponse> RequestAsync<TResponse>(string targetUrl, HttpMethod method, IUnionfabRequest unionfabRequest = null)
        {
            var responseMessage = await RequestGetHttpResponseMessageAsync(targetUrl, method, unionfabRequest);

            var resultStr = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(resultStr);
        }

        private async Task<HttpResponseMessage> RequestGetHttpResponseMessageAsync(string targetUrl, HttpMethod method, IUnionfabRequest unionfabRequest = null)
        {
            var token = $"UfcOrderToken {_options.AccessKey}:{_options.Token}";
            var client = _httpClientFactory.CreateClient();

            targetUrl = _options.BasePath + targetUrl;//.EnsureEndsWith('?');

            HttpRequestMessage requestMsg;

            if (method == HttpMethod.Get)
                requestMsg = BuildHttpGetRequestMessage(targetUrl, unionfabRequest);
            else if (method == HttpMethod.Post)
                requestMsg = BuildHttpPostRequestMessage(targetUrl, unionfabRequest);
            else
                requestMsg = BuildHttpPutRequestMessage(targetUrl, unionfabRequest);

            requestMsg.Headers.Add("Authorization", token);

            return await client.SendAsync(requestMsg);
        }

        private HttpRequestMessage BuildHttpGetRequestMessage(string targetUrl, IUnionfabRequest unionfabRequest)
        {
            //if (unionfabRequest == null) 
            return new HttpRequestMessage(HttpMethod.Get, targetUrl);

            //var requestUrl = BuildQueryString(targetUrl, unionfabRequest);
            //return new HttpRequestMessage(HttpMethod.Get, requestUrl);
        }

        private HttpRequestMessage BuildHttpPostRequestMessage(string targetUrl, IUnionfabRequest unionfabRequest)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, targetUrl);

            if (unionfabRequest != null)
                requestMessage.Content = new StringContent(unionfabRequest.ToString(), Encoding.UTF8, "application/json");
            return requestMessage;
        }

        private HttpRequestMessage BuildHttpPutRequestMessage(string targetUrl, IUnionfabRequest unionfabRequest)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, targetUrl);

            if (unionfabRequest != null)
                requestMessage.Content = new StringContent(unionfabRequest.ToString(), Encoding.UTF8, "application/json");
            return requestMessage;
        }

        private string BuildQueryString(string targetUrl, IUnionfabRequest request)
        {
            if (request == null) return targetUrl;

            var type = request.GetType();
            var properties = type.GetProperties();

            if (properties.Length > 0)
            {
                targetUrl = targetUrl.EnsureEndsWith('&');
            }

            var queryStringBuilder = new StringBuilder(targetUrl);

            foreach (var propertyInfo in properties)
            {
                var jsonProperty = propertyInfo.GetCustomAttribute<JsonPropertyAttribute>();
                var propertyName = jsonProperty != null ? jsonProperty.PropertyName : propertyInfo.Name;

                queryStringBuilder.Append($"{propertyName}={propertyInfo.GetValue(request)}&");
            }

            return queryStringBuilder.ToString().TrimEnd('&');
        }
    }
}
