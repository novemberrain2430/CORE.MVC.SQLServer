using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.CmsKit.Pro.Admin.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.CmsKit.Admin.Web;
using Volo.CmsKit.Localization;
using Volo.CmsKit.Permissions;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;
using Volo.Abp.Localization;
using Volo.Abp.SettingManagement.Web;
using Volo.Abp.SettingManagement.Web.Pages.SettingManagement;
using Volo.CmsKit.Pro.Admin.Web.Settings;

namespace Volo.CmsKit.Pro.Admin.Web
{
    [DependsOn(
        typeof(CmsKitAdminWebModule),
        typeof(CmsKitProAdminHttpApiModule),
        typeof(AbpSettingManagementWebModule)
        )]
    public class CmsKitProAdminWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(CmsKitResource), typeof(CmsKitProAdminWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(CmsKitProAdminWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new CmsKitProAdminMenuContributor());
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<CmsKitProAdminWebModule>();
            });

            Configure<SettingManagementPageOptions>(options =>
            {
                options.Contributors.Add(new CmsKitProSettingManagementPageContributor());
            });
            
            Configure<AbpBundlingOptions>(options =>
            {
                options.ScriptBundles
                    .Configure(typeof(Volo.Abp.SettingManagement.Web.Pages.SettingManagement.IndexModel).FullName,
                        configuration =>
                        {
                            configuration.AddFiles("/Pages/CmsKit/Components/CmsKitProSettingGroup/Default.js");
                        });
            });

            context.Services.AddAutoMapperObjectMapper<CmsKitProAdminWebModule>();
            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<CmsKitProAdminWebModule>(validate: true); });

            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AuthorizeFolder("/CmsKit/Newsletters/", CmsKitProAdminPermissions.Newsletters.Default);
            });

            Configure<AbpPageToolbarOptions>(options =>
            {
                options.Configure<Volo.CmsKit.Pro.Admin.Web.Pages.CmsKit.Newsletters.IndexModel>(
                    toolbar =>
                    {
                        toolbar.AddButton(
                            LocalizableString.Create<CmsKitResource>("ExportCSV"),
                            icon: "download",
                            id: "ExportCsv"
                        );
                    }
                );
            });
        }
        
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            LicenseChecker.Check<CmsKitProAdminWebModule>(context);
        }
    }
}