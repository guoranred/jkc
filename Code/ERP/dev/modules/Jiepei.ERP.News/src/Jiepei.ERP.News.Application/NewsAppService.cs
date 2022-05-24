using Jiepei.ERP.News.Localization;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.News
{
    public abstract class NewsAppService : ApplicationService
    {
        protected NewsAppService()
        {
            LocalizationResource = typeof(NewsResource);
            ObjectMapperContext = typeof(NewsApplicationModule);
        }
    }
}
