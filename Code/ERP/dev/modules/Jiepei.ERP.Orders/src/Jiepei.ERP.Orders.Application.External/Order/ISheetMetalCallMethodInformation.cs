namespace Jiepei.ERP.Orders.Application.External.Order
{
    internal interface ISheetMetalCallMethodInformation
    {

        /// <summary>
        /// 调用的地址
        /// </summary>
        public string CallUrl { get; }

        /// <summary>
        /// 发送的参数
        /// </summary>
        public string CallBody { get; }
    }
}
