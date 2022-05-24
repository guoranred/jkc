using Jiepei.ERP.Suppliers.Localization;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Suppliers
{
    public abstract class SuppliersAppService : ApplicationService
    {
        protected SuppliersAppService()
        {
            LocalizationResource = typeof(SuppliersResource);
            ObjectMapperContext = typeof(SuppliersApplicationModule);
        }
    }
}
