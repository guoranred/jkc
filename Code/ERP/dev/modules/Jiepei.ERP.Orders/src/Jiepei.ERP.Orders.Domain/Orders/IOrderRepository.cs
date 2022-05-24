using System;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.Orders
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
    }
}
