using EasyAbp.Abp.DataDictionary;
using Jiepei.ERP.Members.CustomerServices;
using Jiepei.ERP.Orders.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Jiepei.ERP.Members.Admin.CustomerServices
{
    public class ConsultantServiceManagementAppService : MembersAdminAppService, IConsultantServiceManagementAppService
    {
        private readonly ICustomerServiceRepository _customerServiceRepository;
        private readonly IChannelAppService _channelAppService;
        private readonly IDataDictionaryRepository _dataDictionaryRepository;

        public ConsultantServiceManagementAppService(ICustomerServiceRepository customerServiceRepository,
                                                     IChannelAppService channelAppService,
                                                     IDataDictionaryRepository dataDictionaryRepository)
        {
            _channelAppService = channelAppService;
            _customerServiceRepository = customerServiceRepository;
            _dataDictionaryRepository = dataDictionaryRepository;
        }

        /// <summary>
        /// 分页查询客服信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<CustomerServiceDto>> GetListAsync(CustomerServiceQueryDto input)
        {
            var sorting = input.Sorting.IsNullOrEmpty()
                ? nameof(CustomerService.CreationTime) + " desc"
                : input.Sorting;
            var customerServices = await _customerServiceRepository.GetPagedListAsync(input.SkipCount,
                                                                                      input.MaxResultCount,
                                                                                      sorting
                                                                                      );
            var result = ObjectMapper.Map<List<CustomerService>, List<CustomerServiceDto>>(customerServices);



            var totalCount = await _customerServiceRepository.GetCountAsync();
            return new PagedResultDto<CustomerServiceDto>(totalCount, result);
        }

        /// <summary>
        /// 分页获取客服以及服务的用户数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<CustomerServiceStatusDto>> GetCustomerServiceWithMemberCountListAsync(CustomerServiceQueryDto input)
        {
            var customerServices = await _customerServiceRepository.GetCustomerServiceWithMembersAsync(input.Sorting, input.MaxResultCount, input.SkipCount);
            await CombineBusinessLine(customerServices);
            var totalCount = await _customerServiceRepository.GetCustomerServiceWithMemberCountAsync();
            var consultantsDtos = ObjectMapper.Map<List<CustomerService>, List<CustomerServiceStatusDto>>(customerServices);
            return new PagedResultDto<CustomerServiceStatusDto>(totalCount, consultantsDtos);
        }

        /// <summary>
        /// 分页获取业务员以及服务的用户数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<CustomerServiceStatusDto>> GetSalesmanWithMemberCountListAsync(CustomerServiceQueryDto input)
        {
            var list = await _customerServiceRepository.GetSalesmanWithMembersAsync(input.Sorting, input.MaxResultCount, input.SkipCount);
            await CombineBusinessLine(list);
            var count = await _customerServiceRepository.GetSalesmanWithMemberCountAsync();
            var result = ObjectMapper.Map<List<SalesmanWithMemberQueryResultItem>, List<CustomerServiceStatusDto>>(list);
            return new PagedResultDto<CustomerServiceStatusDto>(count, result);
        }

        /// <summary>
        /// 根据主键ID获取客服信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CustomerServiceDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerService, CustomerServiceDto>(
                await _customerServiceRepository.GetAsync(id));
        }

        /// <summary>
        /// 新增客服
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<CustomerServiceDto> CreateAsync(CreateCustomerServiceInput input)
        {
            var entity = new CustomerService(GuidGenerator.Create(),
                                             input.Name,
                                             input.Phone,
                                             input.AvatarImage,
                                             input.WeChatImage,
                                             input.QQ,
                                             input.Email,
                                             input.Type,
                                             input.PromoCode,
                                             input.BusinessLine,
                                             input.IsOnline,
                                             input.JobNumber);

            return ObjectMapper.Map<CustomerService, CustomerServiceDto>(await _customerServiceRepository.InsertAsync(entity));
        }

        /// <summary>
        /// 根据主键ID修改客服信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public async Task<CustomerServiceDto> UpdateAsync(Guid id, UpdateCustomerServiceDto inputDto)
        {
            var customerService = await _customerServiceRepository.GetAsync(id);
            var model = ObjectMapper.Map(inputDto, customerService);
            return ObjectMapper.Map<CustomerService, CustomerServiceDto>(await _customerServiceRepository.UpdateAsync(model));
        }

        /// <summary>
        /// 根据主键ID删除客服信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            await _customerServiceRepository.DeleteAsync(id);
        }

        #region private method
        /// <summary>
        /// 设置客服是否在线
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task SetIsOnline(Guid id, SetIsOnlineInput input)
        {
            var entity = await _customerServiceRepository.GetAsync(id);
            entity.IsOnline = input.IsOnline;
            await _customerServiceRepository.UpdateAsync(entity);
        }

        private async Task CombineBusinessLine(List<CustomerService> customerServices)
        {
            var businessLines = await GetBusinessLinesAsync();
            foreach (var item in customerServices)
            {
                item.BusinessLine = businessLines
                    .Items.Where(t => t.Code == item.BusinessLine)
                    .Select(t => t.DisplayText)
                    .FirstOrDefault();
            }
        }

        private async Task CombineBusinessLine(List<SalesmanWithMemberQueryResultItem> inputs)
        {
            var businessLines = await GetBusinessLinesAsync();
            foreach (var item in inputs)
            {
                item.BusinessLine = businessLines
                    .Items.Where(t => t.Code == item.BusinessLine)
                    .Select(t => t.DisplayText)
                    .FirstOrDefault();
            }
        }

        private async Task<DataDictionary> GetBusinessLinesAsync()
        {
            return await _dataDictionaryRepository.GetAsync(t => t.Code == "BusinessLine", true);
        }
        #endregion
    }
}
