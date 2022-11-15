using Volo.Abp.Account;
using Volo.Abp.AuditLogging;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.LanguageManagement;
using Volo.Abp.LeptonTheme.Management;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TextTemplateManagement;
using Volo.Saas.Host;
using Volo.CmsKit;
using Volo.Blogging;
using Volo.Blogging.Admin;
using Volo.Docs;
using Volo.Payment;
using Volo.Payment.Admin;
using Volo.Saas.Tenant;
using Volo.FileManagement;
using Volo.Forms;
using Volo.Chat;
using Sample;

namespace CORE.MVC.SQLServer
{
    [DependsOn(
        typeof(SQLServerDomainSharedModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpSettingManagementApplicationContractsModule),
        typeof(SaasHostApplicationContractsModule),
        typeof(AbpAuditLoggingApplicationContractsModule),
        typeof(AbpIdentityServerApplicationContractsModule),
        typeof(AbpAccountPublicApplicationContractsModule),
        typeof(AbpAccountAdminApplicationContractsModule),
        typeof(LanguageManagementApplicationContractsModule),
        typeof(LeptonThemeManagementApplicationContractsModule),
        typeof(CmsKitProApplicationContractsModule),
        typeof(TextTemplateManagementApplicationContractsModule)
    )]
    [DependsOn(typeof(BloggingApplicationContractsModule))]
    [DependsOn(typeof(BloggingAdminApplicationContractsModule))]
    [DependsOn(typeof(DocsApplicationContractsModule))]
    [DependsOn(typeof(AbpAccountSharedApplicationContractsModule))]
    [DependsOn(typeof(AbpPaymentApplicationContractsModule))]
    [DependsOn(typeof(AbpPaymentAdminApplicationContractsModule))]
    [DependsOn(typeof(SaasTenantApplicationContractsModule))]
    [DependsOn(typeof(FileManagementApplicationContractsModule))]
    [DependsOn(typeof(FormsApplicationContractsModule))]
    [DependsOn(typeof(ChatApplicationContractsModule))]
    [DependsOn(typeof(SampleApplicationContractsModule))]
    public class SQLServerApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            SQLServerDtoExtensions.Configure();
        }
    }
}
