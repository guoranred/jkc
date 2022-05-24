using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.News.Admin
{
    public interface IBannerManagementAppService : IApplicationService
    {
        /// <summary>
        /// 分页获取Banner列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<BannerManagementDto>> GetListAsync(BannerManagementQueryDto input);

        /// <summary>
        /// 根据主键ID获取Banner信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BannerManagementDto> GetAsync(Guid id);

        /// <summary>
        /// 添加一条Banner
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        Task<BannerManagementDto> CreateAsync(CreateBannerManagementInput inputDto);

        /// <summary>
        /// 根据主键ID修改Banner
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        Task<BannerManagementDto> UpdateAsync(Guid id, UpdateBannerManagementInput inputDto);

        /// <summary>
        /// 根据主键ID修改Banner启用状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        Task<BannerManagementDto> UpdateEnableStatusAsync(Guid id, UpdateBannerManagementEnableStatusInput inputDto);

        /// <summary>
        /// 根据主键ID删除Banner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
    }
}
