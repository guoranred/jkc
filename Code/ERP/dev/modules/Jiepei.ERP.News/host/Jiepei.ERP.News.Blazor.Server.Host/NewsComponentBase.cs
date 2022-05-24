using Jiepei.ERP.News.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Jiepei.ERP.News.Blazor.Server.Host
{
    public abstract class NewsComponentBase : AbpComponentBase
    {
        protected NewsComponentBase()
        {
            LocalizationResource = typeof(NewsResource);
        }
    }
}
