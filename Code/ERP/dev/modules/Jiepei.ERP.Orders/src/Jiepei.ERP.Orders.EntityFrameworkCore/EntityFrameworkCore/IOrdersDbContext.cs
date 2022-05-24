
using Jiepei.ERP.Orders.Channels;
using Jiepei.ERP.Orders.Materials;
using Jiepei.ERP.Orders.Orders;
using Jiepei.ERP.Orders.Pays;
using Jiepei.ERP.Orders.SubOrders;

using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.EntityFrameworkCore
{
    [ConnectionStringName(OrdersDbProperties.ConnectionStringName)]
    public interface IOrdersDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */

        #region Orders
        DbSet<Order> Orders { get; set; }

        DbSet<OrderCost> OrderCosts { get; set; }

        DbSet<OrderExtra> OrderExtras { get; set; }
        DbSet<OrderLog> OrderLogs { get; set; }
        DbSet<OrderDelivery> OrderDeliveries { get; set; }

        #endregion

        #region SubOrders
        DbSet<SubOrder> SubOrders { get; set; }
        DbSet<SubOrderFlow> SubOrderFlows { get; set; }
        DbSet<SubOrderCncItem> SubOrderCncItems { get; set; }
        DbSet<SubOrderInjectionItem> SubOrderInjectionItems { get; set; }
        DbSet<SubOrderMoldItem> SubOrderMoldItems { get; set; }
        DbSet<SubOrderThreeDItem> SubOrderThreeDItems { get; set; }

        DbSet<SubOrderSheetMetalItem> SubOrderSheetMetalItems { get; set; }

        #endregion

        #region Channels
        DbSet<Channel> Channels { get; set; }
        #endregion

        #region Materials
        DbSet<D3Material> D3Material { get; set; }
        DbSet<MaterialPrice> MaterialPrice { get; set; }
        DbSet<MaterialSupplier> MaterialSupplier { get; set; }

        #endregion

        #region PayLogs
        DbSet<OrderPayLog> OrderPayLogs { get; set; }
        DbSet<OrderPayDetailLog> OrderPayDetailLogs { get; set; }
        #endregion

    }
}