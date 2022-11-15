using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.AuditLogging;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.LanguageManagement;
using Volo.Abp.LeptonTheme.Management;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TextTemplateManagement;
using Volo.Abp.SettingManagement;
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
        typeof(SQLServerApplicationContractsModule),
        typeof(AbpIdentityHttpApiClientModule),
        typeof(AbpPermissionManagementHttpApiClientModule),
        typeof(AbpFeatureManagementHttpApiClientModule),
        typeof(AbpSettingManagementHttpApiClientModule),
        typeof(SaasHostHttpApiClientModule),
        typeof(AbpAuditLoggingHttpApiClientModule),
        typeof(AbpIdentityServerHttpApiClientModule),
        typeof(AbpAccountAdminHttpApiClientModule),
        typeof(AbpAccountPublicHttpApiClientModule),
        typeof(LanguageManagementHttpApiClientModule),
        typeof(LeptonThemeManagementHttpApiClientModule),
        typeof(CmsKitProHttpApiClientModule),
        typeof(TextTemplateManagementHttpApiClientModule)
    )]
    [DependsOn(typeof(BloggingHttpApiClientModule))]
    [DependsOn(typeof(BloggingAdminHttpApiClientModule))]
    [DependsOn(typeof(DocsHttpApiClientModule))]
    [DependsOn(typeof(AbpPaymentHttpApiClientModule))]
    [DependsOn(typeof(AbpPaymentAdminHttpApiClientModule))]
    [DependsOn(typeof(FileManagementHttpApiClientModule))]
    [DependsOn(typeof(CmsKitProAdminHttpApiClientModule))]
    [DependsOn(typeof(FormsHttpApiClientModule))]
    [DependsOn(typeof(ChatHttpApiClientModule))]
    //[DependsOn(typeof(SampleHttpApiClientModule))]
    public class SQLServerHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(SQLServerApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
