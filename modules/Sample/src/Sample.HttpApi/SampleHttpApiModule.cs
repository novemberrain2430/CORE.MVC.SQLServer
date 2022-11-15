using Localization.Resources.AbpUi;
using Sample.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Sample
{
    [DependsOn(
        typeof(SampleApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class SampleHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(SampleHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<SampleResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
