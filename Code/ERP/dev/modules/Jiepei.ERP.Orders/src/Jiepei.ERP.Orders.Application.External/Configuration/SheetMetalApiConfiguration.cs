namespace Jiepei.ERP.Orders.Application.External.Configuration
{
    /// <summary>
    /// 远程调用服务方的配置(单例)
    /// </summary>
    public class SheetMetalApiConfiguration
    {
        /// <summary>
        /// 远程服务分配的账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 远程服务分配账户的密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 远程调用地址
        /// </summary>
        public string RemoteAddress { get; set; }

        /// <summary>
        /// 消息解密Key
        /// </summary>
        public string SecurityKey { get; set; }

        /// <summary>
        /// 渠道来源名称
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// 渠道来源代码
        /// </summary>
        public string ChannelCode { get; set; }

        /// <summary>
        /// 核价地址
        /// </summary>
        public string PriceRemoteAddress { get; set; }
    }
}
