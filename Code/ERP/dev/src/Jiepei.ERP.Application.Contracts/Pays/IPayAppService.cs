using Jiepei.ERP.Pays.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.Pays
{
    public interface IPayAppService : IApplicationService
    {
        Task<string> GetPayResultAsync(GetPayResultDto input);
        Task<CreatePayOutputDto> CreatePayAsync(CreatePayInputDto input);
    }
}
