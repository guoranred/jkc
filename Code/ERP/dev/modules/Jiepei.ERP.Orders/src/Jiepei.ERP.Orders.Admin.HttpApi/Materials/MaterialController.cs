using Jiepei.ERP.Orders.Admin;
using Jiepei.ERP.Orders.Admin.Application.Contracts.Materials.Dtos;
using Jiepei.ERP.Orders.Admin.HttpApi;
using Jiepei.ERP.Orders.Materials.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Orders.Materials
{
    /// <summary>
    /// 材料
    /// </summary>
    [RemoteService(Name = OrdersAdminRemoteServiceConsts.RemoteServiceName)]
    [Route("api/orders/material")]
    public class MaterialController : OrdersAdminController
    {
        private readonly IMaterialAppService _materialAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="materialAppService"></param>
        public MaterialController(IMaterialAppService materialAppService)
        {
            _materialAppService = materialAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("3dmaterial")]
        public async Task<PagedResultDto<D3MaterialDto>> GetListD3MaterialAsync(GetMaterialListDto input)
        {
            var result = await _materialAppService.GetListD3MaterialAsync(input);
            return result;
        }

        /// <summary>
        /// 添加3d材料
        /// </summary>
        /// <returns></returns>
        [HttpPost("create-material")]
        public async Task CreateD3MaterialAsync(CreateD3MaterialExtraDto d3Material)
        {
            await _materialAppService.CreateD3MaterialAsync(d3Material);
        }

        /// <summary>
        /// 修改3d材料
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}/edit-3dmaterial")]
        public async Task UpdateD3MaterialAsync(Guid id, UpdateD3MaterialExtraDto updateD3MaterialExtraDto)
        {
            await _materialAppService.UpdateD3MaterialAsync(id, updateD3MaterialExtraDto);
        }
        /// <summary>
        /// 删除3d材料
        /// </summary>
        /// <returns></returns>
        [HttpDelete("3dmaterial/{id}")]
        public async Task DeleteD3MaterialAsync(Guid id)
        {
            await _materialAppService.DeleteD3MaterialAsync(id);
        }

        /// <summary>
        /// 获取3d材料渠道配置列表
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [HttpGet("materialprice")]
        public async Task<List<MaterialPriceDto>> GetListMaterialPriceAsync(Guid materialId)
        {
            return await _materialAppService.GetListMaterialPriceAsync(materialId);
        }

        /// <summary>
        /// 添加3d材料渠道配置
        /// </summary>
        /// <returns></returns>
        [HttpPost("create-materialprice")]
        public async Task CreateMaterialPriceAsync([FromBody] CreateMaterialPriceDto materialPrice)
        {
            await _materialAppService.CreateMaterialPriceAsync(materialPrice);
        }

        /// <summary>
        ///修改3d材料渠道配置
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}/edit-materialprice")]
        public async Task UpdateMaterialPriceAsync(Guid id, UpdateMaterialPriceDto updateMaterialPriceDto)
        {

            await _materialAppService.UpdateMaterialPriceAsync(id, updateMaterialPriceDto);
        }

        /// <summary>
        /// 删除3d材料渠道配置
        /// </summary>
        /// <returns></returns>
        [HttpDelete("materialprice/{id}")]
        public async Task DeleteMaterialPriceAsync(Guid id)
        {

            await _materialAppService.DeleteMaterialPriceAsync(id);
        }
        /// <summary>
        /// 材料供应商查询
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [HttpGet("materialsupplier")]
        public async Task<List<MaterialSupplierDto>> GetMaterialSupplierListAsync(Guid materialId)
        {
            return await _materialAppService.GetMaterialSupplierListAsync(materialId);
        }

        /// <summary>
        /// 供应商配置添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("materialsupplier")]
        public async Task CreateMaterialSupplierAsync(CreateMaterialSupplierDto input)
        {
            await _materialAppService.CreateMaterialSupplierAsync(input);
        }
        /// <summary>
        /// 供应商配置修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("materialsupplier/{id}")]
        public async Task UpdateMaterialSupplierAsync(Guid id, CreateMaterialSupplierDto input)
        {
            await _materialAppService.UpdateMaterialSupplierAsync(id, input);
        }

        /// <summary>
        /// 供应商配置删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("materialsupplier/{id}")]
        public async Task DeleteMaterialSupplierAsync(Guid id)
        {
            await _materialAppService.DeleteMaterialSupplierAsync(id);

        }
    }
}
