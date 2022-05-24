using Jiepei.ERP.Suppliers.Suppliers.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jiepei.ERP.Suppliers.Suppliers
{
    [Route("api/suppliers")]
    public class SupplierController : SuppliersController
    {
        private readonly ISupplierAppService _supplierAppService;
        public SupplierController(ISupplierAppService supplierAppService)
        {
            _supplierAppService = supplierAppService;
        }

        /// <summary>
        /// 查询供应商信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public async Task<SupplierDto> GetAsync(Guid id)
        {
            return await _supplierAppService.GetByIdAsync(id);
        }

        /// <summary>
        /// 获取供应商列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SupplierDto>> GetListAsync()
        {
            return await _supplierAppService.GetListAsync();
        }
    }
}
