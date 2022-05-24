using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Jiepei.ERP.Orders.MoldOrders
{
    public class MoldOrderFlow : CreationAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; protected set; }
        /// <summary>
        /// 流程类型：审批、报价等
        /// </summary>
        public int Type { get; protected set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; protected set; }
        /// <summary>
        /// 操作记录
        /// </summary>
        public string Note { get; protected set; }


        protected MoldOrderFlow() { }

        public MoldOrderFlow(Guid id, string orderNo, int type, string remark, string note)
        {
            Id = id;
            OrderNo = orderNo;
            Type = type;
            Remark = remark;
            Note = note;
        }
    }
}
