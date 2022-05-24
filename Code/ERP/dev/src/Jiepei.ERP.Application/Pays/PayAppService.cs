using Alipay.EasySDK.Factory;
using Alipay.EasySDK.Kernel.Util;
using Essensoft.Paylink.WeChatPay;
using Essensoft.Paylink.WeChatPay.V3;
using Essensoft.Paylink.WeChatPay.V3.Domain;
using Essensoft.Paylink.WeChatPay.V3.Request;
using Jiepei.ERP.Members;
using Jiepei.ERP.Orders;
using Jiepei.ERP.Orders.Channels;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Orders.Orders.Dtos;
using Jiepei.ERP.Orders.Pays;
using Jiepei.ERP.Orders.Pays.Dtos;
using Jiepei.ERP.Pays.Dtos;
using Jiepei.ERP.Shared.Enums.Pays;
using Jiepei.ERP.Utilities;
using Jiepei.ERP.Utilities.Pays;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;

namespace Jiepei.ERP.Pays
{
    [Authorize]
    public class PayAppService : ERPAppService, IPayAppService
    {
        private readonly IMemberAppService _memberAppService;
        private readonly IOrderAppService _orderAppService;
        private readonly IChannelAppService _channelAppService;
        private readonly IOrderPayLogAppService _orderPayLogAppService;

        private readonly WeChatPayAHOption _weChatPayAHOption;
        private readonly IWeChatPayClient _weChatPayClient;
        private readonly IDistributedCache<string, string> _payCache;

        public PayAppService(IWeChatPayClient weChatPayClient
            , IOptions<WeChatPayAHOption> weChatPayAHOption
            , IMemberAppService memberAppService
            , IOrderAppService orderAppService
            , IChannelAppService channelAppService
            , IOrderPayLogAppService orderPayLogAppService
            , IDistributedCache<string, string> payCache
           )
        {
            _weChatPayClient = weChatPayClient;
            _weChatPayAHOption = weChatPayAHOption.Value;
            _memberAppService = memberAppService;
            _orderAppService = orderAppService;
            _channelAppService = channelAppService;
            _orderPayLogAppService = orderPayLogAppService;
            _payCache = payCache;
        }

        public async Task<string> GetPayResultAsync(GetPayResultDto input)
        {
            var payResult = await _payCache.GetAsync(input.PayNo);
            if (!string.IsNullOrWhiteSpace(payResult) && payResult.ToLower().Equals("true"))
            {
                await _payCache.RemoveAsync(input.PayNo);
                return payResult;
            }

            var payLog = await _orderPayLogAppService.GetByPayCodeAsync(input.PayNo);
            payResult = (payLog?.IsPaySuccess ?? false).ToString();
            await _payCache.SetAsync($"{input.PayNo}", payResult, new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(180)));
            return payResult;
        }

        public async Task<CreatePayOutputDto> CreatePayAsync(CreatePayInputDto input)
        {
            var result = await ValidAsync(input.OrderIds);
            var member = result.Item1;
            var orders = result.Item2;

            var payCode = OrderHelper.CreatePayCode(); //支付码
            var subject = await GetChannelNameAsync(orders.FirstOrDefault().ChannelId);
            var amount = GetTotalAmount(orders); //支付金额
            //预插入
            await PrePayAsync(payCode, (int)input.PayType, amount, subject, member, orders);

            var payResult = await PayAsync(payCode, subject, amount, input.PayType);
            return payResult;
        }

        #region private

        private async Task<CreatePayOutputDto> PayAsync(string payCode, string subject, decimal amount, EnumPayType payType)
        {
            var res = new CreatePayOutputDto { PayNo = payCode };
            switch (payType)
            {
                case EnumPayType.AliPay:
                    res.QRCodeBase64 = await GetAlipayCodeAsync(subject, payCode, amount.ToString());
                    return res;
                case EnumPayType.WeChatPay:
                    res.QRCodeBase64 = await GetWeChatCodeAsync(subject, payCode, (int)(amount * 100));
                    return res;
                default:
                    throw new UserFriendlyException("暂不支持当前支付方式");
            }
        }

        private async Task<(GetMemberDot, List<OrderDto>)> ValidAsync(List<Guid> orderIds)
        {
            var member = await ValidUserAsnyc();
            var orders = await ValidOrdersAsync(orderIds, member.Id);
            return (member, orders);
        }
        private async Task<GetMemberDot> ValidUserAsnyc()
        {
            var merber = await _memberAppService.GetByIdAsync(CurrentUser.Id.Value);

            if (merber == null)
                throw new UserFriendlyException("无效的用户");
            return merber;
        }

