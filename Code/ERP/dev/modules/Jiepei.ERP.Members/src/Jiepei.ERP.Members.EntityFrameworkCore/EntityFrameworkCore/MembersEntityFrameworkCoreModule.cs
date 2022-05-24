using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Jiepei.ERP.Members.EntityFrameworkCore
{
    [DependsOn(
        typeof(MembersDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class MembersEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<MembersDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddDefaultRepositories(includeAllEntities: true);
            });
        }
    }
}