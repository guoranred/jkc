using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Injections
{
    [EventName("Erp.Injection.CompleteChange")]
    public class CompleteInjectionEto : OrderEventBaseDto
    {
        public CompleteInjectionEto()
        {

        }

        /// <param name="orderNo"></param>
        public CompleteInjectionEto(string orderNo)
        {
            OrderNo = orderNo;
        }
    }
}
