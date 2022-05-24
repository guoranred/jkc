using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.Pays
{
    public class OrderPayDetailLogRepository : EfCoreRepository<IOrdersDbContext, OrderPayDetailLog, Guid>, IOrderPayDetailLogRepository
    {
        public OrderPayDetailLogRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
