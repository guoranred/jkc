using System;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.MoldOrders
{
    public interface IMoldOrderRepository : IRepository<MoldOrder, Guid>
    {
    }
}
