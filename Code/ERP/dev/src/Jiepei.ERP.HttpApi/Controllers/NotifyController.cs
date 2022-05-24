using Jiepei.ERP.Pays;
using Jiepei.ERP.Pays.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Jiepei.ERP.Controllers
{
    /// <summary>
    /// 系统回调
    /// </summary>  
    [Route("api/notifys")]
    [ApiController]
    [AllowAnonymous]
    public class NotifyController : ERPController
    {
        private readonly IPayNotifyService _payNotifyService;
        public NotifyController(IPayNotifyService payNotifyService)
        {
            _payNotifyService = payNotifyService;
        }

        #region 支付回调
        /// <summary>
        /// 支付宝回调
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("ah-alipay")]
        public async Task<string> AliNotifyAsync([FromForm] AliPayNotify input)
        {
            return await _payNotifyService.AHAliPayNotify(input);
        }

        /// <summary>
        /// 微信回调
        /// </summary>
        /// <returns></returns>
        [HttpPost("ah-wechat")]
        public async Task WeChatNotifyAsync()
        {
            await _payNotifyService.AHWeChatPayNotify();
        }
        #endregion     
    }
}
