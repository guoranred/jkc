namespace Jiepei.ERP.Orders.Application.External.Order.Models
{
    /// <summary>
    /// 取消订单
    /// </summary>
    public class CancelOrderDto
    {
        public CancelOrderDto()
        {

        }

        public CancelOrderDto(string orderNo, string cancelType, string cancelReason = "")
        {
            this.OrderNo = orderNo;
            this.CancelType = cancelType;
            this.CancelReason = cancelReason;
        }

        /// <summary>
        /// 订单ID
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelType { get; set; }

        /// <summary>
        /// 取消说明
        /// </summary>
        public string CancelReason { get; set; }
    }
}
