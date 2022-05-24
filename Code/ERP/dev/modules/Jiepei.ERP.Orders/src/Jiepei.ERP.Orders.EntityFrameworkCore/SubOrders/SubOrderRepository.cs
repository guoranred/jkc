using Jiepei.ERP.Orders.EntityFrameworkCore;
using Jiepei.ERP.SubOrders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Orders.SubOrders
{
    public class SubOrderRepository : EfCoreRepository<IOrdersDbContext, SubOrder, Guid>, ISubOrderRepository
    {
        public SubOrderRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) : base(dbContextProvider) { }

        public async Task<List<SubOrder>> GetListAsync(EnumSubOrderStatus status = 0,
                                                       DateTime[] dateRange = null,
                                                       string orderNo = null,
                                                       string sorting = null,
                                                       int maxResultCount = int.MaxValue,
                                                       int skipCount = 0,
                                                       //string filter = null,
                                                       CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(status != 0, t => t.Status == status)
                .WhereIf(dateRange != null, t => t.CreationTime > dateRange[0] && t.CreationTime < dateRange[1])
                .WhereIf(!orderNo.IsNullOrWhiteSpace(), t => t.OrderNo.Contains(orderNo))
                .OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(SubOrder.OrderNo) : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> CountAsync(EnumSubOrderStatus status = 0,
                                           DateTime[] dateRange = null,
                                           string orderNo = null,
                                           string sorting = null,
                                           CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(status != 0, t => t.Status == status)
                .WhereIf(dateRange != null, t => t.CreationTime > dateRange[0] && t.CreationTime < dateRange[1])
                .WhereIf(!orderNo.IsNullOrWhiteSpace(), t => t.OrderNo.Contains(orderNo))
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }
    }
}
