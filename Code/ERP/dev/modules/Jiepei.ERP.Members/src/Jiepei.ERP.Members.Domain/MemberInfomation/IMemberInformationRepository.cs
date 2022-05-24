using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Jiepei.ERP.Members
{
    public interface IMemberInformationRepository : IRepository<MemberInformation, Guid>
    {
        Task<List<MemberInformation>> GetPagedListAsync(string name,
                                                               string code,
                                                               string phone,
                                                               Guid? customerServiceId,
                                                               Guid? salesmanId,
                                                               string sorting = null,
                                                               int maxResultCount = int.MaxValue,
                                                               int skipCount = 0,
                                                               bool includeDetails = true,
                                                               CancellationToken cancellationToken = default);
        Task<long> CountAsync(string name, string code, string phone, Guid? customerServiceId,Guid? salesmanId, CancellationToken cancellationToken = default);
    }
}
