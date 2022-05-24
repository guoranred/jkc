using Jiepei.ERP.Orders;
using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.SubOrders
{
    [EventName("Erp.SubOrder.ManufactureChange")]
    public class SubOrderManufactureEto : OrderEventBaseDto
    {
        /// <summary>
        /// 
        /// </summary>
        public SubOrderManufactureEto() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="orderType"></param>
        public SubOrderManufactureEto(string orderNo, EnumOrderType orderType)
        {
            OrderNo = orderNo;
            OrderType = orderType;
        }
    }
}
