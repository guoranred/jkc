using Jiepei.ERP.Commons;
using Jiepei.ERP.Orders.Application.External.Configuration;
using Jiepei.ERP.Utilities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;

namespace Jiepei.ERP.Orders.Application.External.Cache
{
    /// <summary>
    /// 钣金站公用请求方法
    /// </summary>
    public class SheetMetalServiceHelper
    {
        /// <summary>
        /// 缓存的Key
        /// </summary>
        private const string CacheTokenKey = "SheetMetal_Token";

        /// <summary>
        /// 内存缓存
        /// </summary>
        private readonly IDistributedCache<string> _cache;

        /// <summary>
        /// 远程服务的基本配置
        /// </summary>
        private static SheetMetalApiConfiguration _apiConfiguration;

        public SheetMetalServiceHelper(IDistributedCache<string> cache)
        {
            _cache = cache;
            _apiConfiguration = ServiceProviderInstance.Instance.GetRequiredService<SheetMetalApiConfiguration>();
        }

        /// <summary>
        /// 获取授权Token
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetAuthorizeToken()
        {
            return await _cache.GetOrAddAsync(
                CacheTokenKey,
                async () => await GetSheetMetalToken(),
                () => new DistributedCacheEntryOptions
                {
                    //远程返回的Token有效期为6小时，这里设置有效期为2小时
                    AbsoluteExpiration = DateTimeOffset.Now.AddHours(2)
                }
            );
        }

        /// <summary>
        /// 根据账号密码获取远程服务的授权Token，Token有效期6小时
        /// </summary>
        /// <returns></returns>
        private static async Task<string> GetSheetMetalToken()
        {
            return await Task.Run(() =>
            {
                var result = HttpHelper.Post<ApiHttpResponse>(_apiConfiguration.RemoteAddress + "/api/Sys_User/Login4Token",
                    JsonConvert.SerializeObject(new SheetMetalAccount(_apiConfiguration.Account, _apiConfiguration.Password)));
                if (!result.Status)
                    throw new UserFriendlyException($"获取远程服务授权Token失败，{result.Message}");
                return result.Data.ToString();
            });
        }

        /// <summary>
        /// 钣金站公用Post方法
        /// </summary>
        /// <param name="url">调用地址,如果是以http开头的字符串，则以输入地址为准，否则为默认的远程地址加上该字符串</param>
        /// <param name="postData">传递的参数</param>
        /// <returns></returns>
        public async Task<ApiHttpResponse> SheetMetalPostAsync(string url, string postData)
        {
            if (!url.StartsWith("http"))
                url = _apiConfiguration.RemoteAddress + url;
            try
            {
                NameValueCollection header = new()
                {
                    { "Authorization", await this.GetAuthorizeToken() }
                };
                var result = HttpHelper.Post<ApiHttpResponse>(url, postData, header);
                if (result.Code == "202")
                {
                    await ErrorHandleAsync(new Exception("401"), url, postData);
                }
                return result;
            }
            catch (Exception ex)
            {
                return await ErrorHandleAsync(ex, url, postData);
            }
        }

        /// <summary>
        /// 钣金站公用Get方法
        /// </summary>
        /// <param name="url">调用地址,如果是以http开头的字符串，则以输入地址为准，否则为默认的远程地址加上该字符串</param>
        /// <returns></returns>
        public async Task<ApiHttpResponse> SheetMetalGetAsync(string url)
        {
            if (!url.StartsWith("http"))
                url = _apiConfiguration.RemoteAddress + url;
            try
            {
                NameValueCollection header = new()
                {
                    { "Authorization", await this.GetAuthorizeToken() },
                    { "Content-Type", "application/json; charset=utf-8" }
                };
                var result = HttpHelper.Get<ApiHttpResponse>(url, header);
                if (result.Code == "202")
                {
                    await ErrorHandleAsync(new Exception("401"), url, method: EnumHttpMethod.Get);
                }
                return result;
            }
            catch (Exception ex)
            {
                return new ApiHttpResponse($"远程调用失败，原因：{ex.Message}");
            }
        }

        /// <summary>
        /// 根据不同的错误信息采用不同的解决方式
        /// </summary>
        /// <param name="error">捕获的异常</param>
        /// <param name="url">目标地址</param>
        /// <param name="data">要传递的Json字符串</param>
        /// <param name="method">请求方式</param>
        /// <returns></returns>
        private async Task<ApiHttpResponse> ErrorHandleAsync(Exception error, string url, string data = "", EnumHttpMethod method = EnumHttpMethod.Post)
        {
            if (error.Message.Contains("401"))
            {
                try
                {
                    //如果Token有效期内仍然返回无效401
                    //移除Token
                    await _cache.RemoveAsync(CacheTokenKey);
                    //再次获取Token
                    NameValueCollection header = new()
                    {
                        { "Authorization", await this.GetAuthorizeToken() }
                    };
                    if (method == EnumHttpMethod.Get)
                    {
                        header.Add("Content-Type", "application/json; charset=utf-8");
                        return HttpHelper.Get<ApiHttpResponse>(url, header);
                    }
                    return HttpHelper.Post<ApiHttpResponse>(url, data, header);
                }
                catch (Exception e)
                {
                    return new ApiHttpResponse($"远程调用失败，原因：{e.Message}");
                }
            }
            return new ApiHttpResponse($"远程调用失败，原因：{error.Message}");
        }
    }
}
