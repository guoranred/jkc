using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Molds
{
    /// <summary>
    /// 模具订单完成 事件总线ETO
    /// </summary>
    [EventName("Erp.Mold.CompleteChange")]
    public class CompleteMoldEto : OrderEventBaseDto
    {
        /// <summary>
        /// 
        /// </summary>
        public CompleteMoldEto()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        public CompleteMoldEto(string orderNo)
        {
            OrderNo = orderNo;
        }
    }
}
