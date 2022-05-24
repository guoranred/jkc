using Jiepei.ERP.Suppliers.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Jiepei.ERP.Suppliers.Pages
{
    public abstract class SuppliersPageModel : AbpPageModel
    {
        protected SuppliersPageModel()
        {
            LocalizationResourceType = typeof(SuppliersResource);
        }
    }
}