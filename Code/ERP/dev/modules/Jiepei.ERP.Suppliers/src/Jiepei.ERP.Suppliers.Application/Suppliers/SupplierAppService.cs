using Jiepei.ERP.Suppliers.Suppliers.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jiepei.ERP.Suppliers.Suppliers
{
    public class SupplierAppService : SuppliersAppService, ISupplierAppService
    {
        private readonly ISupplierManager _supplierManager;
        private readonly ISupplierRepository _supplierRepository;

        public SupplierAppService(ISupplierManager supplierManager, ISupplierRepository supplierRepository)
        {
            _supplierManager = supplierManager;
            _supplierRepository = supplierRepository;
        }

        public async Task<SupplierDto> GetByIdAsync(Guid id)
        {
            var entity = await _supplierManager.GetByIdAsync(id);
            return ObjectMapper.Map<Supplier, SupplierDto>(entity);
        }

        public async Task<List<SupplierDto>> GetListAsync(List<string> codes)
        {
            var result = await _supplierRepository.GetListAsync(t => codes.Contains(t.Code) && t.IsEnable == true);
            return ObjectMapper.Map<List<Supplier>, List<SupplierDto>>(result);
        }

        public async Task<List<SupplierDto>> GetListAsync(List<Guid> id)
        {
            var result = await _supplierRepository.GetListAsync(t => id.Contains(t.Id) && t.IsEnable == true);
            return ObjectMapper.Map<List<Supplier>, List<SupplierDto>>(result);
        }

        public async Task<List<SupplierDto>> GetListAsync()
        {
            var result = await _supplierRepository.GetListAsync(t => t.IsEnable == true);
            return ObjectMapper.Map<List<Supplier>, List<SupplierDto>>(result);
        }
    }
}
