using Jiepei.ERP.Members.Enums;
using System;
using System.Threading.Tasks;

namespace Jiepei.ERP.Members.CustomerServices
{
    public interface ICustomerServiceManager
    {
        Task<CustomerService> CreateAsync(string name, string phone, string avatarImage, string weChatImage, string qq, string email, CustomerServiceTypeEnum type, string promoCode, string businessLine, bool isOnline, string jobNumber);

        Task ValidatePromoCodeAsync(string promoCode, Guid? id = null);
    }
}
