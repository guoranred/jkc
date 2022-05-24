using Volo.Abp.Reflection;

namespace Jiepei.ERP.Orders.Permissions
{
    public class OrdersPermissions
    {
        #region Order
        public const string GroupName = "Orders";

        public static class Orders
        {
            public const string Default = GroupName + ".Orders";
            public const string Create = Default + ".Create";
            public const string Search = Default + ".Search";
            public const string Detail = Default + ".Detail";
            public const string EditNote = Default + ".EditNote";
            public const string Check = Default + ".Check";
            public const string Cancel = Default + ".Cancel";
            public const string Offer = Default + ".Offer";
            public const string Manufacture = Default + ".Manufacture";
            public const string Deliver = Default + ".Deliver";
            public const string Complete = Default + ".Complete";
        }


        public static class SubOrders
        {
            public const string Default = GroupName + ".SubOrders";
            public const string DesignChange = Default + ".DesignChange";
            public const string ChangeDeleiveryDate = Default + ".ChangeDeleiveryDate";
            public const string Search = Default + ".Search";
            public const string EditNote = Default + ".EditNote";
            public const string Detail = Default + ".Detail";
        }
        #endregion

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(OrdersPermissions));
        }
    }
}