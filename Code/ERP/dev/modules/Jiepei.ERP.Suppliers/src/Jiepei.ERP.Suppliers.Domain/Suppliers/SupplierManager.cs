using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.Suppliers.Suppliers
{
    public class SupplierManager : DomainService, ISupplierManager
    {
        private readonly ISupplierRepository _supplierRepository;
        public SupplierManager(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<Supplier> GetByIdAsync(Guid id)
        {
            return await _supplierRepository.GetAsync(t => t.Id == id);
        }
    }
}