        private async Task<List<OrderDto>> ValidOrdersAsync(List<Guid> orderIds, Guid memberId)
        {
            var orders = await _orderAppService.GetListAsync(orderIds);
            if (!orders?.Any() ?? true)
                throw new UserFriendlyException("无效的订单");

            var unCurUsers = orders.Count(x => x.CreatorId != memberId);
            if (unCurUsers > 0)
                throw new UserFriendlyException("非本人有效订单");

            var unPays = orders.Count(x => !x.IsPay);
            if (orderIds.Count != unPays)
                throw new UserFriendlyException("部分订单已失效或已支付,请刷新再选择");

            var unCheckds = orders.Count(x => x.Status != EnumOrderStatus.CheckedPass);
            if (unCheckds > 0)
                throw new UserFriendlyException("只有审核通过状态的订单才能进行支付");

            var unOffers = orders.Where(x => x.SellingPrice == default);
            if (unOffers.Any())
                throw new UserFriendlyException($"订单:{unOffers.FirstOrDefault().OrderNo}还未报价，暂不能支付");

            return orders;
        }

        private async Task<string> GetChannelNameAsync(Guid channelId)
        {
            var channel = await _channelAppService.GetAsync(channelId);
            return channel?.ChannelName ?? "产品订单";
        }

        private async Task PrePayAsync(string payCode, int payType, decimal totalAmout, string subject, GetMemberDot memberDot, List<OrderDto> ordersDto)
        {
            var log = new CreateOrderPayLogDto
            {
                IsPaySuccess = false,
                MemberId = memberDot.Id,
                MemberName = memberDot.Name,
                PayCode = payCode,
                PayType = payType,
                TotalAmout = totalAmout,
                Remark = subject + ":" + ordersDto.Select(x => x.OrderNo).JoinAsString("|")
            };
            var details = new List<CreateOrderPayDetailLogDto>();
            foreach (var detail in ordersDto)
            {
                details.Add(new CreateOrderPayDetailLogDto
                {
                    OrderNo = detail.OrderNo,
                    SellingMoney = detail.SellingPrice,
                    DiscountAmount = 0,
                    IsSuccess = false,
                    FlowType = EnumOrderFlowType.Pay
                });
            }
            await _orderPayLogAppService.CreateAsync(log, details);
        }

        /// <summary>
        /// 获取订单支付金额
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private decimal GetTotalAmount(List<OrderDto> orders)
        {
            //销售价 + 优惠金额
            var amount = orders?.Sum(x => x.SellingPrice) ?? 0;
            return amount;
        }


        /// <summary>
        /// 支付宝
        /// </summary>
        /// <param name="subject">订单标题</param>
        /// <param name="outTradeNo">商户订单号</param>
        /// <param name="totalAmount">订单总金额</param>
        /// <returns></returns>
        private async Task<string> GetAlipayCodeAsync(string subject, string outTradeNo, string totalAmount)
        {
            try
            {
                var res = await Factory.Payment.FaceToFace().PreCreateAsync(subject, outTradeNo, totalAmount);
                if (!ResponseChecker.Success(res))
                    throw new UserFriendlyException("未返回有效数据,具体请看返回结果");

                return RenderQrCodeToBase64(res.QrCode);
            }
            catch
            {
                throw new UserFriendlyException("系统异常,请刷新");
            }
        }

        private async Task<string> GetWeChatCodeAsync(string subject, string outTradeNo, int totalAmount)
        {
            var reqModel = new WeChatPayTransactionsNativeBodyModel
            {
                AppId = _weChatPayAHOption.AppId,
                MchId = _weChatPayAHOption.MchId,
                Amount = new Amount { Total = totalAmount, Currency = "CNY" },
                Description = subject,
                OutTradeNo = outTradeNo,
                NotifyUrl = _weChatPayAHOption.NotifyUrl
            };
            try
            {
                var req = new WeChatPayTransactionsNativeRequest();
                req.SetBodyModel(reqModel);

                var reps = await _weChatPayClient.ExecuteAsync(req, ObjectMapper.Map<WeChatPayAHOption, WeChatPayOptions>(_weChatPayAHOption));
                if (!string.IsNullOrWhiteSpace(reps.Message) || !string.IsNullOrWhiteSpace(reps.Code) || reps.Detail != null)
                    throw new UserFriendlyException("未返回有效数据,具体请看返回结果");

                return RenderQrCodeToBase64(reps.CodeUrl);
            }
            catch
            {
                throw new UserFriendlyException("获取失败,请刷新");
            }
        }

        /// <summary>
        /// 渲染二维码，并转换成base64
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string RenderQrCodeToBase64(string str)
        {
            QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.L;
            using QRCodeGenerator qrGenerator = new();
            using QRCodeData qrCodeData = qrGenerator.CreateQrCode(str, eccLevel);
            using QRCode qrCode = new(qrCodeData);
            Bitmap bp = qrCode.GetGraphic(20, Color.Black, Color.White, true);
            //bp.Dispose();
            MemoryStream ms = new();
            bp.Save(ms, ImageFormat.Png);
            byte[] bytes = ms.GetBuffer();
            //ms.Close();
            var base64 = Convert.ToBase64String(bytes);
            return base64;
        }
        #endregion      
    }
}
