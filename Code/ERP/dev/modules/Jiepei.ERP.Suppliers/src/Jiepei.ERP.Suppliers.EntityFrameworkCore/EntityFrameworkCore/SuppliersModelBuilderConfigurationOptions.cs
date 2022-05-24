using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Jiepei.ERP.Suppliers.EntityFrameworkCore
{
    public class SuppliersModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public SuppliersModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}