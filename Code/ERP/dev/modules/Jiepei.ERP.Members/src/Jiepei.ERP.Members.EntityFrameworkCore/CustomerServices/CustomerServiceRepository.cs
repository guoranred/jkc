using Jiepei.ERP.Members.EntityFrameworkCore;
using Jiepei.ERP.Members.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Members.CustomerServices
{
    public class CustomerServiceRepository : EfCoreRepository<IMembersDbContext, CustomerService, Guid>, ICustomerServiceRepository
    {
        public CustomerServiceRepository(IDbContextProvider<IMembersDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<CustomerService>> GetCustomerServiceWithMembersAsync(
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var token = GetCancellationToken(cancellationToken);
            var query = (await GetCustomerServiceWithMembersQueryAsync())
                //   .OrderBy(sorting.IsNullOrEmpty() ? "CreationTime desc" : sorting)
                .Include(t => t.Members)
                .PageBy(skipCount, maxResultCount);
            return await query.ToListAsync(token);
        }

        public async Task<long> GetCustomerServiceWithMemberCountAsync(CancellationToken cancellationToken = default)
        {
            var token = GetCancellationToken(cancellationToken);

            var query = await GetCustomerServiceWithMembersQueryAsync();

            return await query.LongCountAsync(token);
        }

        public async Task<List<SalesmanWithMemberQueryResultItem>> GetSalesmanWithMembersAsync(
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var token = GetCancellationToken(cancellationToken);
            var query = await GetListQueryAsync();

            //if (sorting != null)
            //{
            //    sorting = " c."+sorting;
            //}

            query = query//.OrderBy(sorting.IsNullOrEmpty() ? " Id desc" : sorting)
                .PageBy(skipCount, maxResultCount);

            return await query.ToListAsync(token);
        }

        public async Task<long> GetSalesmanWithMemberCountAsync(CancellationToken cancellationToken = default)
        {
            var token = GetCancellationToken(cancellationToken);
            var query = await GetListQueryAsync();
            return await query.LongCountAsync(token);
        }

        public async Task<CustomerService> GetByPromoCode(string promoCode, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.PromoCode == promoCode, GetCancellationToken(cancellationToken));
        }

        protected virtual async Task<IQueryable<SalesmanWithMemberQueryResultItem>> GetListQueryAsync()
        {
            var query = from c in (await GetDbSetAsync())
                        join m in (await GetDbContextAsync()).Set<MemberInformation>() on c.Id equals m.SalesmanId into t1
                        from t2 in t1.DefaultIfEmpty()
                        where c.Type == CustomerServiceTypeEnum.SalesMan
                        group t2 by new { c.Id, c.Name, c.AvatarImage, c.BusinessLine } into grouped
                        select new SalesmanWithMemberQueryResultItem
                        {
                            Id = grouped.Key.Id,
                            Name = grouped.Key.Name,
                            AvatarImage = grouped.Key.AvatarImage,
                            MemberCount = grouped.Count(),
                            BusinessLine = grouped.Key.BusinessLine
                        };
            return query;
        }

        protected virtual async Task<IQueryable<CustomerService>> GetCustomerServiceWithMembersQueryAsync()
        {
            return (await GetDbSetAsync())
                .AsNoTracking()
                .Where(t => t.Type == CustomerServiceTypeEnum.CustomerService);
        }
    }
}
