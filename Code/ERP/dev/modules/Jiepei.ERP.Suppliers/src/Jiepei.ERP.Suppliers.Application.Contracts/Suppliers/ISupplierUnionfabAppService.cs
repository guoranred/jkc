using Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models;
using Jiepei.ERP.Suppliers.Unionfab.Services.Files;
using Jiepei.ERP.Suppliers.Unionfab.Services.Order;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Suppliers.Suppliers
{
    /// <summary>
    /// 优联服务接口
    /// </summary>
    public interface ISupplierUnionfabAppService : IApplicationService
    {
        #region Files
        Task<UnionfabCommonResponse<CreateFileResponse>> CreateAsync(string code, CreateFileRequestInput input);
        Task<UnionfabCommonResponse<GetFileResponse>> GetAsync(string code, string fileId);
        #endregion

        #region Orders
        Task<UnionfabCommonResponse<string>> CreateCodeAsync();
        Task<UnionfabCommonResponseBase> CreateAsync(CreateOrderRequest request);
        Task<UnionfabCommonResponseBase> CloseAsync(CloseOrderRequest request);
        Task<UnionfabCommonResponse<OpenOrder>> GetAsync(string code);
        Task<UnionfabCommonResponseBase> ConfirmAsync(ConfirmOrderRequest request);
        #endregion
    }
}
