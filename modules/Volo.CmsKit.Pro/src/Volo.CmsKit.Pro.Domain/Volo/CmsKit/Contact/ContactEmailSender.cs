using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;
using Volo.Abp.TextTemplating;
using Volo.CmsKit.Localization;
using Volo.CmsKit.Templates;

namespace Volo.CmsKit.Contact
{
    public class ContactEmailSender : ITransientDependency
    {
        protected IEmailSender EmailSender { get; }
        protected ITemplateRenderer TemplateRenderer { get; }
        protected IStringLocalizer<CmsKitResource> Localizer { get; }
        protected ISettingManager SettingManager { get; }

        public ContactEmailSender(
            IEmailSender emailSender,
            ITemplateRenderer templateRenderer,
            IStringLocalizer<CmsKitResource> localizer,
            ISettingManager settingManager)
        {
            EmailSender = emailSender;
            TemplateRenderer = templateRenderer;
            Localizer = localizer;
            SettingManager = settingManager;
        }

        public virtual async Task SendAsync(string name, string subject, string email, string message)
        {
            var receiverEmail = await SettingManager.GetOrNullForCurrentTenantAsync(CmsKitProSettingNames.Contact.ReceiverEmailAddress);
            
            if (string.IsNullOrWhiteSpace(receiverEmail))
            {
                throw new ArgumentNullException(Localizer["EmailToException"]);
            }

            var body = await TemplateRenderer.RenderAsync(
                CmsKitEmailTemplates.ContactEmailTemplate,
                new
                {
                    Title = Localizer["Contact"],
                    Name = $"{Localizer["Name"]} : {name}",
                    Email = $"{Localizer["EmailAddress"]} : {email}",
                    Message = $"{Localizer["Message"]} : {message}"
                }
            );

            await EmailSender.SendAsync(receiverEmail, subject, body);
        }
    }
}
