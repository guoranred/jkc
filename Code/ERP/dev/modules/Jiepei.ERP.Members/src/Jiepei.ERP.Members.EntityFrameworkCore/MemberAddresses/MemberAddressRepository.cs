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

namespace Jiepei.ERP.Members.MemberAddresses
{
    public class MemberAddressRepository : EfCoreRepository<IMembersDbContext, MemberAddress, Guid>, IMemberAddressRepository
    {
        public MemberAddressRepository(IDbContextProvider<IMembersDbContext> dbContextProvider) : base(dbContextProvider) { }


        public virtual async Task<List<MemberAddress>> GetListAsync(
            Guid uid,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {

            var queryable = await GetDbSetAsync();
            return await queryable
                .Where(t => t.MemberId == uid)
                .OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(MemberAddress.CreationTime) : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> CountAsync(Guid uid)
        {
            return await (await GetDbSetAsync()).LongCountAsync(t => t.MemberId == uid);
        }
    }
}
