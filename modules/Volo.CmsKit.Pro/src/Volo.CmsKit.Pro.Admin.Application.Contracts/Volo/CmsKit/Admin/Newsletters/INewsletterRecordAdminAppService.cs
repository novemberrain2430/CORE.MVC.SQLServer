using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Volo.CmsKit.Admin.Newsletters
{
    public interface INewsletterRecordAdminAppService : IApplicationService
    {
        Task<PagedResultDto<NewsletterRecordDto>> GetListAsync(GetNewsletterRecordsRequestInput input);

        Task<NewsletterRecordWithDetailsDto> GetAsync(Guid id);

        Task<List<NewsletterRecordCsvDto>> GetNewsletterRecordsCsvDetailAsync(GetNewsletterRecordsCsvRequestInput input);

        Task<List<string>> GetNewsletterPreferencesAsync();
    }
}