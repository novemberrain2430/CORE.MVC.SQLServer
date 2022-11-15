using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Volo.CmsKit.EntityFrameworkCore
{
    public class CmsKitProModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public CmsKitProModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}