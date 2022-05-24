using System;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Suppliers.Suppliers
{
    public interface ISupplierRepository : IRepository<Supplier, Guid>
    {
    }
}
