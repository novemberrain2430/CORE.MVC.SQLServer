using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.CmsKit.Newsletters;

namespace Volo.CmsKit.Pro
{
    public class CmsKitProDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly CmsKitProTestData _cmsKitProTestData;
        private readonly INewsletterRecordRepository _newsletterRecordRepository;

        public CmsKitProDataSeedContributor(
            IGuidGenerator guidGenerator,
            CmsKitProTestData cmsKitProTestData,
            INewsletterRecordRepository newsletterRecordRepository)
        {
            _guidGenerator = guidGenerator;
            _cmsKitProTestData = cmsKitProTestData;
            _newsletterRecordRepository = newsletterRecordRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await SeedNewsletterRecordAsync();
        }

        private async Task SeedNewsletterRecordAsync()
        {
            var newsletterRecord = new NewsletterRecord(_cmsKitProTestData.NewsletterEmailId, _cmsKitProTestData.Email);
            
            newsletterRecord.AddPreferences(new NewsletterPreference(_guidGenerator.Create(), newsletterRecord.Id,
                _cmsKitProTestData.Preference, _cmsKitProTestData.Source, "sourceUrl")
            );
            
            await _newsletterRecordRepository.InsertAsync(newsletterRecord);
            
            var newsletterRecord2 = new NewsletterRecord(_guidGenerator.Create(), "info@volosoft.io");
            
            newsletterRecord2.AddPreferences(new NewsletterPreference(_guidGenerator.Create(), newsletterRecord2.Id,
                "preference2", "source2", "sourceUrl2")
            );
            
            await _newsletterRecordRepository.InsertAsync(newsletterRecord2);

            var newsletterRecord3 = new NewsletterRecord(_guidGenerator.Create(), "info@aspnetzero.io");
            
            newsletterRecord3.AddPreferences(new NewsletterPreference(_guidGenerator.Create(), newsletterRecord3.Id,
                "preference3", "source3", "sourceUrl3")
            );
            
            await _newsletterRecordRepository.InsertAsync(newsletterRecord3);
        }
    }
}