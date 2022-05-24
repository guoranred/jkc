using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Jiepei.ERP
{
    [Dependency(ReplaceServices = true)]
    public class ERPBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "ERP";
    }
}
