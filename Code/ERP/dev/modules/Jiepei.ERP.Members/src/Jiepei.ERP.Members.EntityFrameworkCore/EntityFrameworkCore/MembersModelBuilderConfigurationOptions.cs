using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Jiepei.ERP.Members.EntityFrameworkCore
{
    public class MembersModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public MembersModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}