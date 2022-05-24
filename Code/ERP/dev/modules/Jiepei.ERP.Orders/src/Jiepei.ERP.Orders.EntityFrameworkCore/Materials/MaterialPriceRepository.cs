using Jiepei.ERP.Orders.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.Materials
{
    public class MaterialPriceRepository : EfCoreRepository<IOrdersDbContext, MaterialPrice, Guid>, IMaterialPriceRepository
    {
        public MaterialPriceRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
