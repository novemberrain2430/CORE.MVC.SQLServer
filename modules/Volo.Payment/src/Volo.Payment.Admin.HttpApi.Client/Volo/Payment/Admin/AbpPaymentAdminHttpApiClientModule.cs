using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Volo.Payment.Admin
{
    [DependsOn(
        typeof(AbpPaymentAdminApplicationContractsModule),
        typeof(AbpPaymentHttpApiClientModule)
        )]
    public class AbpPaymentAdminHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(AbpPaymentAdminApplicationContractsModule).Assembly,
                AbpPaymentAdminRemoteServiceConsts.RemoteServiceName
            );
        }
    }
}
