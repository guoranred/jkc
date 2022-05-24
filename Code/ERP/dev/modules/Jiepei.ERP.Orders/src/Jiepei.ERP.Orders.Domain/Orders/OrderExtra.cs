using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jiepei.ERP.Orders.Orders
{
    public class OrderExtra : AuditedAggregateRoot<Guid>
    {
        public Guid OrderId { get; set; }
    }
}
