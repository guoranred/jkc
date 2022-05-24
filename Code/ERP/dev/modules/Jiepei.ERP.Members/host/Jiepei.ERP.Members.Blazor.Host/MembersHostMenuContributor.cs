using System.Threading.Tasks;
using Jiepei.ERP.Members.Localization;
using Volo.Abp.UI.Navigation;

namespace Jiepei.ERP.Members.Blazor.Host
{
    public class MembersHostMenuContributor : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if(context.Menu.DisplayName != StandardMenus.Main)
            {
                return Task.CompletedTask;
            }

            var l = context.GetLocalizer<MembersResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    "Members.Home",
                    l["Menu:Home"],
                    "/",
                    icon: "fas fa-home"
                )
            );

            return Task.CompletedTask;
        }
    }
}
