using Jiepei.ERP.Orders.Localization;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Orders.Admin
{
    /// <summary>
    /// Admin --订单
    /// </summary>
    public abstract class OrdersAdminAppServiceBase : ApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        public OrdersAdminAppServiceBase()
        {
            ObjectMapperContext = typeof(OrdersAdminApplicationModule);
            LocalizationResource = typeof(OrdersResource);
        }
    }
}
