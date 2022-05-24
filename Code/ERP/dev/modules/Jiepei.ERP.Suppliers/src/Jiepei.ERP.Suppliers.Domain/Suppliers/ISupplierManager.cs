using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.Suppliers.Suppliers
{
    public interface ISupplierManager : IDomainService
    {
        Task<Supplier> GetByIdAsync(Guid id);
    }
}
