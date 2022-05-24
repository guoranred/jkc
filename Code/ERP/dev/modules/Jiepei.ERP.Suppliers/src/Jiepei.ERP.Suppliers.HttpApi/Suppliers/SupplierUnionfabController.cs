using Jiepei.ERP.Suppliers.Suppliers.Dtos;
using Jiepei.ERP.Suppliers.Unionfab;
using Jiepei.ERP.Suppliers.Unionfab.Infrastructure.Models;
using Jiepei.ERP.Suppliers.Unionfab.Services.Files;
using Jiepei.ERP.Suppliers.Unionfab.Services.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Jiepei.ERP.Suppliers.Suppliers
{
    /// <summary>
    /// 优联
    /// </summary>
    [RemoteService]
    [Route("api/suppliers/unionfab")]
    public class SupplierUnionfabController : SuppliersController
    {
        private readonly ISupplierUnionfabAppService _supplierUnionfabAppService;
        private readonly SupplierUnionfabOptions _supplierUnionfabOptions;
        public SupplierUnionfabController(ISupplierUnionfabAppService supplierUnionfabAppService,
                                          IOptions<SupplierUnionfabOptions> supplierUnionfabOptionsAccessor)
        {
            _supplierUnionfabAppService = supplierUnionfabAppService;
            _supplierUnionfabOptions = supplierUnionfabOptionsAccessor.Value;
        }

        #region Files
        /// <summary>
        ///优联-创建文件
        /// </summary>
        /// <returns></returns>
        [HttpPost("{code}/files")]
        public async Task<UnionfabCommonResponse<CreateFileResponse>> CreateAsync(string code, [FromBody] CreateFileRequestInput input)
        {
            return await _supplierUnionfabAppService.CreateAsync(code, input);
        }

        /// <summary>
        /// 优联-获取文件信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [HttpGet("file/{code}/{fileId}")]
        public async Task<UnionfabCommonResponse<GetFileResponse>> GetAsync(string code, string fileId)
        {
            return await _supplierUnionfabAppService.GetAsync(code, fileId);
        }

        #endregion

        #region Orders

        /// <summary>
        /// 优联-生成订单号
        /// </summary>
        /// <returns></returns>
        [HttpPut("order/create-code")]
        public async Task<UnionfabCommonResponse<string>> CreateCodeAsync()
        {
            return await _supplierUnionfabAppService.CreateCodeAsync();
        }

        /// <summary>
        /// 优联-订单创建
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("order/create")]
        public async Task<UnionfabCommonResponseBase> CreateAsync(CreateOrderRequest request)
        {
            return await _supplierUnionfabAppService.CreateAsync(request);
        }

        /// <summary>
        /// 优联-订单关闭
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("order/close")]
        public async Task<UnionfabCommonResponseBase> CloseAsync(CloseOrderRequest request)
        {
            return await _supplierUnionfabAppService.CloseAsync(request);
        }

        /// <summary>
        /// 优联-订单查询
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("order/{code}")]
        public async Task<UnionfabCommonResponse<OpenOrder>> GetAsync(string code)
        {
            return await _supplierUnionfabAppService.GetAsync(code);
        }

        /// <summary>
        /// 优联-订单确认
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("order/confirm")]
        public async Task<UnionfabCommonResponseBase> ConfirmAsync(ConfirmOrderRequest request)
        {
            return await _supplierUnionfabAppService.ConfirmAsync(request);
        }
        #endregion

        [HttpPost("notify")]
        public IActionResult Notify([FromBody] UnionfabCallbackInput input)
        {
            var signature = CreateSignature(input.Code, input.Timestamp);
            if (signature != input.Signature)
                return BadRequest();
            return Ok(new { Status = "ok" });
        }

        private string CreateSignature(string code, long timestamp)
        {
            var md5 = MD5.Create();
            var strBytes = Encoding.UTF8.GetBytes($"Unionfab:{code}:{timestamp}:{_supplierUnionfabOptions.AccessKey}");
            var hashBytes = md5.ComputeHash(strBytes);
            var sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
