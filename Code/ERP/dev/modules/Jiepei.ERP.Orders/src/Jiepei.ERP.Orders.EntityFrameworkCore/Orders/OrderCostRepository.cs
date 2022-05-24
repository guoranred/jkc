using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.Orders
{
    public class OrderCostRepository : EfCoreRepository<IOrdersDbContext, OrderCost, Guid>, IOrderCostRepository
    {
        public OrderCostRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) : base(dbContextProvider)
        { }
    }
}
