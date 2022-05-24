using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Members.Admin
{
    [RemoteService(Name = MembersAdminRemoteServiceConsts.RemoteServiceName)]
    [Area("memberesAdmin")]
    [Route("api/admin/members")]
    public class MamberManagementController : MembersAdminController
    {
        private readonly IMemberManagementAppService _memberManagementAppService;

        public MamberManagementController(IMemberManagementAppService memberManagementAppService)
        {
            _memberManagementAppService = memberManagementAppService;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _memberManagementAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<MemberInformationDetailDto> GetAsync(Guid id)
        {
            return await _memberManagementAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<MemberInfomationDto>> GetListAsync(MemberInfomationQueryInput input)
        {
            return await _memberManagementAppService.GetListAsync(input);
        }

        /// <summary>
        /// 变更会员状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPut("{id}/change-status")]
        public async Task<MemberInfomationDto> UpdateAccountStatusAsync(Guid id, UpdateMemberInformationAccountStatusDto inputDto)
        {
            return await _memberManagementAppService.UpdateAccountStatusAsync(id, inputDto);
        }

        /// <summary>
        /// 修改会员信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPut("{id}/change-userinfo")]
        public async Task<MemberInfomationDto> UpdateAsync(Guid id, UpdateMemberInformationDto inputDto)
        {
            return await _memberManagementAppService.UpdateAsync(id, inputDto);
        }

        /// <summary>
        /// 修改客服
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPut("{id}/customer-service")]
        public async Task<MemberInfomationDto> UpdateCustomerServiceAsync(Guid id, UpdateMemberInfomationConsultantDto inputDto)
        {
            return await _memberManagementAppService.UpdateCustomerServicetAsync(id, inputDto);
        }

        /// <summary>
        /// 修改业务员
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPut("{id}/salesman")]
        public async Task<MemberInfomationDto> UpdateSalesmanAsync(Guid id, UpdateMemberInfomationSalesmanDto inputDto)
        {
            return await _memberManagementAppService.UpdateSalesmanAsync(id, inputDto);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPut("{id}/change-password")]
        public async Task<MemberInfomationDto> UpdatePasswordAsync(Guid id, UpdateMemberInformationPasswordDto inputDto)
        {
            return await _memberManagementAppService.UpdatePasswordAsync(id, inputDto);
        }
    }
}
