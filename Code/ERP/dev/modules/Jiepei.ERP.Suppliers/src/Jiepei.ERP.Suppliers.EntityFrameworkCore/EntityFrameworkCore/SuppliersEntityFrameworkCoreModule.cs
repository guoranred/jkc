using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Suppliers.EntityFrameworkCore
{
    [DependsOn(
        typeof(SuppliersDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class SuppliersEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<SuppliersDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            });
        }
    }
}