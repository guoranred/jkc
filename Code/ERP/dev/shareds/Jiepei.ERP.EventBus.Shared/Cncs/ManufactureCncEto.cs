using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Cncs
{
    [EventName("Erp.Cnc.ManufactureChange")]
    public class ManufactureCncEto : OrderEventBaseDto
    {
        /// <summary>
        /// 
        /// </summary>
        public ManufactureCncEto() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        public ManufactureCncEto(string orderNo)
        {
            OrderNo = orderNo;
        }
    }
}
