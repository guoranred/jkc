using Jiepei.ERP.News.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.News.Artcles
{
    public class ArticleListRepository : EfCoreRepository<INewsDbContext, ArticleList, Guid>, IArticleListRepository
    {
        public ArticleListRepository(IDbContextProvider<INewsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
