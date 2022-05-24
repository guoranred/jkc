using Jiepei.ERP.Orders.Localization;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Orders
{
    public abstract class OrdersAppService : ApplicationService
    {
        public IConfiguration _configuration { get; set; }
        protected OrdersAppService()
        {
            LocalizationResource = typeof(OrdersResource);
            ObjectMapperContext = typeof(OrdersApplicationModule);
        }
    }
}
