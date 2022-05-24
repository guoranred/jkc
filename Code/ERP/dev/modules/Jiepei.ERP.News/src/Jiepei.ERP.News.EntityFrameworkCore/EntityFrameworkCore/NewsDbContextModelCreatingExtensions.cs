using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Jiepei.ERP.News.EntityFrameworkCore
{
    public static class NewsDbContextModelCreatingExtensions
    {
        public static void ConfigureNews(
            this ModelBuilder builder,
            Action<NewsModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new NewsModelBuilderConfigurationOptions(
                NewsDbProperties.DbTablePrefix,
                NewsDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            #region Banners

            builder.Entity<Banner>(b =>
            {
                b.ToTable("Banner");

                b.HasComment("Banner表");

                b.ConfigureByConvention();
                #region 注释
                b.Property(x => x.ChannelId).HasComment("渠道");
                b.Property(x => x.Title).HasComment("图片标题信息").HasColumnType("varchar(80)");
                b.Property(x => x.ImageUrl).HasComment("图片链接地址").HasColumnType("varchar(500)");
                b.Property(x => x.RedirectUrl).HasComment("地址").HasColumnType("varchar(500)");
                b.Property(x => x.StartDate).HasComment("生效时间");
                b.Property(x => x.EndDate).HasComment("失效时间");
                b.Property(x => x.IsEnable).HasComment("是否启用").HasDefaultValue(false); ;
                b.Property(x => x.SortOrder).HasComment("显示顺序");
                b.Property(x => x.Remark).HasComment("备注信息").HasColumnType("varchar(500)");

                #endregion

                #region 索引
                b.HasIndex(x => x.ChannelId, "Index_Banner_ChannelId");
                #endregion
            });
            #endregion

            #region ArticleList

            builder.Entity<ArticleList>(b =>
            {
                b.ToTable("ArticleList");

                b.HasComment("文章表");

                b.ConfigureByConvention();
                #region 注释
                b.Property(x => x.ChannelId).HasComment("渠道");
                b.Property(x => x.Title).HasComment("文章标题").HasColumnType("varchar(300)");
                b.Property(x => x.ColumnType).HasComment("所属栏目").HasColumnType("varchar(300)");
                b.Property(x => x.ImgPath).HasComment("图片路径").HasColumnType("varchar(300)");
                b.Property(x => x.Introduce).HasComment("简介").HasColumnType("varchar(200)");
                b.Property(x => x.Author).HasComment("作者").HasColumnType("varchar(100)");
                b.Property(x => x.ReleaseTime).HasComment("发布时间");
                b.Property(x => x.ReleaseStatus).HasComment("发布状态").HasDefaultValue(false);
                b.Property(x => x.IsDel).HasComment("是否删除").HasDefaultValue(false);
                b.Property(x => x.Content).HasComment("内容").HasColumnType("varchar(max)");
                b.Property(x => x.Sort).HasComment("排序");
                b.Property(x => x.IsSetTop).HasComment("是否置顶");
                b.Property(x => x.Tag).HasComment("标签").HasColumnType("varchar(50)");
                b.Property(x => x.ColumnTypeId).HasComment("所属栏目id");

                #endregion

                #region 索引
                b.HasIndex(x => x.ChannelId, "Index_ArticleList_ChannelId");
                #endregion
            });

            builder.Entity<ColumnType>(b =>
            {
                b.ToTable("ColumnType");

                b.HasComment("栏目表");

                b.ConfigureByConvention();
                #region 注释
                b.Property(x => x.ChannelId).HasComment("渠道");
                b.Property(x => x.Pid).HasComment("父id");
                b.Property(x => x.Code).HasComment("栏目编号").HasColumnType("varchar(100)");
                b.Property(x => x.Name).HasComment("栏目名称").HasColumnType("varchar(300)");
                b.Property(x => x.Alias).HasComment("栏目别名").HasColumnType("varchar(300)");
                b.Property(x => x.Type).HasComment("项目类型").HasColumnType("varchar(max)");
                b.Property(x => x.LogoImage).HasComment("logo图片").HasColumnType("varchar(300)");
                b.Property(x => x.Remark).HasComment("备注").HasColumnType("varchar(1000)");
                b.Property(x => x.ColumnOwnership).HasComment("栏目所属FAQ|新闻|公告");
                #endregion

                #region 索引
                #endregion
            });
            #endregion
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
        }
    }
}