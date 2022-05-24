namespace Jiepei.ERP.Shared.Consumers.Orders
{
    /// <summary>
    /// 通用订单费用信息
    /// </summary>
    public class MQ_OrderTask_OrderCostDto
    {
        /// <summary>
        /// 产品费
        /// </summary>
        public decimal ProMoney { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        public decimal ShipMoney { get; set; }

        /// <summary>
        /// 税费
        /// </summary>
        public decimal TaxMoney { get; set; }

        /// <summary>
        /// 税点
        /// </summary>
        public decimal TaxPoint { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal DiscountMoney { get; set; }
    }
}
