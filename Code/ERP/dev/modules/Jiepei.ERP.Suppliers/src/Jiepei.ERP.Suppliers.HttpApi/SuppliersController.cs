using Jiepei.ERP.Suppliers.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Jiepei.ERP.Suppliers
{
    public abstract class SuppliersController : AbpController
    {
        protected SuppliersController()
        {
            LocalizationResource = typeof(SuppliersResource);
        }
    }
}
