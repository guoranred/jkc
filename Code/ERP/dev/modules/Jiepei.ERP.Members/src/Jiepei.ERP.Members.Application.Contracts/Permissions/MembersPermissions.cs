using Volo.Abp.Reflection;

namespace Jiepei.ERP.Members.Permissions
{
    public class MembersPermissions
    {
        public const string GroupName = "Members";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(MembersPermissions));
        }
    }
}