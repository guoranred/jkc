using Jiepei.ERP.Suppliers.Suppliers.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Suppliers.Suppliers
{
    public interface ISupplierAppService : IApplicationService
    {
        Task<SupplierDto> GetByIdAsync(Guid id);

        Task<List<SupplierDto>> GetListAsync(List<string> codes);
        Task<List<SupplierDto>> GetListAsync(List<Guid> id);
        Task<List<SupplierDto>> GetListAsync();
    }
}
