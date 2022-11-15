using Volo.Abp.Modularity;
using Volo.CmsKit.Admin;

namespace Volo.CmsKit
{
    [DependsOn(
        typeof(CmsKitProDomainSharedModule),
        typeof(CmsKitAdminApplicationContractsModule)
        )]
    public class CmsKitProAdminApplicationContractsModule : AbpModule
    {

    }
}
