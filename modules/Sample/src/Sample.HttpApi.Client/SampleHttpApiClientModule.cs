using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Sample
{
    [DependsOn(
        typeof(SampleApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class SampleHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Sample";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(SampleApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
