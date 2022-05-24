namespace Jiepei.ERP.Cncs
{
    public enum EnumCncMaterial
    {
        /// <summary>
        /// 默认
        /// </summary>  
        [EnumDesc("")]
        Default = 0,

        /// <summary>
        /// 铝合金
        /// </summary>    
        [EnumDesc("铝合金")]
        Alufer = 1,

        /// <summary>
        /// 不锈钢
        /// </summary>  
        [EnumDesc("不锈钢")]
        Stainless = 2,

        /// <summary>
        /// 钢
        /// </summary>  
        [EnumDesc("钢")]
        Steel = 3,

        /// <summary>
        /// 其他
        /// </summary>    
        [EnumDesc("其他")]
        Other = 4
    }
}
