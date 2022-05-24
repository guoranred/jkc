using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Injections
{
    [EventName("Erp.Injection.UpdateDeliveryDays")]
    public class DeliveryDaysInjectionEto : OrderEventBaseDto
    {

        public int DeliveryDays { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DeliveryDaysInjectionEto() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="deliveryDays"></param>
        public DeliveryDaysInjectionEto(string orderNo, int deliveryDays)
        {
            OrderNo = orderNo;
            DeliveryDays = deliveryDays;
        }
    }
}
