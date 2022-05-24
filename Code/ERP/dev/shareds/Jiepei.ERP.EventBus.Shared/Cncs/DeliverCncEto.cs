using System;
using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Cncs
{
    [EventName("Erp.Cnc.DeliverChange")]
    public class DeliverCncEto : OrderEventBaseDto
    {
        public DeliverCncEto()
        {

        }

        public DeliverCncEto(string orderNo)
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
