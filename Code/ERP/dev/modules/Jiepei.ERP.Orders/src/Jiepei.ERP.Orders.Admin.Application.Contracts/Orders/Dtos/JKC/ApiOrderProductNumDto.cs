
namespace Jiepei.ERP.Orders.Admin.Orders
{
    public class ApiOrderProductNumDto
    {
        /// <summary>
        /// 单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 产品套数
        /// </summary>
        public int ProductNum { get; set; }

        /// <summary>
        /// 订单重量
        /// </summary>
        public decimal Weight { get; set; }
    }
}
