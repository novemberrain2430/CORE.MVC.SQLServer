using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.CmsKit.Public;

namespace Volo.CmsKit
{
    [DependsOn(
        typeof(CmsKitProPublicApplicationContractsModule),
        typeof(CmsKitPublicHttpApiClientModule)
        )]
    public class CmsKitProPublicHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(CmsKitProPublicApplicationContractsModule).Assembly,
                CmsKitProPublicRemoteServiceConsts.RemoteServiceName
            );
        }
    }
}
