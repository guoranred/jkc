using Jiepei.ERP.Members.Enums;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.Members.CustomerServices
{
    public class CustomerServiceManager : DomainService, ICustomerServiceManager
    {
        private readonly ICustomerServiceRepository _customerServiceRepository;

        public CustomerServiceManager(ICustomerServiceRepository customerServiceRepository)
        {
            _customerServiceRepository = customerServiceRepository;
        }

        public async Task<CustomerService> CreateAsync(string name, string phone, string avatarImage, string weChatImage, string qq, string email, CustomerServiceTypeEnum type, string promoCode, string businessLine, bool isOnline,string jobNumber)
        {
            var id = GuidGenerator.Create();
            await ValidatePromoCodeAsync(promoCode);

            return new CustomerService(id, name, phone, avatarImage, weChatImage, qq, email, type, promoCode, businessLine, isOnline, jobNumber);
        }

        /// <summary>
        /// 校验推广码唯一
        /// </summary>
        /// <param name="promoCode"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task ValidatePromoCodeAsync(string promoCode, Guid? id = null)
        {
            var other = await _customerServiceRepository.GetByPromoCode(promoCode);
            if (other != null && other.Id != id)
            {
                throw new UserFriendlyException("当前推广码已存在");
            }
        }
    }
}
