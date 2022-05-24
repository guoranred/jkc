using Jiepei.ERP.Injections;
using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Injections
{
    /// <summary>
    /// 注塑订单审核 事件总线ETO
    /// </summary>
    [EventName("Erp.Injection.CheckChange")]
    public class CheckInjectionEto : OrderEventBaseDto
    { /// <summary>
      /// 状态
      /// </summary>
        public EnumInjectionOrderStatus Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CheckInjectionEto()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="status">状态</param>
        /// <param name="remark"></param>
        public CheckInjectionEto(string orderNo, EnumInjectionOrderStatus status, string remark)
        {
            OrderNo = orderNo;
            Status = status;
            Remark = remark;
        }
    }
}
