using Jiepei.ERP.Orders;
using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.SubOrders
{
    [EventName("Erp.SubOrder.UpdateDeliveryDays")]
    public class SubOrderDeliveryDaysEto : OrderEventBaseDto
    {

        public int DeliveryDays { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SubOrderDeliveryDaysEto() { }

        /// <summary>
        /// 订单号
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="deliveryDays"></param>
        /// <param name="orderType"></param>
        public SubOrderDeliveryDaysEto(string orderNo, int deliveryDays, EnumOrderType orderType)
        {
            OrderNo = orderNo;
            DeliveryDays = deliveryDays;
            OrderType = orderType;
        }
    }
}
