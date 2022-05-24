using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Jiepei.ERP.News.EntityFrameworkCore
{
    public class NewsModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public NewsModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}