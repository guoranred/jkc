using Jiepei.ERP.Members.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jiepei.ERP.Members.MemberInfomation
{
    public class MemberInformationRepository : EfCoreRepository<MembersDbContext, MemberInformation, Guid>, IMemberInformationRepository
    {
        public MemberInformationRepository(IDbContextProvider<MembersDbContext> dbContextProvider) : base(dbContextProvider) { }

        public async Task<List<MemberInformation>> GetPagedListAsync(string name,
                                                                            string code,
                                                                            string phone,
                                                                            Guid? customerServiceId,
                                                                            Guid? salesmanId,
                                                                            string sorting = null,
                                                                            int maxResultCount = int.MaxValue,
                                                                            int skipCount = 0,
                                                                            bool includeDetails = true,
                                                                            CancellationToken cancellationToken = default)
        {
            var query = includeDetails ? await WithDetailsAsync() : await GetDbSetAsync();
            return await query
                .AsNoTracking()
                .WhereIf(!name.IsNullOrWhiteSpace(), t => t.Name.Contains(name))
                .WhereIf(!code.IsNullOrWhiteSpace(), t => t.Code.Contains(code))
                .WhereIf(!phone.IsNullOrWhiteSpace(), t => t.PhoneNumber.Contains(phone))
                .WhereIf(customerServiceId.HasValue, t => t.CustomerServiceId == customerServiceId)
                .WhereIf(salesmanId.HasValue, t => t.SalesmanId == salesmanId)
                .OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(MemberInformation.CreationTime) + " descending" : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> CountAsync(string name,
                                           string code,
                                           string phone,
                                           Guid? customerServiceId,
                                           Guid? salesmanId,
                                           CancellationToken cancellationToken = default)
        {
            var query = (await GetDbSetAsync()).AsNoTracking();
            query = CreateFilteredQueryAsync(query, name, code, phone, customerServiceId, salesmanId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        private IQueryable<MemberInformation> CreateFilteredQueryAsync(IQueryable<MemberInformation> query,
                                                                       string name,
                                                                       string code,
                                                                       string phone,
                                                                       Guid? customerServiceId,
                                                                       Guid? salesmanId)
        {
            return query
                .WhereIf(!name.IsNullOrWhiteSpace(), t => t.Name.Contains(name))
                .WhereIf(!phone.IsNullOrWhiteSpace(), t => t.PhoneNumber.Contains(phone))
                .WhereIf(!code.IsNullOrWhiteSpace(), t => t.Code.Contains(code))
                .WhereIf(customerServiceId.HasValue, t => t.CustomerServiceId == customerServiceId)
                .WhereIf(salesmanId.HasValue, t => t.SalesmanId == salesmanId);
        }

    }
}
