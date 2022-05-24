using Jiepei.ERP.Suppliers.Suppliers;
using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Jiepei.ERP.Suppliers.EntityFrameworkCore
{
    public static class SuppliersDbContextModelCreatingExtensions
    {
        public static void ConfigureSuppliers(
            this ModelBuilder builder,
            Action<SuppliersModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new SuppliersModelBuilderConfigurationOptions(
                SuppliersDbProperties.DbTablePrefix,
                SuppliersDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */

            builder.Entity<Supplier>(b =>
            {
                b.HasComment("供应商表");
                b.ConfigureByConvention();
                b.Property(x => x.Code).HasMaxLength(50).HasComment("编号");
                b.Property(x => x.Name).HasMaxLength(50).HasComment("名称");
                b.Property(x => x.SysKey).HasMaxLength(100).HasComment("秘钥");
                b.Property(x => x.SysCallback).HasMaxLength(300).HasComment("回调地址");
                b.Property(x => x.IsEnable).HasDefaultValue(0).HasComment("是否启用");
                b.Property(x => x.Note).HasMaxLength(500).HasComment("备注");
            });
        }
    }
}