using System.ComponentModel;

namespace Jiepei.ERP.DeliverCentersClient.Enums
{
    public enum EnumDeliverCenterPayType
    {
        /// <summary>
        /// 现金付款
        /// </summary>
        [Description("现金付款")]
        Cash = 1,

        /// <summary>
        /// 分期付款
        /// </summary>
        [Description("分期付款")]
        Hire = 5,

        /// <summary>
        /// 预投产
        /// </summary>
        [Description("预投产")]
        PreProduce = 10,

        /// <summary>
        /// 迅捷(投呗)
        /// </summary>
        [Description("迅捷")]
        XunJie = 15,

        /// <summary>
        /// 悦捷(白条)
        /// </summary>
        [Description("悦捷")]
        YueJie = 20
    }
}
