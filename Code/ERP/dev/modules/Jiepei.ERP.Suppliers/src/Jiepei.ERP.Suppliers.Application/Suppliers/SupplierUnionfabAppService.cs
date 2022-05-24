using Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models;
using Jiepei.ERP.Suppliers.Unionfab.Services.Code;
using Jiepei.ERP.Suppliers.Unionfab.Services.Files;
using Jiepei.ERP.Suppliers.Unionfab.Services.Order;
using System.IO;
using System.Threading.Tasks;

namespace Jiepei.ERP.Suppliers.Suppliers
{
    public class SupplierUnionfabAppService : SuppliersAppService, ISupplierUnionfabAppService
    {
        private readonly FileService _filesService;
        private readonly OrderService _orderService;
        private readonly CodeService _codeService;

        public SupplierUnionfabAppService(FileService filesService
            , OrderService orderService
            , CodeService codeService)
        {
            _filesService = filesService;
            _orderService = orderService;
            _codeService = codeService;
        }

        #region Files
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="code"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<UnionfabCommonResponse<CreateFileResponse>> CreateAsync(string code, CreateFileRequestInput input)
        {
            //var md5 = BitConverter.ToString(Convert.FromBase64String(input.Md5)).Replace("-", "");
            var request = new CreateFileRequest(code,
                                                input.Url,
                                                "MODEL",
                                                input.Name,
                                                input.Md5,
                                                Path.GetExtension(input.Name).Replace(".", "")
                                                );
            return await _filesService.CreateAsync(request);
        }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public async Task<UnionfabCommonResponse<GetFileResponse>> GetAsync(string code, string fileId)
        {
            return await _filesService.GetAsync(new GetFileRequest(fileId, code));
        }
        #endregion

        #region Orders
        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns></returns>
        public async Task<UnionfabCommonResponse<string>> CreateCodeAsync()
        {
            return await _codeService.CreateAsync();
        }

        /// <summary>
        /// 订单创建
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UnionfabCommonResponseBase> CreateAsync(CreateOrderRequest request)
        {
            return await _orderService.CreateAsync(request);
        }

        /// <summary>
        /// 订单关闭
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UnionfabCommonResponseBase> CloseAsync(CloseOrderRequest request)
        {
            return await _orderService.CloseAsync(request);
        }

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<UnionfabCommonResponse<OpenOrder>> GetAsync(string code)
        {
            return await _orderService.GetAsync(new GetOrderRequest(code));
        }

        /// <summary>
        /// 订单确认
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UnionfabCommonResponseBase> ConfirmAsync(ConfirmOrderRequest request)
        {
            return await _orderService.ConfirmAsync(request);
        }
        #endregion
    }
}
