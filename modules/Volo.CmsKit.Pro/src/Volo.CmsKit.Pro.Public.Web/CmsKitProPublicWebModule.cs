using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.CmsKit.Pro.Public.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.CmsKit.Localization;
using Volo.CmsKit.Public.Web;

namespace Volo.CmsKit.Pro.Public.Web
{
    [DependsOn(
        typeof(CmsKitProPublicHttpApiModule),
        typeof(CmsKitPublicWebModule)
        )]
    public class CmsKitProPublicWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(CmsKitResource), typeof(CmsKitProPublicWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(CmsKitProPublicWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new CmsKitProPublicMenuContributor());
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<CmsKitProPublicWebModule>("Volo.CmsKit.Pro.Public.Web");
            });

            context.Services.AddAutoMapperObjectMapper<CmsKitProPublicWebModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<CmsKitProPublicWebModule>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AddPageRoute("/Public/Newsletters/EmailPreferences", "cms/newsletter/email-preferences");
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            LicenseChecker.Check<CmsKitProPublicWebModule>(context);
        }
    }
}
