using Jiepei.ERP.Members.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Jiepei.ERP.Members.Permissions
{
    public class MembersPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(MembersPermissions.GroupName, L("Permission:Members"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<MembersResource>(name);
        }
    }
}