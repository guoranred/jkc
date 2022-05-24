using Jiepei.ERP.Pays;
using Jiepei.ERP.Pays.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Jiepei.ERP.Controllers
{
    [Route("api/pays")]
    [ApiController]
    public class PayController : ERPController
    {
        private readonly IPayAppService _payAppService;
        public PayController(IPayAppService payAppService)
        {
            _payAppService = payAppService;
        }

        /// <summary>
        /// 获取支付结果
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("result")]
        public async Task<string> GetPayResultAsync([FromQuery] GetPayResultDto input)
        {
            return await _payAppService.GetPayResultAsync(input);
        }

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("pay")]
        public async Task<CreatePayOutputDto> CreatePayAsync(CreatePayInputDto input)
        {
            return await _payAppService.CreatePayAsync(input);
        }
    }
}
