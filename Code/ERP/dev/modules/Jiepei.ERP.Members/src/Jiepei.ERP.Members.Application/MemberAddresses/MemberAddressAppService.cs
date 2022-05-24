using Jiepei.ERP.Members.AdministrativeDivisions;
using Jiepei.ERP.Members.Enums;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Members.MemberAddresses
{
    public class MemberAddressAppService : MembersAppService, IMemberAddressAppService
    {
        private readonly IMemberAddressManager _memberAddressManager;
        private readonly IMemberAddressRepository _memberAddresses;
        private readonly IAdministrativeDivisionRepository _administrativeDivisions;

        public MemberAddressAppService(
            IMemberAddressManager memberAddressManager,
            IMemberAddressRepository memberAddresses,
            IAdministrativeDivisionRepository administrativeDivisions)
        {
            _memberAddressManager = memberAddressManager;
            _memberAddresses = memberAddresses;
            _administrativeDivisions = administrativeDivisions;
        }
        /// <summary>
        /// 创建收货地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<MemberAddressDto> CreateAsync(CreateMemberAddressInput input)
        {
            var entity = new MemberAddress(GuidGenerator.Create(),
                                           CurrentUser.Id.Value,
                                           input.Recipient,
                                           input.CompanyName,
                                           input.PhoneNumber,
                                           input.ProvinceCode,
                                           input.ProvinceName,
                                           input.CityCode,
                                           input.CityName,
                                           input.CountyCode,
                                           input.CountyName,
                                           input.DetailAddress,
                                           input.IsDefault);
            var result = await _memberAddressManager.CreateAsync(entity);
            return ObjectMapper.Map<MemberAddress, MemberAddressDto>(result);
        }

        /// <summary>
        /// 编辑收货地址
        /// </summary>
        [Authorize]
        public async Task<MemberAddressDto> UpdateAsync(Guid id, UpdateMemberAddressInput input)
        {
            var entity = await _memberAddresses.GetAsync(t => t.Id == id);
            entity.SetRegion(input.Recipient,
                             input.CompanyName,
                             input.PhoneNumber,
                             input.ProvinceCode,
                             input.ProvinceName,
                             input.CityCode,
                             input.CityName,
                             input.CountyCode,
                             input.CountyName,
                             input.DetailAddress,
                             input.IsDefault);
            var result = await _memberAddressManager.ChangeAddress(entity);
            return ObjectMapper.Map<MemberAddress, MemberAddressDto>(entity);
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid Id)
        {
            await _memberAddresses.DeleteAsync(Id);
        }

        /// <summary>
        /// 收货地址详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<MemberAddressDto> GetAsync(Guid Id)
        {
            var result = await _memberAddresses.GetAsync(t => t.Id == Id);
            return ObjectMapper.Map<MemberAddress, MemberAddressDto>(result);
        }

        /// <summary>
        /// 获取所有收货地址
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<PagedResultDto<MemberAddressDto>> GetPageAsync(MemberAddressQueryDto input)
        {
            var userid = CurrentUser.Id.Value;

            var result = await _memberAddresses.GetListAsync(userid, input.Sorting, input.MaxResultCount, input.SkipCount);
            var count = await _memberAddresses.CountAsync(userid);

            return new PagedResultDto<MemberAddressDto>(count, ObjectMapper.Map<List<MemberAddress>, List<MemberAddressDto>>(result));
        }

        /// <summary>
        /// 获取行政区划
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<AdministrativeDivisionDto>> GetAdministrativeDivisions(Guid? id)
        {
            var queryable = (await _administrativeDivisions.GetQueryableAsync())
                .WhereIf(id.HasValue, t => t.Pid == id)
                .WhereIf(id == null, t => t.Level == AdministrativeDivisionLevel.Province)
                .OrderBy(t => t.Sort)
                .Select(t => ObjectMapper.Map<AdministrativeDivision, AdministrativeDivisionDto>(t));
            return await AsyncExecuter.ToListAsync(queryable);
        }


        /// <summary>
        /// 获取行政区划
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<int> GetRelationNmId(string code)
        {
            var entiy = await _administrativeDivisions.FindAsync(t => t.Code == code);
            return entiy.RelationNmId;
        }
    }
}
