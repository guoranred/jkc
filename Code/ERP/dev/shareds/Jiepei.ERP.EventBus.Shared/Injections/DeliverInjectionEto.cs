using System;
using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Injections
{
    [EventName("Erp.Injection.DeliverChange")]
    public class DeliverInjectionEto : OrderEventBaseDto
    {
        public DeliverInjectionEto()
        {

        }

        public DeliverInjectionEto(string orderNo)
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
