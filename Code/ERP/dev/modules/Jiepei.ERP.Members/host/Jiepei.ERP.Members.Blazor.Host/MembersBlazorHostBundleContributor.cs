using Volo.Abp.Bundling;

namespace Jiepei.ERP.Members.Blazor.Host
{
    public class MembersBlazorHostBundleContributor : IBundleContributor
    {
        public void AddScripts(BundleContext context)
        {

        }

        public void AddStyles(BundleContext context)
        {
            context.Add("main.css", true);
        }
    }
}
