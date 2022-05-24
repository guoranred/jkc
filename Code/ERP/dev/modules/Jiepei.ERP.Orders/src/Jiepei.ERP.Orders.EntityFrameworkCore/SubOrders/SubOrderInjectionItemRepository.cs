using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.SubOrders
{
    public class SubOrderInjectionItemRepository : EfCoreRepository<IOrdersDbContext, SubOrderInjectionItem, Guid>, ISubOrderInjectionItemRepository
    {
        public SubOrderInjectionItemRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
