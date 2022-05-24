using Jiepei.ERP.StatisticalDatas;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Jiepei.ERP.EntityFrameworkCore
{
    public static class ERPDbContextModelCreatingExtensions
    {
        public static void ConfigureERP(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(ERPConsts.DbTablePrefix + "YourEntities", ERPConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});

            builder.Entity<StatisticalData>(b =>
            {
                b.ToTable(nameof(StatisticalData));
                b.ConfigureByConvention();
                b.Property(t => t.PaymentAmount).HasPrecision(18, 3);
            });
        }
    }
}