using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Emailing;
using Volo.Abp.TextTemplating;
using Volo.Abp.UI.Navigation.Urls;
using Volo.CmsKit.Newsletters;
using Volo.CmsKit.Newsletters.Helpers;
using Volo.CmsKit.Public.Emailing.Templates;

namespace Volo.CmsKit.Public.Newsletters
{
    public class NewsletterRecordPublicAppService : PublicAppService, INewsletterRecordPublicAppService
    {
        protected INewsletterPreferenceDefinitionStore NewsletterPreferenceDefinitionStore { get; }

        protected INewsletterRecordRepository NewsletterRecordsRepository { get; }

        protected NewsletterRecordManager NewsletterRecordsManager { get; }

        protected IEmailSender EmailSender { get; }

        protected ITemplateRenderer TemplateRenderer { get; }

        protected SecurityCodeProvider SecurityCodeProvider { get; }

        protected IAppUrlProvider AppUrlProvider { get; }

        public NewsletterRecordPublicAppService(
            INewsletterPreferenceDefinitionStore newsletterPreferenceDefinitionStore,
            INewsletterRecordRepository newsletterRecordsRepository,
            NewsletterRecordManager newsletterRecordsManager,
            IEmailSender emailSender,
            ITemplateRenderer templateRenderer,
            SecurityCodeProvider securityCodeProvider,
            IAppUrlProvider appUrlProvider)
        {
            NewsletterPreferenceDefinitionStore = newsletterPreferenceDefinitionStore;
            NewsletterRecordsRepository = newsletterRecordsRepository;
            NewsletterRecordsManager = newsletterRecordsManager;
            EmailSender = emailSender;
            TemplateRenderer = templateRenderer;
            SecurityCodeProvider = securityCodeProvider;
            AppUrlProvider = appUrlProvider;
        }

        public virtual async Task CreateAsync(CreateNewsletterRecordInput input)
        {
            var newsletterRecord = await NewsletterRecordsRepository.FindByEmailAddressAsync(input.EmailAddress);
            if (newsletterRecord != null)
            {
                var isExistingEmailPreference = newsletterRecord.Preferences.Any(x => x.Preference == input.Preference);
                if (isExistingEmailPreference && input.AdditionalPreferences.Count < 1)
                {
                    return;
                }
            }

            await NewsletterRecordsManager.CreateOrUpdateAsync(
                input.EmailAddress,
                input.Preference,
                input.Source,
                input.SourceUrl,
                input.AdditionalPreferences);

            await NewsletterRecordNotifyAsync(input.EmailAddress, NewsletterEmailStatus.Subscription);
        }

        public virtual async Task<List<NewsletterPreferenceDetailsDto>> GetNewsletterPreferencesAsync(string emailAddress)
        {
            var newsletterRecord = await NewsletterRecordsRepository.FindByEmailAddressAsync(emailAddress);

            if (newsletterRecord is null)
            {
                return new List<NewsletterPreferenceDetailsDto>();
            }

            var newsletterPreferences = await NewsletterRecordsManager.GetNewsletterPreferencesAsync();

            var newsletterPreferencesDto = new List<NewsletterPreferenceDetailsDto>();
            
            foreach (var preference in newsletterPreferences)
            {
                newsletterPreferencesDto.Add(new NewsletterPreferenceDetailsDto
                {
                    Preference = preference.Preference,
                    DisplayPreference = preference.DisplayPreference.Localize(StringLocalizerFactory),
                    IsSelectedByEmailAddress = newsletterRecord.Preferences.Any(x => x.Preference == preference.Preference),
                    Definition = preference.Definition?.Localize(StringLocalizerFactory)
                });
            }

            return newsletterPreferencesDto;
        }

        public virtual async Task UpdatePreferencesAsync(UpdatePreferenceRequestInput input)
        {
            var securityCode = SecurityCodeProvider.GetSecurityCode(input.EmailAddress);
            if (securityCode != input.SecurityCode)
            {
                return;
            }

            var newsletterRecord = await NewsletterRecordsRepository.FindByEmailAddressAsync(input.EmailAddress);
            if (newsletterRecord is null)
            {
                return;
            }

            var isExistingEnabledPreference = input.PreferenceDetails.Any(x => x.IsEnabled == true);
            if (!isExistingEnabledPreference)
            {
                await NewsletterRecordsRepository.DeleteAsync(newsletterRecord.Id);
                await NewsletterRecordNotifyAsync(input.EmailAddress, NewsletterEmailStatus.DeleteSubscription);
                return;
            }

            var isPreferenceChanged = false;

            foreach (var preferenceDetail in input.PreferenceDetails)
            {
                var newsletterPreference = newsletterRecord.Preferences.FirstOrDefault(x => x.Preference == preferenceDetail.Preference);
                if (newsletterPreference is null && preferenceDetail.IsEnabled)
                {
                    newsletterRecord.AddPreferences(
                        new NewsletterPreference(GuidGenerator.Create(),
                            newsletterRecord.Id,
                            preferenceDetail.Preference,
                            input.Source,
                            input.SourceUrl,
                            CurrentTenant.Id)
                    );
                    isPreferenceChanged = true;
                }
                else if (newsletterPreference != null && !preferenceDetail.IsEnabled)
                {
                    newsletterRecord.RemovePreference(newsletterPreference.Id);
                    isPreferenceChanged = true;
                }
            }
            await NewsletterRecordsRepository.UpdateAsync(newsletterRecord);

            if (isPreferenceChanged)
            {
                await NewsletterRecordNotifyAsync(input.EmailAddress, NewsletterEmailStatus.UpdatePreference);
            }
        }

