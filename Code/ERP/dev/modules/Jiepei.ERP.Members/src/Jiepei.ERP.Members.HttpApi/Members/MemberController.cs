using Jiepei.ERP.Members.Enums;
using Jiepei.ERP.Members.Members.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace Jiepei.ERP.Members
{
    [RemoteService(Name = MembersRemoteServiceConsts.RemoteServiceName)]
    [Area("members")]
    [Route("api/members")]
    public class MemberController : MembersController
    {
        private readonly IMemberAppService _memberAppService;

        public MemberController(IMemberAppService memberAppService)
        {
            _memberAppService = memberAppService;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task CreateAsync(RegisterInput input)
        {
            await _memberAppService.CreateAsync(input);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("token")]
        public async Task<LoginOutputDto> GetAsync([FromBody] LoginInput input)
        {
            return await _memberAppService.GetAsync(input.PhoneNumber, input.Password, input.ChannelId);
        }

        /// <summary>
        /// 当前用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("my-profile")]
        public async Task<MemberBaseInfoOutputDto> GetMemberAsync()
        {
            return await _memberAppService.GetMemBerAsync();
        }

        [HttpGet("validate-code")]
        public async Task<bool> GetValidateCodeAsync(VerificationCodeTypeEnum type, string phoneNumber, Guid channelId)
        {
            return await _memberAppService.GetValidateCodeAsync(type, phoneNumber, channelId);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("retrieve-password")]
        public async Task<bool> RetrievePasswordAsync(RetrievePasswordInput input)
        {
            return await _memberAppService.RetrievePasswordAsync(input);
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("change-password")]
        public async Task<bool> UpdateAsync(ChangePasswordInput input)
        {
            return await _memberAppService.UpdateAsync(input);
        }

        /// <summary>
        /// 获取用户客服
        /// </summary>
        /// <returns></returns>
        [HttpGet("usercustomerservice")]
        public async Task<GetCurrentUserCustomerServiceOutputDto> GetCurrentUserCustomerService()
        {
            return await _memberAppService.GetCurrentUserCustomerService();
        }

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("my-profile")]
        public async Task<bool> UpdateMemberAsync([FromForm] UpdateMemberInfoInput input)
        {
            return await _memberAppService.UpdateMemBerAsync(input);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("register-app")]
        public async Task CreateByAppAsync(RegisterAppInput input)
        {
            await _memberAppService.CreateByAppAsync(input);

        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("codegeneration")]
        public async Task<CodeGenerationDto> GetCodeGeneration()
        {
            return   await _memberAppService.GetCodeGeneration();
        }
    }
}
