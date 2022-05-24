using Jiepei.ERP.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Jiepei.ERP.Permissions
{
    public class ERPPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ERPResource>(name);
        }
    }
}