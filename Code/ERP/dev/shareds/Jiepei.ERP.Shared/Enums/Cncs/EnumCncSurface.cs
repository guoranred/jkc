namespace Jiepei.ERP.Cncs
{
    public enum EnumCncSurface
    {
        /// <summary>
        /// 默认
        /// </summary>   
        [EnumDesc("")]
        Default = 0,

        /// <summary>
        /// 阳极氧化
        /// </summary>      
        [EnumDesc("阳极氧化")]
        Anodized = 1,

        /// <summary>
        ///着色氧化
        /// </summary>   
        [EnumDesc("着色氧化")]
        Colouring = 2,

        /// <summary>
        /// 拉丝
        /// </summary>  
        [EnumDesc("拉丝")]
        Wire = 3,

        /// <summary>
        /// 其他
        /// </summary>  
        [EnumDesc("其他")]
        Other = 4,

        /// <summary>
        /// 喷砂氧化
        /// </summary>  
        [EnumDesc("喷砂氧化")]
        Sandblasting = 5,

        /// <summary>
        /// 喷塑
        /// </summary>
        [EnumDesc("喷塑")]
        PlasticSpraying = 6,

        /// <summary>
        /// 喷漆
        /// </summary>
        [EnumDesc("喷漆")]
        Painting = 7,

        /// <summary>
        /// 抛光
        /// </summary>
        [EnumDesc("抛光")]
        Polishing = 8,

        /// <summary>
        /// 镭雕
        /// </summary>
        [EnumDesc("镭雕")]
        RadiumCarving = 9,

        /// <summary>
        /// 丝印
        /// </summary>
        [EnumDesc("丝印")]
        SilkScreen = 10,
    }
}
