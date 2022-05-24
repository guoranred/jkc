using Jiepei.ERP.Orders.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Jiepei.ERP.Orders
{
    public abstract class OrdersController : AbpController
    {
        protected OrdersController()
        {
            LocalizationResource = typeof(OrdersResource);
        }
    }
}
