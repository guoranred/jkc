using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.InjectionOrders
{
    public class InjectionOrderRepository : EfCoreRepository<IOrdersDbContext, InjectionOrder, Guid>, IInjectionOrderRepository
    {
        public InjectionOrderRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

    }
}
