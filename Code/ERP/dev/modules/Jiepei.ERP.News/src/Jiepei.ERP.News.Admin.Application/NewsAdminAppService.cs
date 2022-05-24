using Jiepei.ERP.News.Localization;
using Volo.Abp.Application.Services;

namespace Jiepei.ERP.News
{
    public abstract class NewsAdminAppService : ApplicationService
    {
        protected NewsAdminAppService()
        {
            LocalizationResource = typeof(NewsResource);
            ObjectMapperContext = typeof(NewsAdminApplicationModule);
        }
    }
}
