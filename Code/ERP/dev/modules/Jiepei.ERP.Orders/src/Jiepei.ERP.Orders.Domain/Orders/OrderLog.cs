using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Orders.Orders
{
    public class OrderLog : CreationAuditedEntity<Guid>
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public virtual string OrderNo { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public virtual string Content { get; set; }

        protected OrderLog() { }

        public OrderLog(Guid id, string orderNo, string content)
        {
            Id = id;
            OrderNo = orderNo;
            Content = content;
        }
    }
}
