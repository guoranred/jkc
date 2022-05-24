using EasyAbp.Abp.DataDictionary.EntityFrameworkCore;
using Jiepei.ERP.Members;
using Jiepei.ERP.Members.CustomerServices;
using Jiepei.ERP.Members.EntityFrameworkCore;
using Jiepei.ERP.News.EntityFrameworkCore;
using Jiepei.ERP.Orders.EntityFrameworkCore;
using Jiepei.ERP.Suppliers.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace Jiepei.ERP.EntityFrameworkCore
{
    [DependsOn(
        typeof(ERPDomainModule),
        typeof(AbpIdentityEntityFrameworkCoreModule),
        typeof(AbpIdentityServerEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule)
        )]
    [DependsOn(
        typeof(OrdersEntityFrameworkCoreModule),
        typeof(SuppliersEntityFrameworkCoreModule),
        typeof(AbpDataDictionaryEntityFrameworkCoreModule),
        typeof(MembersEntityFrameworkCoreModule),
        typeof(NewsEntityFrameworkCoreModule)
        )]
    public class ERPEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            ERPEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ERPDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
                options.AddRepository<CustomerService, CustomerServiceRepository>();
            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also ERPMigrationsDbContextFactory for EF Core tooling. */
                options.UseSqlServer();
            });
        }
    }
}
