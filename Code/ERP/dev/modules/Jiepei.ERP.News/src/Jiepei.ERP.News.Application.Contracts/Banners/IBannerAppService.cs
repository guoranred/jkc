using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.News
{
    public interface IBannerAppService : IApplicationService
    {
        Task<List<GetBannerInfoOutputDto>> GetAsync();
    }
}
