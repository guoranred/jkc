using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.SubOrders
{
    public class SubOrderCncItemRepository : EfCoreRepository<IOrdersDbContext, SubOrderCncItem, Guid>, ISubOrderCncItemRepository
    {
        public SubOrderCncItemRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
