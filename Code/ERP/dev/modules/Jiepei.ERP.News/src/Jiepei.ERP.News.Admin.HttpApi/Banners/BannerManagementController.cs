using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.News.Admin
{
    [RemoteService]
    [Route("api/banner")]
    public class BannerManagementController : NewsAdminController
    {
        private readonly IBannerManagementAppService _bannerManagementAppService;

        public BannerManagementController(IBannerManagementAppService bannerManagementAppService)
        {
            _bannerManagementAppService = bannerManagementAppService;
        }

        /// <summary>
        /// 分页获取Banner列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<BannerManagementDto>> GetListAsync(BannerManagementQueryDto input)
        {
            return await _bannerManagementAppService.GetListAsync(input);
        }

        /// <summary>
        /// 根据主键ID获取Banner信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<BannerManagementDto> GetAsync(Guid id)
        {
            return await _bannerManagementAppService.GetAsync(id);
        }

        /// <summary>
        /// 添加一条Banner
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BannerManagementDto> CreateAsync(CreateBannerManagementInput input)
        {
            return await _bannerManagementAppService.CreateAsync(input);
        }


        /// <summary>
        /// 根据主键ID修改Banner
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<BannerManagementDto> UpdateAsync(Guid id, UpdateBannerManagementInput input)
        {
            return await _bannerManagementAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 根据主键ID修改Banner启用状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPut("{id}/enable")]
        public async Task<BannerManagementDto> UpdateEnableStatusAsync(Guid id, UpdateBannerManagementEnableStatusInput input)
        {
            return await _bannerManagementAppService.UpdateEnableStatusAsync(id, input);
        }

        /// <summary>
        /// 根据主键ID删除Banner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _bannerManagementAppService.DeleteAsync(id);
        }

    }
}
