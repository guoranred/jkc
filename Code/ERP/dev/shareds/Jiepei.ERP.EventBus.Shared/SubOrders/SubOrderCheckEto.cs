using Jiepei.ERP.Orders;
using Jiepei.ERP.SubOrders;
using Volo.Abp.EventBus;

namespace Jiepei.ERP.EventBus.Shared.SubOrders
{
    /// <summary>
    /// 注塑订单审核 事件总线ETO
    /// </summary>
    [EventName("Erp.SubOrder.CheckChange")]
    public class SubOrderCheckEto : OrderEventBaseDto
    { /// <summary>
      /// 状态
      /// </summary>
        public EnumSubOrderStatus Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SubOrderCheckEto()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="status">状态</param>
        /// <param name="remark"></param>
        /// <param name="orderType"></param>
        public SubOrderCheckEto(string orderNo, EnumSubOrderStatus status, string remark, EnumOrderType orderType)
        {
            OrderNo = orderNo;
            Status = status;
            Remark = remark;
            OrderType = orderType;
        }
    }
}
