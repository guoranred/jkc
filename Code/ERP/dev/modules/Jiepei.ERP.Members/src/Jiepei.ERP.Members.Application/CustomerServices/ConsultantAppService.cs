using Jiepei.ERP.Members.CustomerServices.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jiepei.ERP.Members.CustomerServices
{
    public class ConsultantAppService: MembersAppService, IConsultantServiceAppService
    {
        private readonly ICustomerServiceRepository _customerServiceRepository;


        public ConsultantAppService(ICustomerServiceRepository customerServiceRepository)
        {
            _customerServiceRepository = customerServiceRepository;
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
    }
}
