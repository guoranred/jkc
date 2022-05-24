using System.Threading.Tasks;
using Jiepei.ERP.Suppliers.Localization;
using Volo.Abp.UI.Navigation;

namespace Jiepei.ERP.Suppliers.Blazor.Host
{
    public class SuppliersHostMenuContributor : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if(context.Menu.DisplayName != StandardMenus.Main)
            {
                return Task.CompletedTask;
            }

            var l = context.GetLocalizer<SuppliersResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    "Suppliers.Home",
                    l["Menu:Home"],
                    "/",
                    icon: "fas fa-home"
                )
            );

            return Task.CompletedTask;
        }
    }
}
