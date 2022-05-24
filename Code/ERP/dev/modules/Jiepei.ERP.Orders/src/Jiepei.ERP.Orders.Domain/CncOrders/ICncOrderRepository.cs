using System;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.CncOrders
{
    public interface ICncOrderRepository : IRepository<CncOrder, Guid>
    {

    }
}