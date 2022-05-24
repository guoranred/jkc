namespace Jiepei.ERP.Orders
{
    /// <summary>
    /// 预计年使用量
    /// </summary>
    public enum EnumUsage
    {
        /// <summary>
        /// 一万以下
        /// </summary>
        [EnumDesc("一万以下")]
        UnOne = 1,

        /// <summary>
        /// 一万到五万
        /// </summary>
        [EnumDesc("一万到五万")]
        UnFive = 2,

        /// <summary>
        /// 五万到十万
        /// </summary>
        [EnumDesc("五万到十万")]
        UnTen = 4,

        /// <summary>
        /// 十万以上
        /// </summary>
        [EnumDesc("十万以上")]
        UpTen = 8
    }
}
