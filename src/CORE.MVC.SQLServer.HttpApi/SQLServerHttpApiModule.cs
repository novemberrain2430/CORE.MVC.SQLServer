using Localization.Resources.AbpUi;
using CORE.MVC.SQLServer.Localization;
using Volo.Abp.Account;
using Volo.Abp.AuditLogging;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.LanguageManagement;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Saas.Host;
using Volo.Abp.LeptonTheme;
using Volo.Abp.Localization;
using Volo.Abp.SettingManagement;
using Volo.Abp.TextTemplateManagement;
using Volo.CmsKit;
using Volo.Blogging;
using Volo.Blogging.Admin;
using Volo.Docs;
using Volo.Payment;
using Volo.Payment.Admin;
using Volo.Payment.Stripe;
using Volo.Saas.Tenant;
using Volo.FileManagement;
using Volo.Forms;
using Volo.Chat;
using Sample;

namespace CORE.MVC.SQLServer
{
    [DependsOn(
        typeof(SQLServerApplicationContractsModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpFeatureManagementHttpApiModule),
        typeof(AbpSettingManagementHttpApiModule),
        typeof(AbpAuditLoggingHttpApiModule),
        typeof(AbpIdentityServerHttpApiModule),
        typeof(AbpAccountAdminHttpApiModule),
        typeof(AbpAccountPublicHttpApiModule),
        typeof(LanguageManagementHttpApiModule),
        typeof(SaasHostHttpApiModule),
        typeof(LeptonThemeManagementHttpApiModule),
        typeof(CmsKitProHttpApiModule),
        typeof(TextTemplateManagementHttpApiModule)
        )]
    [DependsOn(typeof(BloggingHttpApiModule))]
    [DependsOn(typeof(BloggingAdminHttpApiModule))]
    [DependsOn(typeof(DocsHttpApiModule))]
    [DependsOn(typeof(AbpPaymentHttpApiModule))]
    [DependsOn(typeof(AbpPaymentAdminHttpApiModule))]
    [DependsOn(typeof(AbpPaymentStripeHttpApiModule))]
    [DependsOn(typeof(SaasTenantHttpApiModule))]
    [DependsOn(typeof(FileManagementHttpApiModule))]
    [DependsOn(typeof(FormsHttpApiModule))]
    [DependsOn(typeof(ChatHttpApiModule))]
    //[DependsOn(typeof(SampleHttpApiModule))]
    public class SQLServerHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureLocalization();
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<SQLServerResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );
            });
        }
    }
}
