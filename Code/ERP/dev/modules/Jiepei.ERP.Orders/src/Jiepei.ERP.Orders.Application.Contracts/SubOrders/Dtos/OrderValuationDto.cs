namespace Jiepei.ERP.Orders.SubOrders.Dtos
{
    public class OrderValuationDto
    {
        /// <summary>
        /// 总价
        /// </summary>
        public decimal SumPrice { get; set; }
        /// <summary>
        /// 交期
        /// </summary>
        public int MaxDay { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight { get; set; }
    }
}
