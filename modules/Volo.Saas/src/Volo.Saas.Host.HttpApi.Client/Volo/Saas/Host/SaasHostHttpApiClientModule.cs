using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Payment;

namespace Volo.Saas.Host
{
    [DependsOn(
        typeof(SaasHostApplicationContractsModule),
        typeof(AbpHttpClientModule),
        typeof(AbpFeatureManagementHttpApiClientModule),
        typeof(AbpPaymentHttpApiClientModule)
        )]
    public class SaasHostHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(SaasHostApplicationContractsModule).Assembly,
                SaasHostRemoteServiceConsts.RemoteServiceName
            );
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
        }
    }
}
