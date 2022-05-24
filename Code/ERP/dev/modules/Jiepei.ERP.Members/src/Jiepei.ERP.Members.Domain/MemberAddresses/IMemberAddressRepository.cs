using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Members.MemberAddresses
{
    public interface IMemberAddressRepository : IRepository<MemberAddress, Guid>
    {
        Task<List<MemberAddress>> GetListAsync(
            Guid uid,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default);

        Task<long> CountAsync(Guid uid);
    }
}
