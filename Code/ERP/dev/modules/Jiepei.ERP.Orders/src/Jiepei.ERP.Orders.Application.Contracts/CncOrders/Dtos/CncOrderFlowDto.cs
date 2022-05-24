using System;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.CncOrders.Dtos
{
    public class CncOrderFlowDto : CreationAuditedEntityDto<Guid>
    {
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
