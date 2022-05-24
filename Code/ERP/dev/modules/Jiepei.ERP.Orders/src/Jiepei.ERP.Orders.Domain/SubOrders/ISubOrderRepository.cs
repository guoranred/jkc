using Jiepei.ERP.SubOrders;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Orders.SubOrders
{
    public interface ISubOrderRepository : IRepository<SubOrder, Guid>
    {
        Task<List<SubOrder>> GetListAsync(EnumSubOrderStatus status = 0,
                                                       DateTime[] dateRange = null,
                                                       string orderNo = null,
                                                       string sorting = null,
                                                       int maxResultCount = int.MaxValue,
                                                       int skipCount = 0,
                                                       //string filter = null,
                                                       CancellationToken cancellationToken = default);
        Task<long> CountAsync(EnumSubOrderStatus status = 0,
                                                       DateTime[] dateRange = null,
                                                       string orderNo = null,
                                                       string sorting = null,
                                                       CancellationToken cancellationToken = default);
    }
}
