using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Account;
using Volo.Abp.AuditLogging;
using Volo.Abp.AutoMapper;
using Volo.Abp.Emailing;
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
        typeof(SQLServerDomainModule),
        typeof(SQLServerApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpSettingManagementApplicationModule),
        typeof(SaasHostApplicationModule),
        typeof(AbpAuditLoggingApplicationModule),
        typeof(AbpIdentityServerApplicationModule),
        typeof(AbpAccountPublicApplicationModule),
        typeof(AbpAccountAdminApplicationModule),
        typeof(LanguageManagementApplicationModule),
        typeof(LeptonThemeManagementApplicationModule),
        typeof(CmsKitProApplicationModule),
        typeof(TextTemplateManagementApplicationModule)
        )]
    [DependsOn(typeof(BloggingApplicationModule))]
    [DependsOn(typeof(BloggingAdminApplicationModule))]
    [DependsOn(typeof(DocsApplicationModule))]
    [DependsOn(typeof(AbpAccountSharedApplicationModule))]
    [DependsOn(typeof(AbpPaymentApplicationModule))]
    [DependsOn(typeof(AbpPaymentAdminApplicationModule))]
    [DependsOn(typeof(SaasTenantApplicationModule))]
    [DependsOn(typeof(FileManagementApplicationModule))]
    [DependsOn(typeof(FormsApplicationModule))]
    [DependsOn(typeof(ChatApplicationModule))]
    [DependsOn(typeof(SampleApplicationModule))]
    public class SQLServerApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<SQLServerApplicationModule>();
            });
        }
    }
}
