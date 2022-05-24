using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Members.MemberAddresses
{
    public interface IMemberAddressAppService : IApplicationService
    {
        #region 更新
        /// <summary>
        /// 创建收货地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<MemberAddressDto> CreateAsync(CreateMemberAddressInput input);

        /// <summary>
        /// 编辑收货地址
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<MemberAddressDto> UpdateAsync(Guid id, UpdateMemberAddressInput input);

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid Id);

        #endregion
        #region 查询
        /// <summary>
        /// 收货地址详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<MemberAddressDto> GetAsync(Guid Id);

        /// <summary>
        /// 获取所有收货地址
        /// </summary>
        /// <returns></returns>
        Task<PagedResultDto<MemberAddressDto>> GetPageAsync(MemberAddressQueryDto input);

        Task<List<AdministrativeDivisionDto>> GetAdministrativeDivisions(Guid? id);

        Task<int> GetRelationNmId(string code);
        #endregion
    }
}
