using Jiepei.ERP.Molds;
using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.Molds
{
    /// <summary>
    /// 模具订单审核 事件总线ETO
    /// </summary>
    [EventName("Erp.Mold.CheckChange")]
    public class CheckMoldEto : OrderEventBaseDto
    {
        /// <summary>
        /// 状态
        /// </summary>
        public EnumMoldOrderStatus Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CheckMoldEto()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="status">状态</param>
        /// <param name="remark"></param>
        public CheckMoldEto(string orderNo, EnumMoldOrderStatus status, string remark)
        {
            OrderNo = orderNo;
            Status = status;
            Remark = remark;
        }
    }
}
