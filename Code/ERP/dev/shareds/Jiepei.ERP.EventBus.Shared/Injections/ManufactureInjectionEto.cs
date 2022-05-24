using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Injections
{
    [EventName("Erp.Injection.ManufactureChange")]
    public class ManufactureInjectionEto : OrderEventBaseDto
    {
        /// <summary>
        /// 
        /// </summary>
        public ManufactureInjectionEto() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        public ManufactureInjectionEto(string orderNo)
        {
            OrderNo = orderNo;
        }
    }
}
