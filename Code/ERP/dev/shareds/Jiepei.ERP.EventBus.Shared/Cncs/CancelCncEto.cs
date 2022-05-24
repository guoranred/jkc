using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Cncs
{
    [EventName("Erp.Cnc.CancelChange")]
    public class CancelCncEto : OrderEventBaseDto
    {
        public CancelCncEto()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo">订单号</param>
        public CancelCncEto(string orderNo)
        {
            OrderNo = orderNo;
        }
    }
}
