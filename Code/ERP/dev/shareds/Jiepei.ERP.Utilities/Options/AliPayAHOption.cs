namespace Jiepei.ERP.Utilities.Options
{
    /// <summary>
    /// 安徽金密金属支付
    /// </summary>
    public class AliPayAHOption
    {
        // 应用ID,您的APPID
        public string AppId { get; set; }

        /// <summary>
        /// 合作商户uid
        /// </summary>
        public string Uid { get; set; }

        // 支付宝网关
        public string Gatewayurl { get; set; }

        // 商户私钥，您的原始格式RSA私钥
        public string PrivateKey { get; set; }

        // 支付宝公钥,查看地址：https://openhome.alipay.com/platform/keyManage.htm 对应APPID下的支付宝公钥。
        public string AlipayPublicKey { get; set; }

        // 签名方式
        public string SignType { get; set; }

        // 编码格式
        public string CharSet { get; set; }
    }
}
