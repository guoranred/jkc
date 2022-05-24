using Volo.Abp.Reflection;

namespace Jiepei.ERP.Suppliers.Permissions
{
    public class SuppliersPermissions
    {
        public const string GroupName = "Suppliers";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(SuppliersPermissions));
        }
    }
}