using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Orders.Pays
{
    public class OrderPayLog : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 支付单号
        /// </summary>
        public virtual string PayCode { get; set; }

        /// <summary>
        /// 客户id
        /// </summary>
        public virtual Guid MemberId { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public virtual string MemberName { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public virtual int PayType { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public virtual decimal TotalAmout { get; set; }

        /// <summary>
        /// 是否支付成功
        /// </summary>
        public virtual bool IsPaySuccess { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }

    }
}
