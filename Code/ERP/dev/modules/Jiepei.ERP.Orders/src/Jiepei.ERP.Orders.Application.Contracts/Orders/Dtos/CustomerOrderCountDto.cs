namespace Jiepei.ERP.Orders.Orders.Dtos
{
    public class CustomerOrderCountDto
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 待处理
        /// </summary>
        public int PendingSum { get; set; }
        /// <summary>
        /// 已处理
        /// </summary>
        public int ProcessedSum { get; set; }
    }
}
