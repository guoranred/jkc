using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Jiepei.ERP.Suppliers.Pages
{
    public class IndexModel : SuppliersPageModel
    {
        public void OnGet()
        {
            
        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}