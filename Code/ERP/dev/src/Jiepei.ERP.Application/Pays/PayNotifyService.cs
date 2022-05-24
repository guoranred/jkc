using Alipay.EasySDK.Factory;
using Essensoft.Paylink.WeChatPay;
using Essensoft.Paylink.WeChatPay.V3;
using Essensoft.Paylink.WeChatPay.V3.Notify;
using Jiepei.ERP.EventBus.Shared.Pays;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Orders.Pays;
using Jiepei.ERP.Pays.Dtos;
using Jiepei.ERP.Utilities.Pays;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Local;

namespace Jiepei.ERP.Pays
{
    public class PayNotifyService : ERPAppService, IPayNotifyService
    {
        private readonly ILogger<PayNotifyService> _logger;
        //private readonly IDistributedCache<string, string> _payCache;
        private readonly IOrderPayLogAppService _orderPayLogAppService;

        private readonly ILocalEventBus _localEventBus;

        private readonly IHttpContextAccessor _httpContext;
        private readonly WeChatPayAHOption _weChatPayAHOption;
        private readonly AliPayAHOption _aliPayAHOption;
        private readonly IWeChatPayNotifyClient _weChatPayNotifyClient;
        private readonly IOrderAppService _orderAppService;

        public PayNotifyService(IHttpContextAccessor httpContext
            , ILogger<PayNotifyService> logger
            , ILocalEventBus localEventBus
            , IOptions<AliPayAHOption> aliPayAHOption
            , IOptions<WeChatPayAHOption> weChatPayAHOption
            , IOrderPayLogAppService orderPayLogAppService
            , IWeChatPayNotifyClient weChatPayNotifyClient
            , IOrderAppService orderAppService)
        {
            _logger = logger;
            //_payCache = payCache;
            _httpContext = httpContext;
            _localEventBus = localEventBus;
            _aliPayAHOption = aliPayAHOption.Value;
            _weChatPayAHOption = weChatPayAHOption.Value;
            _orderPayLogAppService = orderPayLogAppService;
            _weChatPayNotifyClient = weChatPayNotifyClient;
            _orderAppService = orderAppService;
        }

        public async Task AHWeChatPayNotify()
        {
            var payCode = string.Empty;
            try
            {
                var notify = await _weChatPayNotifyClient.ExecuteAsync<WeChatPayTransactionsNotify>(_httpContext.HttpContext.Request, ObjectMapper.Map<WeChatPayAHOption, WeChatPayOptions>(_weChatPayAHOption));

                payCode = notify?.OutTradeNo;
                var notifyStr = JsonConvert.SerializeObject(notify);
                var isSuccess = notify?.TradeState == WeChatPayTradeState.Success;
                _logger.LogInformation($"支付单号{payCode}的微信支付回调信息：{notifyStr}");

                await _orderPayLogAppService.UpdatePayLogAsync(payCode, isSuccess);

                await PayEventBusAsync(payCode);



                //await _payCache.SetAsync($"{payCode}", "true", new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(180)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"支付单号：{payCode}的微信回调异常：{ex.Message}");
            }
        }

        public async Task<string> AHAliPayNotify(AliPayNotify notify)
        {
            var payCode = notify?.OutTradeNo;
            try
            {
                var isSafe = AlipaySignature(notify);
                if (!isSafe)
                {
                    _logger.LogError($"支付单号：{payCode}的支付宝回调验签失败");
                    return "fail";
                }

                var notifyStr = JsonConvert.SerializeObject(notify);
                _logger.LogInformation($"支付单号{payCode}的支付宝支付回调信息：{notifyStr}");

                await _orderPayLogAppService.UpdatePayLogAsync(payCode, true);

                await PayEventBusAsync(payCode);

                //await _payCache.SetAsync($"{notify.OutTradeNo}", "true", new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(180)));

                //await _httpContext.HttpContext.Response.WriteAsync("success");

                return "success";
            }
            catch (Exception ex)
            {
                _logger.LogError($"支付单号：{payCode}的支付宝回调异常：{ex.Message}");
                return "fail";
            }
        }

        #region private
        private bool AlipaySignature(AliPayNotify notify)
        {
            if (notify.AppId != _aliPayAHOption.AppId)
                return false;

            var request = _httpContext.HttpContext.Request;
            var dict = new Dictionary<string, string>();
            var keys = request.Form.Keys;
            if (keys == null)
                return false;

            foreach (var key in keys)
            {
                dict.Add(key, request.Form[key]);
            }

            return Factory.Payment.Common().VerifyNotify(dict) ?? false;
        }


        private async Task PayEventBusAsync(string payCode)
        {
            var log = await _orderPayLogAppService.GetByPayCodeAsync(payCode);
            var details = await _orderPayLogAppService.GetDetailListAsync(log?.Id ?? Guid.NewGuid());

            foreach (var order in details ?? new List<Orders.Pays.Dtos.GetOrderPayDetailLogDto>())
            {
                _logger.LogError($"支付回调的订单{order.OrderNo}正在执行事件总线Amount:" +order.SellingMoney);
                await _localEventBus.PublishAsync(new OrderPayChangedEto
                {
                    OrderNo = order.OrderNo,
                    Amount = order.SellingMoney
                });

                _logger.LogError($"支付回调的订单{order.OrderNo}正在同步交付中台");
                await _orderAppService.PayNotifyAsync(order.OrderNo);
            }
        }
        #endregion
    }
}
