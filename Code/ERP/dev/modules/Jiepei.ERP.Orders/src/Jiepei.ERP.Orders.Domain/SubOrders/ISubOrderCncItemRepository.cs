using System;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.SubOrders
{
    public interface ISubOrderCncItemRepository : IRepository<SubOrderCncItem, Guid>
    {
    }
}
