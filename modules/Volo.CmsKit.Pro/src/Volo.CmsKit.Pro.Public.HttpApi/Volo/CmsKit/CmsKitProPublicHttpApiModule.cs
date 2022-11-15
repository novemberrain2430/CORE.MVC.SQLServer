using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.CmsKit.Localization;
using Volo.CmsKit.Public;

namespace Volo.CmsKit
{
    [DependsOn(
        typeof(CmsKitProPublicApplicationContractsModule),
        typeof(CmsKitPublicHttpApiModule)
        )]
    public class CmsKitProPublicHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(CmsKitProPublicHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<CmsKitResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            LicenseChecker.Check<CmsKitProPublicHttpApiModule>(context);
        }
    }
}
