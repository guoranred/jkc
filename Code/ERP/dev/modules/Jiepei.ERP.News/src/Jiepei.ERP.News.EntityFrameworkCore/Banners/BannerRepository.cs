using Jiepei.ERP.News.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.News.Banners
{
    public class BannerRepository : EfCoreRepository<INewsDbContext, Banner, Guid>, IBannerRepository
    {
        public BannerRepository(IDbContextProvider<INewsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }


        public async Task<Tuple<IEnumerable<Banner>, long>> GetBannerPageList(
            Expression<Func<Banner, bool>> expression = null,
            Func<IQueryable<Banner>, IOrderedQueryable<Banner>> orderBy = null,
            int maxResultCount = 10,
            int skipCount = 0,
            bool includeDetails = false,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = (await base.GetQueryableAsync()).AsNoTracking();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            IEnumerable<Banner> data = await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken: cancellationToken);
            var count = await query.LongCountAsync(cancellationToken: cancellationToken);
            return new Tuple<IEnumerable<Banner>, long>(data, count);
        }
    }
}
