using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Jiepei.ERP.Members
{
    [Dependency(ReplaceServices = true)]
    public class MembersBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Members";
    }
}
