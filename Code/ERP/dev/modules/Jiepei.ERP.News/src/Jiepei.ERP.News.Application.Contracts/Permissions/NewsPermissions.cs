using Volo.Abp.Reflection;

namespace Jiepei.ERP.News.Permissions
{
    public class NewsPermissions
    {
        public const string GroupName = "News";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(NewsPermissions));
        }
    }
}