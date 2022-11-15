using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Volo.Forms
{
    [DependsOn(
        typeof(AbpHttpClientModule),
        typeof(FormsApplicationContractsModule)
        )]
    public class FormsHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(FormsApplicationContractsModule).Assembly,
                FormsRemoteServiceConsts.RemoteServiceName
            );
        }
    }
}
