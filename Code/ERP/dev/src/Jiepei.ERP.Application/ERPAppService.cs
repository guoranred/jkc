using Jiepei.ERP.Localization;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP
{
    /* Inherit your application services from this class.
     */
    public abstract class ERPAppService : ApplicationService
    {
        protected ERPAppService()
        {
            LocalizationResource = typeof(ERPResource);
        }
    }
}
