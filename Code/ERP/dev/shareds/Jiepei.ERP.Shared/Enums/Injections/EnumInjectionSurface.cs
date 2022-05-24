namespace Jiepei.ERP.Injections
{
    public enum EnumInjectionSurface
    {
        [EnumDesc("原色")]
        PrimaryColor = 1,

        [EnumDesc("丝印")]
        SilkScreenPrinting = 2,

        [EnumDesc("电镀")]
        Electroplating = 3,

        [EnumDesc("烫金")]
        Bronzing = 4,

        [EnumDesc("其他")]
        Other = 100,
    }
}
