using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.Orders
{
    public class OrderLogRepository : EfCoreRepository<IOrdersDbContext, OrderLog, Guid>, IOrderLogRepository
    {
        public OrderLogRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
