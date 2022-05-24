using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.News.Admin
{
    public interface IColumnTypeManagementAppService : IApplicationService
    {
        /// <summary>
        /// 创建栏目类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ColumnTypeDto> CreateAsync(CreateColumnTypeInput input);

        /// <summary>
        /// 删除栏目类型
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid Id);

        /// <summary>
        /// 获取所有栏目类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ColumnTypeDto>> GetListAsync(ColumnTypeQueryInput input);

        /// <summary>
        /// 修改栏目类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ColumnTypeDto> UpdateAsync(Guid id, UpdateColumnTypeInput input);
    }
}
