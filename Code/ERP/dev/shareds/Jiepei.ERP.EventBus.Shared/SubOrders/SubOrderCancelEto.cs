using Jiepei.ERP.Orders;
using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.SubOrders
{
    [EventName("Erp.SubOrder.CancelChange")]
    public class SubOrderCancelEto : OrderEventBaseDto
    {
        public SubOrderCancelEto()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="orderType"></param>
        public SubOrderCancelEto(string orderNo, EnumOrderType orderType)
        {
            OrderNo = orderNo;
            OrderType = orderType;
        }
    }
}
