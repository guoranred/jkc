using Jiepei.ERP.Suppliers.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Suppliers.Suppliers
{
    public class SupplierRepository
        : EfCoreRepository<ISuppliersDbContext, Supplier, Guid>, ISupplierRepository
    {
        public SupplierRepository(IDbContextProvider<ISuppliersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
