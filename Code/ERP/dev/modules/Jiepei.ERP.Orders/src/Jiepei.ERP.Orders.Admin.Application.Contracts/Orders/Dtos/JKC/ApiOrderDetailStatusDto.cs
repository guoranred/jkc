namespace Jiepei.ERP.Orders.Admin.Orders
{
    public class ApiOrderDetailStatusDto
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int Status { get; set; }
    }
}
