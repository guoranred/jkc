using Volo.Abp.Bundling;

namespace Jiepei.ERP.Suppliers.Blazor.Host
{
    public class SuppliersBlazorHostBundleContributor : IBundleContributor
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
