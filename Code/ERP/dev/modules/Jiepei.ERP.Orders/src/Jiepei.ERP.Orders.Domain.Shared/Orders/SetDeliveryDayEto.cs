using System;

namespace Jiepei.ERP.Orders.Orders
{
    public class SetDeliveryDayEto
    {
        public Guid OrderId { get; set; }

        public int DeliveryDay { get; set; }

        public SetDeliveryDayEto(Guid orderId, int deliveryDay)
        {
            OrderId = orderId;
            DeliveryDay = deliveryDay;
        }
    }
}
