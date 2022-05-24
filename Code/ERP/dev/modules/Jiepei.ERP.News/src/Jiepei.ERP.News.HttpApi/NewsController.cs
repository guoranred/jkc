using Jiepei.ERP.News.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Jiepei.ERP.News
{
    public abstract class NewsController : AbpController
    {
        protected NewsController()
        {
            LocalizationResource = typeof(NewsResource);
        }
    }
}
