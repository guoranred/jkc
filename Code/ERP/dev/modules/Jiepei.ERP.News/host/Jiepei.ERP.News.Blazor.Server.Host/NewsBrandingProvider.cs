using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Jiepei.ERP.News.Blazor.Server.Host
{
    [Dependency(ReplaceServices = true)]
    public class NewsBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "News";
    }
}
