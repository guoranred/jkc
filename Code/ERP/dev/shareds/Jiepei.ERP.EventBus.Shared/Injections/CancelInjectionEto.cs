using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Injections
{
    [EventName("Erp.Injection.CancelChange")]
    public class CancelInjectionEto : OrderEventBaseDto
    {
        public CancelInjectionEto()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo">订单号</param>
        public CancelInjectionEto(string orderNo)
        {
            OrderNo = orderNo;
        }
    }
}
