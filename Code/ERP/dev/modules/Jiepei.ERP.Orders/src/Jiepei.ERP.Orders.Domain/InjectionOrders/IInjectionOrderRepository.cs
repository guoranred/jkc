using System;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.InjectionOrders
{
    public interface IInjectionOrderRepository : IRepository<InjectionOrder, Guid>
    {

    }
}