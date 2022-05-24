
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.News.Admin.HttpApi.ColumnTypes
{
    [RemoteService]
    [Route("api/columntype")]
    public class ColumnTypeManagementController : NewsAdminController
    {
        private readonly IColumnTypeManagementAppService _columnTypeManagementAppService;

        public ColumnTypeManagementController(IColumnTypeManagementAppService columnTypeManagementAppService)
        {
            _columnTypeManagementAppService = columnTypeManagementAppService;
        }
        /// <summary>
        /// 创建栏目类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ColumnTypeDto> CreateAsync(CreateColumnTypeInput input)
        {
            return await _columnTypeManagementAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除栏目类型
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task DeleteAsync(Guid Id)
        {
            await _columnTypeManagementAppService.DeleteAsync(Id);
        }

        /// <summary>
        /// 获取所有栏目类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<ColumnTypeDto>> GetListAsync(ColumnTypeQueryInput input)
        {
            return await _columnTypeManagementAppService.GetListAsync(input);

        }

        /// <summary>
        /// 修改栏目类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ColumnTypeDto> UpdateAsync(Guid id, UpdateColumnTypeInput input)
        {
            return await _columnTypeManagementAppService.UpdateAsync(id, input);
        }

    }
}
