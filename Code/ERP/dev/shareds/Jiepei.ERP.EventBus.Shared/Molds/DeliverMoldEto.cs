using System;
using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Molds
{
    /// <summary>
    /// 模具订单发货 事件总线ETO
    /// </summary>
    [EventName("Erp.Mold.DeliverChange")]
    public class DeliverMoldEto : OrderEventBaseDto
    {
        public DeliverMoldEto()
        {

        }

        public DeliverMoldEto(string orderNo)
        {
            OrderNo = orderNo;
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
