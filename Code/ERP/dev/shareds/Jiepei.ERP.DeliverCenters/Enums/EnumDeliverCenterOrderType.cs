using System.ComponentModel;

namespace Jiepei.ERP.DeliverCentersClient.Enums
{
    /// <summary>
    /// 订单类型
    /// </summary>
    public enum EnumDeliverCenterOrderType
    {
        /// <summary>
        /// 单产品线订单
        /// </summary>
        [Description("单产品线订单")]
        SingleLine = 1,

        /// <summary>
        /// 内部下单
        /// </summary>
        [Description("内部下单")]
        InternalOrder = 5,

        /// <summary>
        /// 组合生产
        /// </summary>
        [Description("组合生产")]
        ComProduction = 10,

        /// <summary>
        /// 组合拆分
        /// </summary>
        [Description("组合拆分")]
        ComResolution = 15,

        /// <summary>
        /// 内部调转
        /// </summary>
        [Description("内部调转")]
        InternalTransfer = 20
    }
}
