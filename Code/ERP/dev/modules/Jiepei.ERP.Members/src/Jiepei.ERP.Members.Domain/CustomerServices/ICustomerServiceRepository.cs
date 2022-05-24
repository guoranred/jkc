using Jiepei.ERP.Members.CustomerServices;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Members
{
    public interface ICustomerServiceRepository : IRepository<CustomerService, Guid>
    {
        Task<List<SalesmanWithMemberQueryResultItem>> GetSalesmanWithMembersAsync(
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default);

        Task<long> GetSalesmanWithMemberCountAsync(CancellationToken cancellationToken = default);
        Task<List<CustomerService>> GetCustomerServiceWithMembersAsync(
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default);
        Task<long> GetCustomerServiceWithMemberCountAsync(CancellationToken cancellationToken = default);
        Task<CustomerService> GetByPromoCode(string promoCode, CancellationToken cancellationToken = default);
    }
}
