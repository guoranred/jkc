using System;

namespace Jiepei.ERP.Orders.Admin.Orders
{
    public class ApiOrderDetailDeliveryDayDto
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 交期天数
        /// </summary>
        public int DeliveryDays { get; set; }

        /// <summary>
        /// 交期日期
        /// </summary>
        public DateTime DeliveryDate { get; set; }
    }
}
