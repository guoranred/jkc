using Jiepei.ERP.Suppliers.Suppliers;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace Jiepei.ERP.Suppliers
{
    public class SupplierDataSeedContributor
        : IDataSeedContributor, ITransientDependency
    {
        private readonly ISupplierRepository _supplierRepository;
        public SupplierDataSeedContributor(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        [UnitOfWork(true)]
        public async Task SeedAsync(DataSeedContext context)
        {
            var supplier = await _supplierRepository.FindAsync(x => x.Code.Equals("000"));
        }
    }
}
