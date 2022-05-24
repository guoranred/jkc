using System;

namespace Jiepei.ERP.Orders.Orders
{
    [Serializable]
    public class CompleteOrderEto
    {
        public Guid OrderId { get; set; }

        public CompleteOrderEto(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
