using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.GlobalFeatures;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.Permissions;

namespace Volo.CmsKit.Admin.Newsletters
{
    [RequiresGlobalFeature(typeof(NewslettersFeature))]
    [RemoteService(Name = CmsKitAdminRemoteServiceConsts.RemoteServiceName)]
    [Area("cms-kit")]
    [Route("api/cms-kit-admin/newsletter")]
    [Authorize(CmsKitProAdminPermissions.Newsletters.Default)]
    public class NewsletterRecordAdminController : CmsKitProAdminController, INewsletterRecordAdminAppService
    {
        protected INewsletterRecordAdminAppService NewsletterRecordAdminAppService { get; }

        public NewsletterRecordAdminController(INewsletterRecordAdminAppService newsletterRecordAdminAppService)
        {
            NewsletterRecordAdminAppService = newsletterRecordAdminAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<NewsletterRecordDto>> GetListAsync(GetNewsletterRecordsRequestInput input)
        {
            return NewsletterRecordAdminAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<NewsletterRecordWithDetailsDto> GetAsync(Guid id)
        {
            return NewsletterRecordAdminAppService.GetAsync(id);
        }

        [NonAction]
        public Task<List<NewsletterRecordCsvDto>> GetNewsletterRecordsCsvDetailAsync(GetNewsletterRecordsCsvRequestInput input)
        {
            return NewsletterRecordAdminAppService.GetNewsletterRecordsCsvDetailAsync(input);
        }

        [HttpGet]
        [Route("preferences")]
        public Task<List<string>> GetNewsletterPreferencesAsync()
        {
            return NewsletterRecordAdminAppService.GetNewsletterPreferencesAsync();
        }

        [HttpGet]
        [Route("export-csv")]
        public async Task<IActionResult> ExportCsv(GetNewsletterRecordsCsvRequestInput input)
        {
            var newsletters = await NewsletterRecordAdminAppService.GetNewsletterRecordsCsvDetailAsync(input);

            var csvConfiguration = new CsvConfiguration(new System.Globalization.CultureInfo(CultureInfo.CurrentUICulture.Name));
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(stream: memoryStream, encoding: new UTF8Encoding(true)))
                {
                    using (var csvWriter = new CsvWriter(streamWriter, csvConfiguration))
                    {
                        await csvWriter.WriteRecordsAsync(newsletters);
                    }

                    await memoryStream.FlushAsync();

                    input.Preference = input.Preference?.Insert(0, "-");
                    input.Source = input.Source?.Insert(0, "-");

                    var file = File(memoryStream.ToArray(), "text/csv", $"newsletter-emails-{DateTime.Now.ToString("yyyyMMddHHmmss")}{input.Preference}{input.Source}.csv");

                    return file;
                }
            }
        }
    }
}
