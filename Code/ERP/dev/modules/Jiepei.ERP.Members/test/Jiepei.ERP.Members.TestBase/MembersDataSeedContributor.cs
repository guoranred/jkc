using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Jiepei.ERP.Members
{
    public class MembersDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;

        public MembersDataSeedContributor(
            IGuidGenerator guidGenerator, ICurrentTenant currentTenant)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
        }

        public Task SeedAsync(DataSeedContext context)
        {
            /* Instead of returning the Task.CompletedTask, you can insert your test data
             * at this point!
             */

            using (_currentTenant.Change(context?.TenantId))
            {
                return Task.CompletedTask;
            }
        }
    }
}
