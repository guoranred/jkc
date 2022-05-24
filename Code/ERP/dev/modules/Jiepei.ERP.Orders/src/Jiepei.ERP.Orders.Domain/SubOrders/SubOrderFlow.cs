using Jiepei.ERP.SubOrders;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Orders.SubOrders
{
    public class SubOrderFlow : CreationAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 子订单编号
        /// </summary>
        public virtual string OrderNo { get; set; }
        /// <summary>
        /// 流程类型：审批、报价等
        /// </summary>
        public virtual EnumSubOrderFlowType Type { get; set; }
        /// <summary>
        /// 操作记录
        /// </summary>
        public virtual string Content { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }

        protected SubOrderFlow() { }

        public SubOrderFlow(Guid id, string orderNo, EnumSubOrderFlowType type, string content, string remark)
        {
            Id = id;
            OrderNo = orderNo;
            Type = type;
            Content = content;
            Remark = remark;
        }

    }
}
