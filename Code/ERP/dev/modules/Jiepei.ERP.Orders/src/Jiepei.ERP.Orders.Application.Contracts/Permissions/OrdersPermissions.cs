using Volo.Abp.Reflection;

namespace Jiepei.ERP.Orders.Permissions
{
    public class OrdersPermissions
    {
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(OrdersPermissions));
        }
    }
}