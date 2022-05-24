using Jiepei.ERP.Suppliers.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Jiepei.ERP.Suppliers.Permissions
{
    public class SuppliersPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(SuppliersPermissions.GroupName, L("Permission:Suppliers"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<SuppliersResource>(name);
        }
    }
}