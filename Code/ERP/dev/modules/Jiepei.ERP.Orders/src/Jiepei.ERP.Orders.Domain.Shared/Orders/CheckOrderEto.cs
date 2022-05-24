using System;

namespace Jiepei.ERP.Orders.Orders
{
    [Serializable]
    public class CheckOrderEto
    {
        public Guid OrderId { get; set; }
        public bool IsPassed { get; set; }

        public CheckOrderEto(Guid orderId, bool isPassed)
        {
            OrderId = orderId;
            IsPassed = isPassed;
        }
    }
}
