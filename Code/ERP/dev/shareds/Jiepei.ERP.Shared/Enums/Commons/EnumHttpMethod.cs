namespace Jiepei.ERP.Commons
{
    public enum EnumHttpMethod : byte
    {
        /// <summary>
        /// Get请求
        /// </summary>
        [EnumDesc("GET")]
        Get = 0,

        /// <summary>
        /// Post请求
        /// </summary>
        [EnumDesc("POST")]
        Post = 1
    }
}
