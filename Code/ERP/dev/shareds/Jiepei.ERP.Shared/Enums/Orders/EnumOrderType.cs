namespace Jiepei.ERP.Orders
{
    /// <summary>
    /// 订单类型
    /// </summary>
    public enum EnumOrderType
    {
        /// <summary>
        /// 模具
        /// </summary>
        [EnumDesc("模具")]
        Mold = 1,

        /// <summary>
        /// 注塑
        /// </summary>
        [EnumDesc("注塑")]
        Injection = 2,

        /// <summary>
        /// CNC
        /// </summary>
        [EnumDesc("CNC")]
        Cnc = 4,

        /// <summary>
        /// 3D打印
        /// </summary>
        [EnumDesc("3D打印")]
        Print3D = 8,

        /// <summary>
        /// 钣金
        /// </summary>
        [EnumDesc("钣金")]
        SheetMetal = 16
    }
}
