using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.CmsKit.Newsletters;
using Volo.CmsKit.Newsletters.Helpers;
using Volo.CmsKit.Permissions;

namespace Volo.CmsKit.Admin.Newsletters
{
    [Authorize(CmsKitProAdminPermissions.Newsletters.Default)]
    public class NewsletterRecordAdminAppService : CmsKitProAdminAppService, INewsletterRecordAdminAppService
    {
        protected INewsletterRecordRepository NewsletterRecordsRepository { get; }
        
        protected NewsletterRecordManager NewsletterRecordManager { get; }
        
        protected SecurityCodeProvider SecurityCodeProvider { get; }

        public NewsletterRecordAdminAppService(
            INewsletterRecordRepository newsletterRecordsRepository, 
            NewsletterRecordManager newsletterRecordManager,
            SecurityCodeProvider securityCodeProvider)
        {
            NewsletterRecordsRepository = newsletterRecordsRepository;
            NewsletterRecordManager = newsletterRecordManager;
            SecurityCodeProvider = securityCodeProvider;
        }

        public async Task<PagedResultDto<NewsletterRecordDto>> GetListAsync(GetNewsletterRecordsRequestInput input)
        {
            var count = await NewsletterRecordsRepository.GetCountByFilterAsync(input.Preference, input.Source);
            
            var newsletterSummaries = await NewsletterRecordsRepository.GetListAsync(
                input.Preference,
                input.Source,
                input.SkipCount,
                input.MaxResultCount
            );

            return new PagedResultDto<NewsletterRecordDto>(
                count,
                ObjectMapper.Map<List<NewsletterSummaryQueryResultItem>, List<NewsletterRecordDto>>(newsletterSummaries)
            );
        }

        public async Task<NewsletterRecordWithDetailsDto> GetAsync(Guid id)
        {
            var newsletterRecord = await NewsletterRecordsRepository.GetAsync(id);

            return ObjectMapper.Map<NewsletterRecord, NewsletterRecordWithDetailsDto>(newsletterRecord);
        }

        public async Task<List<NewsletterRecordCsvDto>> GetNewsletterRecordsCsvDetailAsync(GetNewsletterRecordsCsvRequestInput input)
        {
            var count = await NewsletterRecordsRepository.GetCountAsync();
        
            var newsletters = await NewsletterRecordsRepository.GetListAsync(input.Preference, input.Source,0, int.MaxValue);
        
            var newsletterRecordsCsvDto = ObjectMapper.Map<List<NewsletterSummaryQueryResultItem>, List<NewsletterRecordCsvDto>>(newsletters);
        
            foreach (var newsletterRecord in newsletterRecordsCsvDto)
            {
                newsletterRecord.SecurityCode = SecurityCodeProvider.GetSecurityCode(newsletterRecord.EmailAddress);
            }
            
            return newsletterRecordsCsvDto;
        }

        public async Task<List<string>> GetNewsletterPreferencesAsync()
        {
            var newsletterPreferences = await NewsletterRecordManager.GetNewsletterPreferencesAsync();

            return newsletterPreferences.Select(newsletterPreference => newsletterPreference.Preference).ToList();
        }
    }
}