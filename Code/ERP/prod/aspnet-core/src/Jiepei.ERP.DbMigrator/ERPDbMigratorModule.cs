using Jiepei.ERP.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(ERPEntityFrameworkCoreDbMigrationsModule),
        typeof(ERPApplicationContractsModule),
        typeof(ERPAdminApplicationContractsModule)
    )]
    public class ERPDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = false;
            });
        }
    }
}