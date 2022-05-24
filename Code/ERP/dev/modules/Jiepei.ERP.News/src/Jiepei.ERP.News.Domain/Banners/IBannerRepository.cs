using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.News
{
    public interface IBannerRepository : IRepository<Banner, Guid>
    {
        Task<Tuple<IEnumerable<Banner>, long>> GetBannerPageList(
       Expression<Func<Banner, bool>> expression = null,
       Func<IQueryable<Banner>, IOrderedQueryable<Banner>> orderBy = null,
       int maxResultCount = 10,
       int skipCount = 0,
       bool includeDetails = false,
       CancellationToken cancellationToken = default(CancellationToken));
    }
}
