
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.News.Admin.Application.ColumnTypes
{
    /// <summary>
    /// 
    /// </summary>
    public class ColumnTypeManagementAppService : NewsAdminAppService, IColumnTypeManagementAppService
    {
        private readonly IColumnTypeRepository _columnTypeRepository;
        private readonly IColumnTypeManage _columnTypeManage;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnTypeRepository"></param>
        /// <param name="columnTypeManage"></param>
        public ColumnTypeManagementAppService(IColumnTypeRepository columnTypeRepository, IColumnTypeManage columnTypeManage)
        {
            _columnTypeRepository = columnTypeRepository;
            _columnTypeManage = columnTypeManage;
        }
        /// <summary>
        /// 创建栏目类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ColumnTypeDto> CreateAsync(CreateColumnTypeInput input)
        {
            var columnType = await this._columnTypeManage.CreateColumnType(new Guid(), input.Pid, "", input.Name, input.Alias, input.Type, input.LogoImage, input.Remark, input.ColumnOwnership);
            return ObjectMapper.Map<ColumnType, ColumnTypeDto>(columnType);
        }

        /// <summary>
        /// 删除栏目类型
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid Id)
        {
            await this._columnTypeRepository.DeleteAsync(Id);
        }

        /// <summary>
        /// 获取所有栏目类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ColumnTypeDto>> GetListAsync(ColumnTypeQueryInput input)
        {

            var query = await _columnTypeRepository.GetQueryableAsync();

            var count = await AsyncExecuter.CountAsync(query);
            var result = await AsyncExecuter.ToListAsync(query);

            var columnTypeDtos = ObjectMapper.Map<List<ColumnType>, List<ColumnTypeDto>>(result);

            return new PagedResultDto<ColumnTypeDto>(count, columnTypeDtos);
        }

        /// <summary>
        /// 修改栏目类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ColumnTypeDto> UpdateAsync(Guid id, UpdateColumnTypeInput input)
        {
            var columnType = await this._columnTypeManage.UpdateColumnType(id, input.Pid, input.Name, input.Alias, input.Type, input.LogoImage, input.Remark, input.ColumnOwnership);
            return ObjectMapper.Map<ColumnType, ColumnTypeDto>(columnType);
        }

    }
}
