using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Jiepei.ERP.Members.Pages
{
    public class IndexModel : MembersPageModel
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