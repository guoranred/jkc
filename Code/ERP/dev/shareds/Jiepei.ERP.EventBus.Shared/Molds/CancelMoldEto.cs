using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Molds
{
    /// <summary>
    /// 模具订单取消 事件总线ETO
    /// </summary>
    [EventName("Erp.Mold.CancelChange")]
    public class CancelMoldEto : OrderEventBaseDto
    {
        /// <summary>
        /// 
        /// </summary>
        public CancelMoldEto()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo">订单号</param>
        public CancelMoldEto(string orderNo)
        {
            OrderNo = orderNo;
        }
    }
}
