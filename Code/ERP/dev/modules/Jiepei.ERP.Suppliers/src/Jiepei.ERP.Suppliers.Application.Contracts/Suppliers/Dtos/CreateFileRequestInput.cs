namespace Jiepei.ERP.Suppliers
{
    public class CreateFileRequestInput
    {
        /// <summary>
        /// 文件URL，公网可访问，在订单周期内可访问
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件 md5，用于文件处理校验
        /// </summary>
        public string Md5 { get; set; }
    }
}
