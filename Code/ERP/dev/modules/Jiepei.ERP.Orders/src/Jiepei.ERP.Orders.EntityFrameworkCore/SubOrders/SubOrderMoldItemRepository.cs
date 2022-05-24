using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.SubOrders
{
    public class SubOrderMoldItemRepository : EfCoreRepository<IOrdersDbContext, SubOrderMoldItem, Guid>, ISubOrderMoldItemRepository
    {
        public SubOrderMoldItemRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
