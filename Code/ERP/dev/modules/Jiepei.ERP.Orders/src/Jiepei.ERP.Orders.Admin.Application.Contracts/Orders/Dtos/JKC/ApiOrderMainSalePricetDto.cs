namespace Jiepei.ERP.Orders.Admin.Orders
{
    public class ApiOrderMainSalePricetDto
    {
        /// <summary>
        /// 订单包
        /// </summary>
        public string GroupNo { set; get; }

        /// <summary>
        /// 运费
        /// </summary>
        public decimal ShipMoney { get; set; }

        /// <summary>
        /// 税费
        /// </summary>
        public decimal TaxMoney { get; set; }

        /// <summary>
        /// 浮动总金额
        /// </summary>
        public decimal PreferentialMoney { get; set; }

        /// <summary>
        /// 修改价格备注
        /// </summary>
        public string Note { get; set; }
    }
}
