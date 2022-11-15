using Microsoft.AspNetCore.Identity;
using Volo.Abp.Modularity;

namespace Volo.Abp.Identity
{
    [DependsOn(
        typeof(AbpIdentityDomainModule),
        typeof(AbpIdentityProDomainSharedModule)
    )]
    public class AbpIdentityProDomainModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IdentityBuilder>(builder =>
            {
                builder.AddUserValidator<MaxUserCountValidator>();
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            LicenseChecker.Check<AbpIdentityProDomainModule>(context);
        }
    }
}
