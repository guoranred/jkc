namespace Jiepei.ERP.Injections
{
    /// <summary>
    /// 包装方式
    /// </summary>
    public enum EnumInjectionOrderPackMethod
    {
        [EnumDesc("无")]
        NoThing = 0,

        [EnumDesc("吸塑盒")]
        BlisterBox = 1,

        [EnumDesc("箱装")]
        Boxed = 2,

        [EnumDesc("散装")]
        Bulk = 3,
    }
}
