namespace Jiepei.ERP.Orders.Admin.Orders
{
    public class ApiOrderDetailTotalMoneyDto
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 产品订单金额
        /// </summary>
        public decimal TotalMoney { get; set; }
    }
}
