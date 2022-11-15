using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Payment;

namespace Volo.Saas.Host
{
    [DependsOn(
        typeof(SaasDomainModule),
        typeof(SaasHostApplicationContractsModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpPaymentApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class SaasHostApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<SaasHostApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<SaasHostApplicationAutoMapperProfile>(validate: true);
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            LicenseChecker.Check<SaasHostApplicationModule>(context);
        }
    }
}
