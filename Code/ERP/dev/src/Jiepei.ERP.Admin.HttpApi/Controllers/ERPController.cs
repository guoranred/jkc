using Jiepei.ERP.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Jiepei.ERP.Admin.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class ERPController : AbpController
    {
        protected ERPController()
        {
            LocalizationResource = typeof(ERPResource);
        }
    }
}