namespace Jiepei.ERP.Shared.Enums.Pays
{
    /// <summary>
    /// 支付方式
    /// </summary>
    public enum EnumPayType
    {
        /// <summary>
        /// 微信支付
        /// </summary>
        [EnumDesc("微信")]
        WeChatPay = 1,

        /// <summary>
        /// 支付宝支付
        /// </summary>
        [EnumDesc("支付宝")]
        AliPay = 2,

        /// <summary>
        /// PayPal支付
        /// </summary>
        [EnumDesc("PayPal支付")]
        PayPal = 3
    }
}
