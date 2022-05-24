using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.SubOrders
{
    public class SubOrderSheetMetalItemRepository : EfCoreRepository<IOrdersDbContext, SubOrderSheetMetalItem, Guid>, ISubOrderSheetMetalItemRepository
    {
        public SubOrderSheetMetalItemRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
