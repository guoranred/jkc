using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Members.Admin.CustomerServices
{
    /// <summary>
    /// 
    /// </summary>
    [RemoteService(Name = MembersAdminRemoteServiceConsts.RemoteServiceName)]
    [Area("memberesAdmin")]
    [Route("api/admin/consultant")]
    public class ConsultantManagementController : MembersAdminController
    {
        private readonly IConsultantServiceManagementAppService _consultantManagementAppService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="consultantManagementAppService"></param>
        public ConsultantManagementController(IConsultantServiceManagementAppService consultantManagementAppService)
        {
            _consultantManagementAppService = consultantManagementAppService;
        }

        /// <summary>
        /// 新增客服
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<CustomerServiceDto> CreateAsync(CreateCustomerServiceInput inputDto)
        {
            return await _consultantManagementAppService.CreateAsync(inputDto);
        }


        /// <summary>
        /// 根据主键ID删除客服信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _consultantManagementAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 根据主键ID获取客服信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CustomerServiceDto> GetAsync(Guid id)
        {
            return await _consultantManagementAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页获取客服以及服务的用户数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/with-member")]
        public async Task<PagedResultDto<CustomerServiceStatusDto>> GetCustomerServiceWithMemberCountListAsync(CustomerServiceQueryDto input)
        {
            return await _consultantManagementAppService.GetCustomerServiceWithMemberCountListAsync(input);
        }

        /// <summary>
        /// 分页获取业务员以及服务的用户数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/salesman-with-member")]
        public async Task<PagedResultDto<CustomerServiceStatusDto>> GetSalesmanWithMemberCountListAsync(CustomerServiceQueryDto input)
        {
            return await _consultantManagementAppService.GetSalesmanWithMemberCountListAsync(input);
        }

        /// <summary>
        /// 分页查询客服信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<CustomerServiceDto>> GetListAsync(CustomerServiceQueryDto input)
        {
            return await _consultantManagementAppService.GetListAsync(input);
        }

        /// <summary>
        /// 根据主键ID修改客服信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<CustomerServiceDto> UpdateAsync(Guid id, UpdateCustomerServiceDto inputDto)
        {
            return await _consultantManagementAppService.UpdateAsync(id, inputDto);
        }

        /// <summary>
        /// 根据主键ID修改客服信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPut("{id}/isonline")]
        public async Task SetIsOnline(Guid id, SetIsOnlineInput input)
        {
            await _consultantManagementAppService.SetIsOnline(id, input);
        }

    }
}
