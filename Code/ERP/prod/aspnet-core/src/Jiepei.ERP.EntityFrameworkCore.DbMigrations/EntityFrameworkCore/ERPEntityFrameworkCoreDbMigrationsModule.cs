using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.EntityFrameworkCore
{
    [DependsOn(
        typeof(ERPEntityFrameworkCoreModule)
    )]
    public class ERPEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ERPMigrationsDbContext>();
        }
    }
}