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
    public class OrdersDbContext : AbpDbContext<OrdersDbContext>, IOrdersDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        #region Orders
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderCost> OrderCosts { get; set; }

        public DbSet<OrderExtra> OrderExtras { get; set; }
        public DbSet<OrderLog> OrderLogs { get; set; }
        public DbSet<OrderDelivery> OrderDeliveries { get; set; }

        #endregion

        #region SubOrders
        public DbSet<SubOrder> SubOrders { get; set; }
        public DbSet<SubOrderFlow> SubOrderFlows { get; set; }
        public DbSet<SubOrderCncItem> SubOrderCncItems { get; set; }
        public DbSet<SubOrderInjectionItem> SubOrderInjectionItems { get; set; }
        public DbSet<SubOrderMoldItem> SubOrderMoldItems { get; set; }
        public DbSet<SubOrderThreeDItem> SubOrderThreeDItems { get; set; }
        public DbSet<SubOrderSheetMetalItem> SubOrderSheetMetalItems { get; set; }


        #endregion

        #region Channels
        public DbSet<Channel> Channels { get; set; }
        #endregion

        #region Materials
        public DbSet<D3Material> D3Material { get; set; }
        public DbSet<MaterialPrice> MaterialPrice { get; set; }
        public DbSet<MaterialSupplier> MaterialSupplier { get; set; }

        #endregion

        #region PayLogs
        public DbSet<OrderPayLog> OrderPayLogs { get; set; }
        public DbSet<OrderPayDetailLog> OrderPayDetailLogs { get; set; }
        #endregion

        public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureOrders();
        }
    }
}