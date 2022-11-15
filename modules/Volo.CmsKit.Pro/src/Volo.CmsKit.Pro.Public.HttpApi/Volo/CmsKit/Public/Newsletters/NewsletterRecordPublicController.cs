using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.GlobalFeatures;
using Volo.CmsKit.GlobalFeatures;

namespace Volo.CmsKit.Public.Newsletters
{
    [RequiresGlobalFeature(typeof(NewslettersFeature))]
    [RemoteService(Name = CmsKitProPublicRemoteServiceConsts.RemoteServiceName)]
    [Area("cms-kit")]
    [Route("api/cms-kit-public/newsletter")]
    public class NewsletterRecordPublicController : CmsKitProPublicController, INewsletterRecordPublicAppService
    {
        protected INewsletterRecordPublicAppService NewsletterRecordPublicAppService { get; }

        public NewsletterRecordPublicController(INewsletterRecordPublicAppService newsletterRecordPublicAppService)
        {
            NewsletterRecordPublicAppService = newsletterRecordPublicAppService;
        }

        [HttpPost]
        public virtual Task CreateAsync(CreateNewsletterRecordInput input)
        {
            return NewsletterRecordPublicAppService.CreateAsync(input);
        }

        [HttpGet]
        [Route("emailAddress")]
        public virtual Task<List<NewsletterPreferenceDetailsDto>> GetNewsletterPreferencesAsync(string emailAddress)
        {
            return NewsletterRecordPublicAppService.GetNewsletterPreferencesAsync(emailAddress);
        }

        [HttpPut]
        public virtual async Task UpdatePreferencesAsync(UpdatePreferenceRequestInput input)
        {
            await NewsletterRecordPublicAppService.UpdatePreferencesAsync(input);
        }

        [HttpGet]
        [Route("preference-options")]
        public virtual async Task<NewsletterEmailOptionsDto> GetOptionByPreference(string preference)
        {
            return await NewsletterRecordPublicAppService.GetOptionByPreference(preference);
        }
    }
}
