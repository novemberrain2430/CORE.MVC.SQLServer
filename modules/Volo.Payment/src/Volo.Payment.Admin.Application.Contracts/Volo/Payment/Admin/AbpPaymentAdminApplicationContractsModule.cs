using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Volo.Payment.Admin
{
    [DependsOn(
        typeof(AbpPaymentApplicationContractsModule),
        typeof(AbpAuthorizationModule))]
    public class AbpPaymentAdminApplicationContractsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpPaymentAdminApplicationContractsModule>();
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
        }
    }
}
