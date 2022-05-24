using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.News.EntityFrameworkCore
{
    [ConnectionStringName(NewsDbProperties.ConnectionStringName)]
    public interface INewsDbContext : IEfCoreDbContext
    {
        DbSet<Banner> Banners { get; set; }
        DbSet<ArticleList> ArticleLists { get; set; }
        DbSet<ColumnType> ColumnTypes { get; set; }
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}