using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.Pays
{
    public class OrderPayLogRepository : EfCoreRepository<IOrdersDbContext, OrderPayLog, Guid>, IOrderPayLogRepository
    {
        public OrderPayLogRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