        public virtual async Task<NewsletterEmailOptionsDto> GetOptionByPreference([NotNull] string preference)
        {
            var newsletterPreferenceDefinitions = await NewsletterRecordsManager.GetNewsletterPreferencesAsync();

            var newsletterPreference = newsletterPreferenceDefinitions.FirstOrDefault(x =>
                x.Preference == preference);

            var dto = ObjectMapper.Map<NewsletterPreferenceDefinition, NewsletterEmailOptionsDto>(newsletterPreference);

            if (newsletterPreference?.AdditionalPreferences is null || newsletterPreference.AdditionalPreferences.Count == 0)
            {
                return dto;
            }

            for (var i = 0; i < newsletterPreferenceDefinitions.Count; i++)
            {
                if (newsletterPreferenceDefinitions[i].AdditionalPreferences is null)
                {
                    continue;
                }

                foreach (var additionalPreference in newsletterPreferenceDefinitions[i].AdditionalPreferences)
                {
                    var verifiedAdditionalPreference = newsletterPreferenceDefinitions.FirstOrDefault(x =>
                        x.Preference == additionalPreference);

                    if (verifiedAdditionalPreference != null && additionalPreference != preference)
                    {
                        dto.AdditionalPreferences.Add(verifiedAdditionalPreference.Preference);
                        dto.DisplayAdditionalPreferences.Add(verifiedAdditionalPreference.DisplayPreference.Localize(StringLocalizerFactory));
                    }
                }
            }

            // TODO check `?`
            dto.PrivacyPolicyConfirmation = newsletterPreference.PrivacyPolicyConfirmation?.Localize(StringLocalizerFactory);
            dto.AdditionalPreferences = dto.AdditionalPreferences.Distinct().ToList();
            dto.DisplayAdditionalPreferences = dto.DisplayAdditionalPreferences.Distinct().ToList();
            return dto;
        }

        private async Task NewsletterRecordNotifyAsync([NotNull] string emailAddress, NewsletterEmailStatus emailStatus)
        {
            var (subject, emailBodyModel) = await GetEmailSubjectAndBody(emailAddress, emailStatus);

            var body = await TemplateRenderer.RenderAsync(CmsKitEmailTemplates.NewsletterEmailTemplate, emailBodyModel);

            await EmailSender.SendAsync(
                emailAddress,
                subject,
                body
            );
        }

        private async Task<(string, NewsletterEmailBodyModel)> GetEmailSubjectAndBody([NotNull] string emailAddress, NewsletterEmailStatus emailStatus)
        {
            var rootUrl = await AppUrlProvider.GetUrlOrNullAsync("MVCPublic") ?? await AppUrlProvider.GetUrlAsync("MVC");
            var securityCode = SecurityCodeProvider.GetSecurityCode(emailAddress);

            var subject = "";
            var emailBodyModel = new NewsletterEmailBodyModel
            {
                Root = rootUrl.EnsureEndsWith('/'),
                Text = L["NewsletterEmailFooterTemplateManageEmail"],
                Email = emailAddress,
                Code = securityCode
            };

            switch (emailStatus)
            {
                case NewsletterEmailStatus.Subscription:
                    emailBodyModel.Title = L["NewsletterEmailTitle"];
                    emailBodyModel.Description = L["NewsletterEmailFooterCreateTemplateMessage"];
                    subject = L["NewsletterEmailSubject"];
                    break;
                case NewsletterEmailStatus.UpdatePreference:
                    emailBodyModel.Title = L["NewsletterUpdatePreferenceTitle"];
                    emailBodyModel.Description = L["NewsletterEmailFooterUpdateTemplateMessage"];
                    subject = L["NewsletterUpdatePreferenceTitleSubject"];
                    break;
                case NewsletterEmailStatus.DeleteSubscription:
                    emailBodyModel = new NewsletterEmailBodyModel();
                    emailBodyModel.Title = L["NewsletterDeleteSubscriptionTitle"];
                    subject = L["NewsletterDeleteSubscriptionSubject"];
                    break;
            }

            return (subject, emailBodyModel);
        }
    }
}
