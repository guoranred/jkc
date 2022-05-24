using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Members.Admin
{
    /// <summary>
    /// 会员信息管理
    /// </summary>
    public interface IMemberManagementAppService : IApplicationService
    {
        /// <summary>
        /// 分页获取会员信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<MemberInfomationDto>> GetListAsync(MemberInfomationQueryInput input);

        /// <summary>
        /// 根据主键Guid获取会员详细信息
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        Task<MemberInformationDetailDto> GetAsync(Guid id);

        /// <summary>
        /// 根据主键Guid修改会员信息
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="inputDto">修改内容</param>
        /// <returns></returns>
        Task<MemberInfomationDto> UpdateAsync(Guid id, UpdateMemberInformationDto inputDto);

        /// <summary>
        /// 根据主键Guid修改客服
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        Task<MemberInfomationDto> UpdateCustomerServicetAsync(Guid id, UpdateMemberInfomationConsultantDto inputDto);

        /// <summary>
        /// 根据主键Guid变更会员账号状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        Task<MemberInfomationDto> UpdateAccountStatusAsync(Guid id, UpdateMemberInformationAccountStatusDto inputDto);

        /// <summary>
        /// 根据主键Guid修改会员账户的密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        Task<MemberInfomationDto> UpdatePasswordAsync(Guid id, UpdateMemberInformationPasswordDto inputDto);

        /// <summary>
        /// 根据主键Guid删除会员信息
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        Task<List<MemberInfomationDto>> GetListByIdsAsync(Guid[] ids);
        Task<MemberInfomationDto> UpdateSalesmanAsync(Guid id, UpdateMemberInfomationSalesmanDto inputDto);
    }
}
