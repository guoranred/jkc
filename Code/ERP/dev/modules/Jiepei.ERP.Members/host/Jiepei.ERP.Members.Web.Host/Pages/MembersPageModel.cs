using Jiepei.ERP.Members.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Jiepei.ERP.Members.Pages
{
    public abstract class MembersPageModel : AbpPageModel
    {
        protected MembersPageModel()
        {
            LocalizationResourceType = typeof(MembersResource);
        }
    }
}