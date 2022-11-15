using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using CORE.MVC.SQLServer.Localization;
using CORE.MVC.SQLServer.MultiTenancy;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Emailing;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.LanguageManagement;
using Volo.Abp.LeptonTheme.Management;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.IdentityServer;
using Volo.Abp.SettingManagement;
using Volo.Abp.TextTemplateManagement;
using Volo.Saas;
using Volo.Abp.BlobStoring.Database;
using Volo.CmsKit;
using Volo.CmsKit.Contact;
using Volo.CmsKit.Newsletters;
using Volo.Blogging;
using Volo.Docs;
using Volo.Payment;
using Volo.Payment.Payu;
using Volo.Payment.TwoCheckout;
using Volo.Payment.Iyzico;
using Volo.Payment.PayPal;
using Volo.Payment.Stripe;
using Volo.FileManagement;
using Volo.Forms;
using Volo.Chat;
using Volo.Abp.Sms.Twilio;
using Sample;

namespace CORE.MVC.SQLServer
{
    [DependsOn(
        typeof(SQLServerDomainSharedModule),
        typeof(AbpAuditLoggingDomainModule),
        typeof(AbpBackgroundJobsDomainModule),
        typeof(AbpFeatureManagementDomainModule),
        typeof(AbpIdentityProDomainModule),
        typeof(AbpPermissionManagementDomainIdentityModule),
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpPermissionManagementDomainIdentityServerModule),
        typeof(AbpSettingManagementDomainModule),
        typeof(SaasDomainModule),
        typeof(TextTemplateManagementDomainModule),
        typeof(LeptonThemeManagementDomainModule),
        typeof(LanguageManagementDomainModule),
        typeof(AbpEmailingModule),
        typeof(CmsKitProDomainModule),
        typeof(BlobStoringDatabaseDomainModule)
        )]
    [DependsOn(typeof(BloggingDomainModule))]
    [DependsOn(typeof(DocsDomainModule))]
    [DependsOn(typeof(AbpPaymentDomainModule))]
    [DependsOn(typeof(AbpPaymentPayuDomainModule))]
    [DependsOn(typeof(AbpPaymentTwoCheckoutDomainModule))]
    [DependsOn(typeof(AbpPaymentIyzicoDomainModule))]
    [DependsOn(typeof(AbpPaymentPayPalDomainModule))]
    [DependsOn(typeof(AbpPaymentStripeDomainModule))]
    [DependsOn(typeof(FileManagementDomainModule))]
    [DependsOn(typeof(FormsDomainModule))]
    [DependsOn(typeof(ChatDomainModule))]
    [DependsOn(typeof(AbpTwilioSmsModule))]
    [DependsOn(typeof(SampleDomainModule))]
    public class SQLServerDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("ar", "ar", "العربية", "ae"));
                options.Languages.Add(new LanguageInfo("en", "en", "English", "gb"));
                options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish", "fi"));
                options.Languages.Add(new LanguageInfo("fr", "fr", "Français", "fr"));
                options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
                options.Languages.Add(new LanguageInfo("it", "it", "Italian", "it"));
                options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak", "sk"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe", "tr"));
                options.Languages.Add(new LanguageInfo("sl", "sl", "Slovenščina", "si"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文", "cn"));
                options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsche", "de"));
                options.Languages.Add(new LanguageInfo("es", "es", "Español", "es"));
            });
            Configure<NewsletterOptions>(options =>
            {
                options.AddPreference(
                    "Newsletter_Default",
                    new NewsletterPreferenceDefinition(
                        LocalizableString.Create<SQLServerResource>("NewsletterPreference_Default"),
                        privacyPolicyConfirmation: LocalizableString.Create<SQLServerResource>("NewsletterPrivacyAcceptMessage")
                    )
                );
            });


#if DEBUG
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
        }
    }
}
