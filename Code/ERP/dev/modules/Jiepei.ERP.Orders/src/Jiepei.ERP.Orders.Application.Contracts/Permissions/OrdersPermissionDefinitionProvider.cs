using Jiepei.ERP.Orders.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Jiepei.ERP.Orders.Permissions
{
    public class OrdersPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<OrdersResource>(name);
        }
    }
}