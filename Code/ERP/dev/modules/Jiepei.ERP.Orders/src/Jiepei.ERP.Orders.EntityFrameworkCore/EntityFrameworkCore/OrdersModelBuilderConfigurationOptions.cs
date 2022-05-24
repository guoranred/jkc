using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Jiepei.ERP.Orders.EntityFrameworkCore
{
    public class OrdersModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public OrdersModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}