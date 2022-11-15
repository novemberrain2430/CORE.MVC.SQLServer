using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Owl.reCAPTCHA;
using Owl.reCAPTCHA.v3;
using Volo.Abp;
using Volo.Abp.GlobalFeatures;
using Volo.CmsKit.GlobalFeatures;

namespace Volo.CmsKit.Public.Contact
{
    [RequiresGlobalFeature(typeof(ContactFeature))]
    [RemoteService(Name = CmsKitProPublicRemoteServiceConsts.RemoteServiceName)]
    [Area("cms-kit")]
    [Route("api/cms-kit-public/contacts")]
    public class ContactPublicController : CmsKitProPublicController, IContactPublicAppService
    {
        protected IContactPublicAppService ContactPublicAppService { get; }
        
        protected IreCAPTCHASiteVerifyV3 SiteVerify { get; }

        public ContactPublicController(IContactPublicAppService contactPublicAppService, IreCAPTCHASiteVerifyV3 siteVerify)
        {
            ContactPublicAppService = contactPublicAppService;
            SiteVerify = siteVerify;
        }
        
        [HttpPost]
        public virtual async Task SendMessageAsync(ContactCreateInput input)
        {
            var response = await SiteVerify.Verify(new reCAPTCHASiteVerifyRequest
            {
                Response = input.RecaptchaToken,
                RemoteIp = HttpContext.Connection.RemoteIpAddress.ToString()
            });

            if (response.Success && response.Score > 0.5)
            {
                await ContactPublicAppService.SendMessageAsync(input);
            }
            else
            {
                throw new UserFriendlyException(L["RecaptchaError"]);
            }
        }
    }
}