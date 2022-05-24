using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Cncs
{
    [EventName("Erp.Cnc.CompleteChange")]
    public class CompleteCncEto : OrderEventBaseDto
    {
        public CompleteCncEto()
        {

        }

        /// <param name="orderNo"></param>
        public CompleteCncEto(string orderNo)
        {
            OrderNo = orderNo;
        }
    }
}
