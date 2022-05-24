using System;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.Orders
{
    public interface IOrderCostRepository : IRepository<OrderCost, Guid>
    {
    }
}
