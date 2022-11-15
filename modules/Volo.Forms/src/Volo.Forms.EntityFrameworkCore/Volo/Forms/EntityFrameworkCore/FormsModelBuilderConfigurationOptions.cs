using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Volo.Forms.EntityFrameworkCore
{
    public class FormsModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public FormsModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}