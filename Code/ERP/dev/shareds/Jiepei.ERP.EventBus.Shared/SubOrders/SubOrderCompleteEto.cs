using Jiepei.ERP.Orders;
using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.SubOrders
{
    [EventName("Erp.SubOrder.CompleteChange")]
    public class SubOrderCompleteEto : OrderEventBaseDto
    {
        public SubOrderCompleteEto()
        {

        }
        /// <summary>
        /// 渠道订单号
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="orderType"></param>
        public SubOrderCompleteEto(string orderNo, EnumOrderType orderType)
        {
            OrderNo = orderNo;
            OrderType = orderType;
        }
    }
}
