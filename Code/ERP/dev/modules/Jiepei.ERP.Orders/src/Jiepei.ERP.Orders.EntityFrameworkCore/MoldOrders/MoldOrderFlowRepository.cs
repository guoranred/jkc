using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.MoldOrders
{
    public class MoldOrderFlowRepository : EfCoreRepository<IOrdersDbContext, MoldOrderFlow, Guid>, IMoldOrderFlowRepository
    {
        public MoldOrderFlowRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
