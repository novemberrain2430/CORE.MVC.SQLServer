using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;

namespace Volo.CmsKit.Newsletters
{
    public class NewsletterRecordManager : CmsKitProDomainServiceBase
    {
        protected INewsletterRecordRepository NewsletterRecordsRepository { get; }
        protected INewsletterPreferenceDefinitionStore NewsletterPreferenceDefinitionStore { get; }

        public NewsletterRecordManager(
            INewsletterRecordRepository newsletterRecordsRepository,
            INewsletterPreferenceDefinitionStore newsletterPreferenceDefinitionStore)
        {
            NewsletterRecordsRepository = newsletterRecordsRepository;
            NewsletterPreferenceDefinitionStore = newsletterPreferenceDefinitionStore;
        }

        public virtual async Task<List<NewsletterPreferenceDefinition>> GetNewsletterPreferencesAsync()
        {
            return await NewsletterPreferenceDefinitionStore.GetNewslettersAsync();
        }

        public virtual async Task<NewsletterRecord> CreateOrUpdateAsync(
            [NotNull] string emailAddress,
            [NotNull] string preference,
            [NotNull] string source,
            [NotNull] string sourceUrl,
            List<string> additionalPreferences)
        {
            Check.NotNullOrWhiteSpace(emailAddress, nameof(emailAddress));
            Check.NotNullOrWhiteSpace(preference, nameof(preference));
            Check.NotNullOrWhiteSpace(source, nameof(source));
            Check.NotNullOrWhiteSpace(sourceUrl, nameof(sourceUrl));

            additionalPreferences ??= new List<string>();

            var newsletterRecord = await NewsletterRecordsRepository.FindByEmailAddressAsync(emailAddress);

            if (newsletterRecord is null)
            {
                newsletterRecord = new NewsletterRecord(GuidGenerator.Create(), emailAddress, CurrentTenant.Id);

                newsletterRecord.AddPreferences(
                    new NewsletterPreference(GuidGenerator.Create(),
                        newsletterRecord.Id,
                        preference,
                        source,
                        sourceUrl,
                        CurrentTenant.Id)
                );

                foreach (var additionalPreference in additionalPreferences)
                {
                    newsletterRecord.AddPreferences(
                        new NewsletterPreference(GuidGenerator.Create(),
                            newsletterRecord.Id,
                            additionalPreference,
                            source,
                            sourceUrl,
                            CurrentTenant.Id)
                    );
                }

                return await NewsletterRecordsRepository.InsertAsync(newsletterRecord);
            }

            var newsletterPreference = newsletterRecord.Preferences.FirstOrDefault(x => x.Preference == preference);
            if (newsletterPreference is null)
            {
                newsletterRecord = newsletterRecord.AddPreferences(
                    new NewsletterPreference(GuidGenerator.Create(),
                        newsletterRecord.Id,
                        preference,
                        source,
                        sourceUrl,
                        CurrentTenant.Id)
                );

                foreach (var additionalPreference in additionalPreferences)
                {
                    newsletterRecord.AddPreferences(
                        new NewsletterPreference(GuidGenerator.Create(),
                            newsletterRecord.Id,
                            additionalPreference,
                            source,
                            sourceUrl,
                            CurrentTenant.Id)
                    );
                }

                return await NewsletterRecordsRepository.UpdateAsync(newsletterRecord);
            }

            foreach (var additionalPreference in additionalPreferences)
            {
                var isExistingAdditionalPreference =
                    newsletterRecord.Preferences.Any(x => x.Preference == additionalPreference);
                if (!isExistingAdditionalPreference)
                {
                    newsletterRecord.AddPreferences(
                        new NewsletterPreference(GuidGenerator.Create(),
                            newsletterRecord.Id,
                            additionalPreference,
                            source,
                            sourceUrl,
                            CurrentTenant.Id)
                    );
                }
            }

            return await NewsletterRecordsRepository.UpdateAsync(newsletterRecord);
        }
    }
}