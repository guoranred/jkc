using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.News.EntityFrameworkCore
{
    [ConnectionStringName(NewsDbProperties.ConnectionStringName)]
    public class NewsDbContext : AbpDbContext<NewsDbContext>, INewsDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<Banner> Banners { get; set; }
        public DbSet<ArticleList> ArticleLists { get; set; }
        public DbSet<ColumnType> ColumnTypes { get; set; }

        public NewsDbContext(DbContextOptions<NewsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureNews();
        }
    }
}