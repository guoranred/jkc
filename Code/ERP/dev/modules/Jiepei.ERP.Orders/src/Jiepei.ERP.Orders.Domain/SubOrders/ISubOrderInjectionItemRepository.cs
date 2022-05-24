using System;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.SubOrders
{
    public interface ISubOrderInjectionItemRepository : IRepository<SubOrderInjectionItem, Guid>
    {
    }
}
