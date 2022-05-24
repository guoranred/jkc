using Jiepei.InTradeConsumer.Domain.CncOrders;
using Jiepei.InTradeConsumer.Domain.InjectionOrders;
using Jiepei.InTradeConsumer.Domain.OrderMains;
using Jiepei.InTradeConsumer.MoldOrders;
using Jiepei.InTradeConsumer.OrderDetails;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.InTradeConsumer.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class InTradeConsumerDbContext : AbpDbContext<InTradeConsumerDbContext>
    {
        public DbSet<MoldOrder> MoldOrders { get; set; }
        public DbSet<OrderMain> OrderMains { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<InjectionOrder> InjectionOrders { get; set; }
        public DbSet<CncOrder> CncOrder { get; set; }
        public InTradeConsumerDbContext(DbContextOptions<InTradeConsumerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MoldOrder>(
                b =>
                {
                    b.ToTable(nameof(MoldOrder));
                    b.Property(t => t.ProductAfterAmt).HasColumnType("decimal");
                    b.Property(t => t.ProductAmt).HasColumnType("decimal");
                    b.Property(t => t.Weight).HasColumnType("decimal");
                });
            builder.Entity<OrderMain>(b =>
            {
                b.ToTable(nameof(OrderMain));
            });
            builder.Entity<OrderDetail>(b => { b.ToTable(nameof(OrderDetail)); });
            builder.Entity<InjectionOrder>(
              b =>
              {
                  b.ToTable(nameof(InjectionOrder));
                  b.Property(t => t.ProductAfterAmt).HasColumnType("decimal");
                  b.Property(t => t.ProductAmt).HasColumnType("decimal");
                  b.Property(t => t.Weight).HasColumnType("decimal");
              });
        }
    }
}
