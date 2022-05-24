using EasyAbp.Abp.DataDictionary.EntityFrameworkCore;
using Jiepei.ERP.Members.EntityFrameworkCore;
using Jiepei.ERP.News.EntityFrameworkCore;
using Jiepei.ERP.Orders.EntityFrameworkCore;
using Jiepei.ERP.Suppliers.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace Jiepei.ERP.EntityFrameworkCore
{
    /* This DbContext is only used for database migrations.
     * It is not used on runtime. See ERPDbContext for the runtime DbContext.
     * It is a unified model that includes configuration for
     * all used modules and your application.
     */
    public class ERPMigrationsDbContext : AbpDbContext<ERPMigrationsDbContext>
    {
        public ERPMigrationsDbContext(DbContextOptions<ERPMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentity();
            builder.ConfigureIdentityServer();
            builder.ConfigureFeatureManagement();
            //builder.ConfigureTenantManagement();

            /* Configure your own tables/entities inside the ConfigureERP method */

            builder.ConfigureERP();
            builder.ConfigureDataDictionary();
            builder.ConfigureOrders();
            builder.ConfigureMembers();
            builder.ConfigureSuppliers();
            builder.ConfigureNews();
            //builder.ConfigureCustomers();
            //builder.ConfigureFileManagement();
        }
    }
}