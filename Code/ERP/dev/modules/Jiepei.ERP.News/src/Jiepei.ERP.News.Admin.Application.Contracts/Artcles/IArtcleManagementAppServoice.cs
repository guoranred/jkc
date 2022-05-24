using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.News.Admin
{
    public interface IArtcleManagementAppServoice : IApplicationService
    {
        /// <summary>
        /// 创建栏目类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ActicleListDto> CreateAsync(CreateActicleListInput input);

        /// <summary>
        /// 获取所有的栏目类型
        /// </summary>
        /// <returns></returns>
        Task<PagedResultDto<ActicleListDto>> GetListAsync(ActicleListQueryInput input);

        /// <summary>
        /// 更新栏目类型
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ActicleListDto> UpdateAsync(Guid id, UpdateActicleListInput input);

        /// <summary>
        /// 删除栏目类型
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid Id);

        /// <summary>
        /// 查看详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ActicleListDto> GetDetailAsync(Guid Id);
    }
}
