using Jiepei.ERP.Orders.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Jiepei.ERP.Orders.Admin.HttpApi
{
    /// <summary>
    /// 
    /// </summary>
    public class OrdersAdminController : AbpController
    {
        /// <summary>
        /// 
        /// </summary>
        public OrdersAdminController()
        {
            LocalizationResource = typeof(OrdersResource);
        }
    }
}
