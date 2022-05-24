using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.Members.MemberAddresses
{
    public interface IMemberAddressManager : IDomainService
    {
        Task<MemberAddress> ChangeAddress(MemberAddress entity);
        Task<MemberAddress> CreateAsync(MemberAddress entity);
    }
}
