using Jiepei.ERP.Orders.Admin.Application.Contracts.Materials.Dtos;
using Jiepei.ERP.Orders.Materials.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Orders.Materials
{
    public interface IMaterialAppService : IApplicationService
    {
        /// <summary>
        /// 获取3d材料列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<D3MaterialDto>> GetListD3MaterialAsync(GetMaterialListDto input);

        /// <summary>
        /// 添加3d材料
        /// </summary>
        /// <returns></returns>
        Task CreateD3MaterialAsync(CreateD3MaterialExtraDto d3Material);

        /// <summary>
        /// 修改3d材料
        /// </summary>
        /// <returns></returns>
        Task UpdateD3MaterialAsync(Guid id, UpdateD3MaterialExtraDto updateD3MaterialExtraDto);

        /// <summary>
        /// 删除3d材料
        /// </summary>
        /// <returns></returns>
        Task DeleteD3MaterialAsync(Guid guid);

        /// <summary>
        /// 获取3d材料渠道配置列表
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        Task<List<MaterialPriceDto>> GetListMaterialPriceAsync(Guid materialId);

        /// <summary>
        /// 添加3d材料渠道配置
        /// </summary>
        /// <returns></returns>
        Task CreateMaterialPriceAsync(CreateMaterialPriceDto materialPrice);

        /// <summary>
        ///修改3d材料渠道配置
        /// </summary>
        /// <returns></returns>
        Task UpdateMaterialPriceAsync(Guid id, UpdateMaterialPriceDto updateMaterialPriceDto);

        /// <summary>
        /// 删除3d材料渠道配置
        /// </summary>
        /// <returns></returns>
        Task DeleteMaterialPriceAsync(Guid guid);
        /// <summary>
        /// 材料供应商查询
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        Task<List<MaterialSupplierDto>> GetMaterialSupplierListAsync(Guid materialId);
        /// <summary>
        /// 供应商配置添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateMaterialSupplierAsync(CreateMaterialSupplierDto input);
        /// <summary>
        /// 供应商配置修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateMaterialSupplierAsync(Guid id, CreateMaterialSupplierDto input);

        /// <summary>
        /// 供应商配置删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteMaterialSupplierAsync(Guid id);
    }
}
