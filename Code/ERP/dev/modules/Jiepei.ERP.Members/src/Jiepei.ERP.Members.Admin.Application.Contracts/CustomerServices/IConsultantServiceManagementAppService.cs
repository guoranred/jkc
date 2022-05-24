using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Members.Admin.CustomerServices
{
    public interface IConsultantServiceManagementAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询客服信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<CustomerServiceDto>> GetListAsync(CustomerServiceQueryDto input);

        /// <summary>
        /// 分页获取客服以及服务的用户数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<CustomerServiceStatusDto>> GetCustomerServiceWithMemberCountListAsync(
            CustomerServiceQueryDto input);

        /// <summary>
        /// 根据主键ID获取客服信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CustomerServiceDto> GetAsync(Guid id);

        /// <summary>
        /// 新增客服
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        Task<CustomerServiceDto> CreateAsync(CreateCustomerServiceInput inputDto);

        /// <summary>
        /// 根据主键ID修改客服信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        Task<CustomerServiceDto> UpdateAsync(Guid id, UpdateCustomerServiceDto inputDto);

        /// <summary>
        /// 根据主键ID删除客服信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 设置是否在线
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task SetIsOnline(Guid id, SetIsOnlineInput input);
        Task<PagedResultDto<CustomerServiceStatusDto>> GetSalesmanWithMemberCountListAsync(CustomerServiceQueryDto input);
    }
}
