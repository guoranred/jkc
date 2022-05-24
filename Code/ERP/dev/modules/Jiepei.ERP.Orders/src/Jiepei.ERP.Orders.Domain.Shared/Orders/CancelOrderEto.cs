using System;

namespace Jiepei.ERP.Orders.Orders
{
    public class CancelOrderEto
    {
        public Guid OrderId { get; set; }

        public CancelOrderEto(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
