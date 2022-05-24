using Jiepei.ERP.Orders.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Jiepei.ERP.Orders.Permissions
{
    public class OrdersPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var orderGroup = context.AddGroup(OrdersPermissions.GroupName, L("Permission:Orders"));
            var ordersPermission = orderGroup.AddPermission(OrdersPermissions.Orders.Default, L("Permission:OrderManagement"));
            ordersPermission.AddChild(OrdersPermissions.Orders.Search, L("Permission:Search"));
            ordersPermission.AddChild(OrdersPermissions.Orders.Detail, L("Permission:Detail"));
            ordersPermission.AddChild(OrdersPermissions.Orders.EditNote, L("Permission:OrderEditNote"));
            ordersPermission.AddChild(OrdersPermissions.Orders.Create, L("Permission:Create"));
            ordersPermission.AddChild(OrdersPermissions.Orders.Cancel, L("Permission:Cancel"));
            ordersPermission.AddChild(OrdersPermissions.Orders.Check, L("Permission:Check"));
            ordersPermission.AddChild(OrdersPermissions.Orders.Complete, L("Permission:Complete"));
            ordersPermission.AddChild(OrdersPermissions.Orders.Deliver, L("Permission:Deliver"));
            ordersPermission.AddChild(OrdersPermissions.Orders.Manufacture, L("Permission:Manufacture"));
            ordersPermission.AddChild(OrdersPermissions.Orders.Offer, L("Permission:Offer"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<OrdersResource>(name);
        }
    }
}