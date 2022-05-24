using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.Orders
{
    public class OrderRepository : EfCoreRepository<IOrdersDbContext, Order, Guid>, IOrderRepository
    {
        public OrderRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
