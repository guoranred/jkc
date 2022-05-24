using System.ComponentModel;

namespace Jiepei.ERP.Members.Enums
{
    public enum PayTypeEnum : byte
    {
        /// <summary>
        /// 微信支付
        /// </summary>
        [Description("微信支付")]
        WeChatPay = 0,
        /// <summary>
        /// 支付宝支付
        /// </summary>
        [Description("支付宝支付")]
        AliPay = 1,
        /// <summary>
        /// PayPal支付
        /// </summary>
        [Description("PayPal支付")]
        PayPal = 2
    }
}
