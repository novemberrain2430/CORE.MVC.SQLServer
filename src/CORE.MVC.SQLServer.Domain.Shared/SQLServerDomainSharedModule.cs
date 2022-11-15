using CORE.MVC.SQLServer.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.LanguageManagement;
using Volo.Abp.LeptonTheme.Management;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TextTemplateManagement;
using Volo.Abp.VirtualFileSystem;
using Volo.Saas;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.Commercial.SuiteTemplates;
using Volo.CmsKit;
using Volo.Blogging;
using Volo.Docs;
using Volo.Payment;
using Volo.FileManagement;
using Volo.Forms;
using Volo.Chat;
using Sample;

namespace CORE.MVC.SQLServer
{
    [DependsOn(
        typeof(AbpAuditLoggingDomainSharedModule),
        typeof(AbpBackgroundJobsDomainSharedModule),
        typeof(AbpFeatureManagementDomainSharedModule),
        typeof(AbpIdentityProDomainSharedModule),
        typeof(AbpIdentityServerDomainSharedModule),
        typeof(AbpPermissionManagementDomainSharedModule),
        typeof(AbpSettingManagementDomainSharedModule),
        typeof(LanguageManagementDomainSharedModule),
        typeof(SaasDomainSharedModule),
        typeof(TextTemplateManagementDomainSharedModule),
        typeof(VoloAbpCommercialSuiteTemplatesModule),
        typeof(LeptonThemeManagementDomainSharedModule),
        typeof(CmsKitProDomainSharedModule),
        typeof(BlobStoringDatabaseDomainSharedModule)
        )]
    [DependsOn(typeof(BloggingDomainSharedModule))]
    [DependsOn(typeof(DocsDomainSharedModule))]
    [DependsOn(typeof(AbpPaymentDomainSharedModule))]
    [DependsOn(typeof(FileManagementDomainSharedModule))]
    [DependsOn(typeof(FormsDomainSharedModule))]
    [DependsOn(typeof(ChatDomainSharedModule))]
    [DependsOn(typeof(SampleDomainSharedModule))]
    public class SQLServerDomainSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            SQLServerGlobalFeatureConfigurator.Configure();
            SQLServerModuleExtensionConfigurator.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<SQLServerDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<SQLServerResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/SQLServer");

                options.DefaultResourceType = typeof(SQLServerResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("SQLServer", typeof(SQLServerResource));
            });
        }
    }
}
