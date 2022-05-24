using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Jiepei.ERP.Members.AdministrativeDivisions
{
    public interface IAdministrativeDivisionManager : IDomainService
    {
        Task<AdministrativeDivision> CreateAsync(AdministrativeDivision input);
        Task<AdministrativeDivision> UpdateAsync(AdministrativeDivision input);
    }
}
