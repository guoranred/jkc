using Microsoft.EntityFrameworkCore;
using Volo.Abp;

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

            //if (builder.IsHostDatabase())
            //{
            //    /* Tip: Configure mappings like that for the entities only available in the host side,
            //     * but should not be in the tenant databases. */
            //}
        }
    }
}