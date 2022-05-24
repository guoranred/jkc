using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.MoldOrders
{
    public class MoldOrderRepository : EfCoreRepository<IOrdersDbContext, MoldOrder, Guid>, IMoldOrderRepository
    {
        public MoldOrderRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
