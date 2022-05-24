using Jiepei.ERP.Members.CustomerServices.Dtos;
using Jiepei.ERP.Members.Enums;
using Jiepei.ERP.Members.Members.Dtos;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Members
{
    public interface IMemberAppService : IApplicationService
    {
        /// <summary>
        /// 获取tocken
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<LoginOutputDto> GetAsync(string phoneNumber, string password, Guid channelId);

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GetMemberDot> GetByIdAsync(Guid id);

        Task<MemberBaseInfoOutputDto> GetAsync(Guid id);
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(RegisterInput input);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        Task<bool> UpdateAsync(ChangePasswordInput input);

        /// <summary>
        /// 修改会员基础信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> UpdateMemBerAsync(UpdateMemberInfoInput input);

        /// <summary>
        /// 获取当前登录人基础信息
        /// </summary>
        /// <returns></returns>
        Task<MemberBaseInfoOutputDto> GetMemBerAsync();


        /// <summary>找回密码=>获取验证码
        /// </summary>
        /// <param name="PhoneNumber"></param>
        /// <returns></returns>
        Task<bool> GetValidateCodeAsync(VerificationCodeTypeEnum Type, string PhoneNumber, Guid channelId);

        /// <summary>
        /// <summary>找回密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> RetrievePasswordAsync(RetrievePasswordInput input);

        /// <summary>
        /// 获取用户客服
        /// </summary>
        /// <returns></returns>
        Task<GetCurrentUserCustomerServiceOutputDto> GetCurrentUserCustomerService();
        Task<CustomerServiceDto> GetCustomerServiceAsync(Guid id);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateByAppAsync(RegisterAppInput input);
        Task<CodeGenerationDto> GetCodeGeneration();
    }
}
