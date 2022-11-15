using Volo.Abp;
using Volo.Abp.Modularity;

namespace Volo.Payment
{
    [DependsOn(
        typeof(AbpPaymentDomainSharedModule)
    )]
    public class AbpPaymentDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            LicenseChecker.Check<AbpPaymentDomainModule>(context);
        }
    }
}
