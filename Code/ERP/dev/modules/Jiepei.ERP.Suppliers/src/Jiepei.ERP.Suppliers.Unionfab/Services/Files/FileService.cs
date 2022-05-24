using Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Files
{
    public class FileService : CommonService
    {
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UnionfabCommonResponse<CreateFileResponse>> CreateAsync(CreateFileRequest request)
        {
            string targetUrl = $"/order/{request.Code}/files";

            var response = await SupplierUnionfabApiRequester.RequestAsync<UnionfabCommonResponse<CreateFileResponse>>(targetUrl, HttpMethod.Post, request);

            if (response.Status == ResponseStatus.Error)
                throw new UserFriendlyException("请求优联创建文件接口出错",
                                                response.ErrorMessage.Code,
                                                response.ErrorMessage.Reason);
            return response;
        }

        /// <summary>
        /// 获取文件信息 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UnionfabCommonResponse<GetFileResponse>> GetAsync(GetFileRequest request)
        {
            string targetUrl = $"/order/{request.Code}/files/{request.FileId}";

            var response = await SupplierUnionfabApiRequester.RequestAsync<UnionfabCommonResponse<GetFileResponse>>(targetUrl, HttpMethod.Get);
            if (response.Status == ResponseStatus.Error)
                throw new UserFriendlyException("请求优联创建文件接口出错",
                                                response.ErrorMessage.Code,
                                                response.ErrorMessage.Reason);
            return response;
        }
    }
}
