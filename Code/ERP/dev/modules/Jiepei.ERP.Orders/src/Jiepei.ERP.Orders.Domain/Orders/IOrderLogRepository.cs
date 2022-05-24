using System;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.Orders
{
    public interface IOrderLogRepository : IRepository<OrderLog, Guid>
    {
    }
}
