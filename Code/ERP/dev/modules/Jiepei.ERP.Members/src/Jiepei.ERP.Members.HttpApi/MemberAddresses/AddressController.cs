using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Members.MemberAddresses
{
    [RemoteService(Name = MembersRemoteServiceConsts.RemoteServiceName)]
    [Area("addresses")]
    [Route("api/addresses")]
    public class AddressController : MembersController
    {
        private readonly IMemberAddressAppService _memberAddressAppService;

        public AddressController(IMemberAddressAppService memberAddressAppService)
        {
            _memberAddressAppService = memberAddressAppService;
        }

        /// <summary>
        /// 创建收货地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MemberAddressDto> CreateAsync(CreateMemberAddressInput input)
        {
            return await _memberAddressAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _memberAddressAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<MemberAddressDto> GetAsync(Guid id)
        {
            return await _memberAddressAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<MemberAddressDto>> GetPageAsync(MemberAddressQueryDto input)
        {
            return await _memberAddressAppService.GetPageAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<MemberAddressDto> UpdateAsync(Guid id, UpdateMemberAddressInput input)
        {
            return await _memberAddressAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 行政区划
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("administrative-divisions/{id?}")]
        public async Task<List<AdministrativeDivisionDto>> GetAdministrativeDivisions(Guid? id = null)
        {
            return await _memberAddressAppService.GetAdministrativeDivisions(id);
        }
    }
}
