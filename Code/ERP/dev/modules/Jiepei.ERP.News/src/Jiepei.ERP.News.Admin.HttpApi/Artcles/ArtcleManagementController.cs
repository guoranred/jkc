using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.News.Admin
{
    /// <summary>
    /// 文章列表
    /// </summary>
    [RemoteService]
    [Route("api/artcle")]
    public class ArtcleManagementController : NewsAdminController
    {
        private readonly IArtcleManagementAppServoice _artcleManagementAppServoice;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="artcleManagementAppServoice"></param>
        public ArtcleManagementController(IArtcleManagementAppServoice artcleManagementAppServoice)
        {
            _artcleManagementAppServoice = artcleManagementAppServoice;
        }
        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActicleListDto> CreateAsync(CreateActicleListInput input)
        {
            return await _artcleManagementAppServoice.CreateAsync(input);
        }

        /// <summary>
        /// 获取所有文章
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<ActicleListDto>> GetListAsync(ActicleListQueryInput input)
        {

            return await _artcleManagementAppServoice.GetListAsync(input);
        }

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActicleListDto> UpdateAsync(Guid id, UpdateActicleListInput input)
        {
            return await _artcleManagementAppServoice.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid Id)
        {
            await _artcleManagementAppServoice.DeleteAsync(Id);
        }

        /// <summary>
        /// 查看详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActicleListDto> GetDetailAsync(Guid Id)
        {
            return await _artcleManagementAppServoice.GetDetailAsync(Id);
        }

    }
}
