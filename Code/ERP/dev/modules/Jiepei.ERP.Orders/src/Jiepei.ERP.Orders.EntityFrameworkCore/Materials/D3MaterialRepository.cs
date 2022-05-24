using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.Materials
{

    public class D3MaterialRepository : EfCoreRepository<IOrdersDbContext, D3Material, Guid>, ID3MaterialRepository
    {
        public D3MaterialRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
