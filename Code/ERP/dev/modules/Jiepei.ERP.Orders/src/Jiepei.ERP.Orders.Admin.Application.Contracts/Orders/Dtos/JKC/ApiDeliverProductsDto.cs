
namespace Jiepei.ERP.Orders.Admin.Orders
{
    public class ApiDeliverProductsDto
    {
        /// <summary>
        /// 单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 快递公司
        /// </summary>
        public string SendExpName { get; set; }

        /// <summary>
        /// 快递号
        /// </summary>
        public string SendExpNo { get; set; }

        /// <summary>
        /// 寄件时间（yyyy-mm-dd hh:mm:ss）
        /// </summary>
        public string SendTime { get; set; }
    }
}
