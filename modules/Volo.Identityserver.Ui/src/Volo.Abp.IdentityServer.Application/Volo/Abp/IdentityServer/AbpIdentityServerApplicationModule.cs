using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Volo.Abp.IdentityServer
{
    [DependsOn(
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpIdentityServerApplicationContractsModule),
        typeof(AbpDddApplicationModule)
        )]
    public class AbpIdentityServerApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbpIdentityServerApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AbpIdentityServerApplicationAutoMapperProfile>(validate: true); //TODO: Enable validation after fixing mappings in AbpIdentityServerApplicationAutoMapperProfile
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            LicenseChecker.Check<AbpIdentityServerApplicationModule>(context);
        }
    }
}
