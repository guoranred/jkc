using System;
using System.Threading.Tasks;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Jiepei.ERP.Suppliers.Localization;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace Jiepei.ERP.Suppliers
{
    public class SuppliersWebHostMenuContributor : IMenuContributor
    {
        private readonly IConfiguration _configuration;

        public SuppliersWebHostMenuContributor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.User)
            {
                AddLogoutItemToMenu(context);
            }

            return Task.CompletedTask;
        }

        private void AddLogoutItemToMenu(MenuConfigurationContext context)
        {
            var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();
            var l = context.GetLocalizer<SuppliersResource>();

            if (currentUser.IsAuthenticated)
            {
                context.Menu.Items.Add(new ApplicationMenuItem(
                    "Account.Manage",
                    l["ManageYourProfile"],
                    $"{_configuration["AuthServer:Authority"].EnsureEndsWith('/')}Account/Manage",
                    icon: "fa fa-cog",
                    order: int.MaxValue - 1001,
                    null,
                    "_blank")
                    );


                context.Menu.Items.Add(new ApplicationMenuItem(
                    "Account.Logout",
                    l["Logout"],
                    "~/Account/Logout",
                    "fas fa-power-off",
                    order: int.MaxValue - 1000
                ));
            }
        }
    }
}
