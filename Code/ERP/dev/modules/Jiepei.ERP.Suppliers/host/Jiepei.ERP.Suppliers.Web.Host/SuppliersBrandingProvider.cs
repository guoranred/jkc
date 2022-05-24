using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Jiepei.ERP.Suppliers
{
    [Dependency(ReplaceServices = true)]
    public class SuppliersBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Suppliers";
    }
}
