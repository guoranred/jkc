using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.SubOrders
{
    public class SubOrderThreeDItemRepository : EfCoreRepository<IOrdersDbContext, SubOrderThreeDItem, Guid>, ISubOrderThreeDItemRepository
    {
        public SubOrderThreeDItemRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
