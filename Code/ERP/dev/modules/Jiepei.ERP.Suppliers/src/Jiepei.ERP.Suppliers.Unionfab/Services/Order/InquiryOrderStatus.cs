using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Jiepei.ERP.Suppliers.Unionfab.Services.Order
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InquiryOrderStatus
    {
        /// <summary>
        /// 待提交
        /// </summary>
        WAIT_SUBMIT,
        /// <summary>
        /// 询价中
        /// </summary>
        INQUIRING_PRICE,
        /// <summary>
        /// 待生产
        /// </summary>
        WAIT_PRINT,
        /// <summary>
        /// 数据处理
        /// </summary>
        FILE_HANDLE,
        /// <summary>
        /// 生产中
        /// </summary>
        PRINTING,
        /// <summary>
        /// 后处理
        /// </summary>
        HANDLE,
        /// <summary>
        /// 待发货
        /// </summary>
        WAIT_DELIVER,
        /// <summary>
        /// 待收货
        /// </summary>
        WAIT_RECEIVE,
        /// <summary>
        /// 交易关闭
        /// </summary>
        ORDER_CLOSED,
        /// <summary>
        /// 交易完成
        /// </summary>
        ORDER_COMPLETE
    }
}
