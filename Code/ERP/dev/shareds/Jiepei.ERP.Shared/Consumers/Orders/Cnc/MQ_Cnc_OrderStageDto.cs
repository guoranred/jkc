using Jiepei.ERP.Orders;
using System;

namespace Jiepei.ERP.Shared.Consumers.Orders
{
    /// <summary>
    /// 模具渠道订单分期付款同步定义
    /// </summary>
    public class MQ_Cnc_OrderStageDto : MQ_BaseOrderDto
    {
        /// <summary>
        /// 是否首期
        /// </summary>
        public bool IsFirstIssue { get; set; }

        /// <summary>
        /// 代付金额
        /// </summary>
        public decimal? PendingMoney { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        public decimal? PaidMoney { get; set; }

        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime? PayTime { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public EnumPayMode? PayModel { get; set; }
    }
}
