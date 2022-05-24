using Jiepei.ERP.SubOrders;
using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.SubOrders.Dtos
{
    public class SubOrderFlowDto : CreationAuditedEntityDto<Guid>
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
    }
}
