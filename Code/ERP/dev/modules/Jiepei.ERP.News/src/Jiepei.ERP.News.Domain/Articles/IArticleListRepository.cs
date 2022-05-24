using System;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.News
{
    public interface IArticleListRepository : IRepository<ArticleList, Guid>
    {
    }
}
