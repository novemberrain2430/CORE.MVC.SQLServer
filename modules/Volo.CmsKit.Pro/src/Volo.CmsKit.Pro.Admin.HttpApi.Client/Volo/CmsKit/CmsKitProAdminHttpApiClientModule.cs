using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.CmsKit.Admin;

namespace Volo.CmsKit
{
    [DependsOn(
        typeof(CmsKitProAdminApplicationContractsModule),
        typeof(CmsKitAdminHttpApiClientModule)
        )]
    public class CmsKitProAdminHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(CmsKitProAdminApplicationContractsModule).Assembly,
                CmsKitAdminRemoteServiceConsts.RemoteServiceName
            );
        }
    }
}
