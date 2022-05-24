using System.Threading.Tasks;

namespace Jiepei.ERP.Data
{
    public interface IERPDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}