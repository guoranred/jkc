using Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Code
{
    public class CodeService : CommonService
    {
        public async Task<UnionfabCommonResponse<string>> CreateAsync()
        {
            const string targetUrl = "/order/code";

            var response = await SupplierUnionfabApiRequester.RequestAsync<UnionfabCommonResponse<string>>(targetUrl, HttpMethod.Put);
            if (response.Status == ResponseStatus.Error)
                throw new UserFriendlyException("请求优联生成订单号接口出错",
                                                response.ErrorMessage.Code,
                                                response.ErrorMessage.Reason);
            return response;
        }
    }
}
