using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Suppliers.Common
{
    public class SupplierCommonModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClient();
        }
    }
}
