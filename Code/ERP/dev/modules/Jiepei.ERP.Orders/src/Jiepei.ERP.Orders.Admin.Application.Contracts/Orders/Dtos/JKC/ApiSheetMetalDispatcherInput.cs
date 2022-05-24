
namespace Jiepei.ERP.Orders.Admin.Orders
{
    public class ApiSheetMetalDispatcherInput
    {
        /// <summary>
        /// 调用方法名
        /// </summary>
        public EnumApiSheetMetalMethod Method { get; set; }

        /// <summary>
        /// 相关参数
        /// </summary>
        public string Parameter { get; set; }

        /// <summary>
        /// 消息签名
        /// </summary>
        public string Sign { get; set; }
    }
}
