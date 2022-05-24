using System;

namespace Jiepei.ERP.Orders.Orders
{
    public class DeliverOrderEto
    {
        public Guid OrderId { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string TrackingNo { get; set; }

        /// <summary>
        /// 快递公司
        /// </summary>
        public string CourierCompany { get; set; }


        public DeliverOrderEto(Guid orderId, string trackingNo, string courierCompany)
        {
            OrderId = orderId;
            TrackingNo = trackingNo;
            CourierCompany = courierCompany;
        }
    }
}
