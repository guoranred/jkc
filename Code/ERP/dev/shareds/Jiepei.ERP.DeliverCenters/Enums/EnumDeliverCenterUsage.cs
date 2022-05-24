using System.ComponentModel;

namespace Jiepei.ERP.DeliverCentersClient.Enums
{
    /// <summary>
    /// 预计年使用量
    /// </summary>
    public enum EnumDeliverCenterUsage
    {       
        /// <summary>
            /// 无
            /// </summary>
        [Description("无")]
        Other = 0,
        /// <summary>
        /// 一万以下
        /// </summary>
        [Description("一万以下")]
        UnOne = 1,

        /// <summary>
        /// 一万到五万
        /// </summary>
        [Description("一万到五万")]
        UnFive = 2,

        /// <summary>
        /// 五万到十万
        /// </summary>
        [Description("五万到十万")]
        UnTen = 4,

        /// <summary>
        /// 十万以上
        /// </summary>
        [Description("十万以上")]
        UpTen = 8
    }
}
