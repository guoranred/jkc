using Jiepei.ERP.Cncs;
using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Cncs
{
    /// <summary>
    /// 注塑订单审核 事件总线ETO
    /// </summary>
    [EventName("Erp.Cnc.CheckChange")]
    public class CheckCncEto : OrderEventBaseDto
    { /// <summary>
      /// 状态
      /// </summary>
        public EnumCncOrderStatus Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CheckCncEto()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="status">状态</param>
        /// <param name="remark"></param>
        public CheckCncEto(string orderNo, EnumCncOrderStatus status, string remark)
        {
            OrderNo = orderNo;
            Status = status;
            Remark = remark;
        }
    }
}
