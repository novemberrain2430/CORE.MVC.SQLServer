using Volo.Abp;
using Volo.Abp.Emailing;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Volo.Abp.TextTemplating;
using Volo.Abp.VirtualFileSystem;

namespace Volo.CmsKit
{
    [DependsOn(
        typeof(CmsKitProDomainSharedModule),
        typeof(CmsKitDomainModule),
        typeof(AbpEmailingModule),
        typeof(AbpTextTemplatingModule),
        typeof(AbpSettingManagementDomainModule)
    )]
    public class CmsKitProDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<CmsKitProDomainModule>();
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            LicenseChecker.Check<CmsKitProDomainModule>(context);
        }
    }
}
