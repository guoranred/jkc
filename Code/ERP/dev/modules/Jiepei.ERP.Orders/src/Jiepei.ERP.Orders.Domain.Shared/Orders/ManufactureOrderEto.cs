using System;

namespace Jiepei.ERP.Orders.Orders
{
    [Serializable]
    public class ManufactureOrderEto
    {
        public Guid OrderId { get; set; }

        public ManufactureOrderEto(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
