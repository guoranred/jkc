using System;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.Pays
{
    public interface IOrderPayDetailLogRepository : IRepository<OrderPayDetailLog, Guid>
    {
    }
}
