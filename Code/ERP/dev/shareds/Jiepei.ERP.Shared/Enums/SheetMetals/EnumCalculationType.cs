namespace Jiepei.ERP.Shared.Enums.SheetMetals
{
    /// <summary>
    /// 计价方式
    /// </summary>
    public enum EnumCalculationType : byte
    {
        /// <summary>
        /// 自动计价
        /// </summary>
        [EnumDesc("自动计价")]
        Automatic = 0,

        /// <summary>
        /// 人工计价
        /// </summary>
        [EnumDesc("人工计价")]
        Artificial = 1
    }
}
