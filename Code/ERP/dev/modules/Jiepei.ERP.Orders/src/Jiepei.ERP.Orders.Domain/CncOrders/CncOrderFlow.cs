using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Orders.CncOrders
{
    public class CncOrderFlow : CreationAuditedAggregateRoot<Guid>
    {
        protected CncOrderFlow() { }

        public CncOrderFlow(Guid id, string orderNo, int type, string remark, string note)
        {
            Id = id;
            OrderNo = orderNo;
            Type = type;
            Remark = remark;
            Note = note;
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 流程类型：审批、报价等
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 操作记录
        /// </summary>
        public string Note { get; set; }
    }
}
