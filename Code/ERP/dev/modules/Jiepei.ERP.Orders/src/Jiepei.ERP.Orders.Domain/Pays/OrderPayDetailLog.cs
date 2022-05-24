using Jiepei.ERP.Shared.Enums.Pays;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Orders.Pays
{
    public class OrderPayDetailLog : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 外键
        /// </summary>
        public virtual Guid PayLogId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public virtual string OrderNo { get; set; }

        /// <summary>
        /// 销售金额
        /// </summary>
        public virtual decimal SellingMoney { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public virtual decimal DiscountAmount { get; set; }

        /// <summary>
        /// 流水类型
        /// </summary>
        public virtual EnumOrderFlowType FlowType { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public virtual bool IsSuccess { get; set; }
    }
}
