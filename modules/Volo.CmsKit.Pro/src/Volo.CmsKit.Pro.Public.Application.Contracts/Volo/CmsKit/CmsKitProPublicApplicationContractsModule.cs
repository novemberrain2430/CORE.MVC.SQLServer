using Volo.Abp.Modularity;
using Volo.CmsKit.Public;

namespace Volo.CmsKit
{
    [DependsOn(
        typeof(CmsKitProDomainSharedModule),
        typeof(CmsKitPublicApplicationContractsModule)
        )]
    public class CmsKitProPublicApplicationContractsModule : AbpModule
    {

    }
}
