using JetBrains.Annotations;

namespace Volo.Abp.Identity.EntityFrameworkCore
{
    public class IdentityProModelBuilderConfigurationOptions : IdentityModelBuilderConfigurationOptions
    {
        public IdentityProModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}
