using Jiepei.ERP.Orders.Admin;
using Jiepei.ERP.Orders.Admin.Application.Contracts.Materials.Dtos;
using Jiepei.ERP.Orders.Channels;
using Jiepei.ERP.Orders.Materials.Dtos;
using Jiepei.ERP.Suppliers.Suppliers;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.Materials
{
    /// <summary>
    /// 材料
    /// </summary>
    [Authorize]
    public class MaterialAppService : OrdersAdminAppServiceBase, IMaterialAppService
    {

        private readonly ID3MaterialRepository _d3MaterialRepository;
        private readonly IMaterialPriceRepository _materialPriceRepository;
        private readonly IMaterialManager _materialManager;
        private readonly IRepository<MaterialSupplier> _materialSupplierRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IChannelAppService _channelAppService;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="d3MaterialRepository"></param>
        /// <param name="materialPriceRepository"></param>
        /// <param name="materialManager"></param>
        /// <param name="materialSupplierRepository"></param>
        /// <param name="supplierRepository"></param>
        /// <param name="channelAppService"></param>
        public MaterialAppService(ID3MaterialRepository d3MaterialRepository
            , IMaterialPriceRepository materialPriceRepository
            , IMaterialManager materialManager
            , IRepository<MaterialSupplier> materialSupplierRepository
            , ISupplierRepository supplierRepository
            , IChannelAppService channelAppService)
        {
            _d3MaterialRepository = d3MaterialRepository;
            _materialPriceRepository = materialPriceRepository;
            _materialManager = materialManager;
            _materialSupplierRepository = materialSupplierRepository;
            _supplierRepository = supplierRepository;
            _channelAppService = channelAppService;
        }

        #region 3D材料管理
        /// <summary>
        /// 3D材料列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<D3MaterialDto>> GetListD3MaterialAsync(GetMaterialListDto input)
        {
            var query = await _d3MaterialRepository.GetQueryableAsync();
            var query2 = query.WhereIf(!input.PartCode.IsNullOrEmpty(), t => t.Name.Contains(input.PartCode.Trim()) || t.PartCode.Contains(input.PartCode.Trim()));
            var totalCount = await AsyncExecuter.CountAsync(query2);
            query2 = query2.OrderByDescending(t => t.CreationTime).PageBy(input.SkipCount, input.MaxResultCount);
            var orders = await AsyncExecuter.ToListAsync(query2);

            var dtos = new List<D3MaterialDto>();
            foreach (var order in orders)
            {
                var item = ObjectMapper.Map<D3Material, D3MaterialDto>(order);
                dtos.Add(item);
            }
            return new PagedResultDto<D3MaterialDto>(totalCount, dtos);
        }

        /// <summary>
        /// 添加3d材料
        /// </summary>
        /// <returns></returns>
        public async Task CreateD3MaterialAsync(CreateD3MaterialExtraDto createD3MaterialExtraDto)
        {
            var d3Material = ObjectMapper.Map<CreateD3MaterialExtraDto, D3Material>(createD3MaterialExtraDto);
            await _d3MaterialRepository.InsertAsync(d3Material);
        }

        /// <summary>
        /// 修改3d材料
        /// </summary>
        /// <returns></returns>
        public async Task UpdateD3MaterialAsync(Guid id, UpdateD3MaterialExtraDto updateD3MaterialExtraDto)
        {
            var entiy = await _d3MaterialRepository.GetAsync(t => t.Id == id);
            var d3Material = ObjectMapper.Map<UpdateD3MaterialExtraDto, D3Material>(updateD3MaterialExtraDto, entiy);
            await _d3MaterialRepository.UpdateAsync(d3Material);
        }
        /// <summary>
        /// 删除3d材料
        /// </summary>
        /// <returns></returns>
        public async Task DeleteD3MaterialAsync(Guid guid)
        {
            await _d3MaterialRepository.DeleteAsync(guid);
        }
        #endregion

        #region 材料渠道管理

        /// <summary>
        /// 获取3d材料渠道配置列表
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public async Task<List<MaterialPriceDto>> GetListMaterialPriceAsync(Guid materialId)
        {
            var materialPrices = await _materialPriceRepository.GetListAsync(t => t.MaterialId == materialId);
            var channelList = await _channelAppService.GetListAsync(materialPrices.Select(t => t.ChannelId).ToList());
            var dtos = new List<MaterialPriceDto>();
            foreach (var entity in materialPrices ?? new List<MaterialPrice>())
            {
                var d3Material = ObjectMapper.Map<MaterialPrice, MaterialPriceDto>(entity);
                d3Material.ChannelName = channelList.Where(t => t.Id == entity.ChannelId).First().ChannelName;
                dtos.Add(d3Material);
            }
            return dtos;
        }

        /// <summary>
        /// 添加3d材料渠道配置
        /// </summary>
        /// <returns></returns>
        public async Task CreateMaterialPriceAsync(CreateMaterialPriceDto input)
        {
            var entiy = await _materialPriceRepository.FindAsync(t => t.MaterialId == input.MaterialId);
            if (entiy != null)
                throw new UserFriendlyException("该材料已添加此渠道配置信息！");
            var materialPrice = ObjectMapper.Map<CreateMaterialPriceDto, MaterialPrice>(input);

            await _materialPriceRepository.InsertAsync(materialPrice);
        }

        /// <summary>
        ///修改3d材料渠道配置
        /// </summary>
        /// <returns></returns>
        public async Task UpdateMaterialPriceAsync(Guid id, UpdateMaterialPriceDto updateMaterialPriceDto)
        {
            var entiy = await _materialPriceRepository.GetAsync(t => t.Id == id);
            var materialPrice = ObjectMapper.Map(updateMaterialPriceDto, entiy);
            await _materialPriceRepository.UpdateAsync(materialPrice);
        }

        /// <summary>
        /// 删除3d材料渠道配置
        /// </summary>
        /// <returns></returns>
        public async Task DeleteMaterialPriceAsync(Guid guid)
        {
            await _materialPriceRepository.DeleteAsync(guid);
        }
        #endregion

        #region 供应商配置
        /// <summary>
        /// 材料供应商查询
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public async Task<List<MaterialSupplierDto>> GetMaterialSupplierListAsync(Guid materialId)
        {
            var materialSupplier = await _materialSupplierRepository.GetListAsync(t => t.MaterialId == materialId);
            var dtos = new List<MaterialSupplierDto>();
            foreach (var entity in materialSupplier ?? new List<MaterialSupplier>())
            {
                var d3Material = ObjectMapper.Map<MaterialSupplier, MaterialSupplierDto>(entity);
                dtos.Add(d3Material);
            }
            return dtos;

        }
        /// <summary>
        /// 供应商配置添加
        /// </summary>
        /// <returns></returns>
        public async Task CreateMaterialSupplierAsync(CreateMaterialSupplierDto input)
        {
            var entiy = await _supplierRepository.GetAsync(t => t.Id == input.SupplierId);
            if (entiy == null)
                throw new UserFriendlyException("无该供应商信息！");
            var materialSupplierEntiy = await _materialSupplierRepository.FindAsync(
               t => t.MaterialId == input.MaterialId && t.SupplierId == input.SupplierId
                );
            if (materialSupplierEntiy != null)
                throw new UserFriendlyException("已添加该供应商信息！");

            var materialSupplier = new MaterialSupplier(
                id: GuidGenerator.Create()
                , materialId: input.MaterialId
                , supplierId: input.SupplierId
                , supplierSpuId: input.SupplierSpuId
                , supplierName: entiy.Name
             );
            await _materialSupplierRepository.InsertAsync(materialSupplier);
        }

        /// <summary>
        /// 供应商配置修改
        /// </summary>
        /// <returns></returns>
        public async Task UpdateMaterialSupplierAsync(Guid id, CreateMaterialSupplierDto input)
        {
            var entiy = await _materialSupplierRepository.FindAsync(
                t => t.MaterialId == input.MaterialId
                && t.SupplierId == input.SupplierId
                && t.Id != id
                );
            if (entiy != null)
                throw new UserFriendlyException("已添加该供应商信息！");

            var materialSupplier = await _materialSupplierRepository.FindAsync(t => t.Id == id
                 && t.MaterialId == input.MaterialId
                 && t.SupplierId == input.SupplierId);

            materialSupplier.SupplierSpuId = input.SupplierSpuId;

            await _materialSupplierRepository.UpdateAsync(materialSupplier);
        }
        /// <summary>
        /// 供应商配置删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteMaterialSupplierAsync(Guid id)
        {
            var materialSupplier = await _materialSupplierRepository.GetAsync(t => t.Id == id);
            await _materialSupplierRepository.DeleteAsync(materialSupplier);
        }

        #endregion
    }
}
