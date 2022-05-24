using Jiepei.ERP.Orders;
using System;
using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.SubOrders
{
    [EventName("Erp.SubOrder.DeliverChange")]
    public class SubOrderDeliverEto : OrderEventBaseDto
    {
        public SubOrderDeliverEto()
        {

        }

        public SubOrderDeliverEto(string orderNo, EnumOrderType orderType)
        {
            OrderNo = orderNo;
            OrderType = orderType;
        }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string TrackingNo { get; set; }

        /// <summary>
        /// 快递公司
        /// </summary>
        public string CourierCompany { get; set; }

        public DateTime SendTime { get; set; }
    }
}
