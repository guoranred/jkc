using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Jiepei.ERP.Pages
{
    public class IndexModel : AbpPageModel
    {
        public ActionResult OnGet()
        {
            if (!CurrentUser.IsAuthenticated)
            {
                return Redirect("~/Account/Login");
            }
            else
            {
                return Page();
            }
        }
    }
}
