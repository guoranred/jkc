using Jiepei.ERP.News.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Jiepei.ERP.News.Admin
{
    public abstract class NewsAdminController : AbpController
    {
        protected NewsAdminController()
        {
            LocalizationResource = typeof(NewsResource);
        }
    }
}
