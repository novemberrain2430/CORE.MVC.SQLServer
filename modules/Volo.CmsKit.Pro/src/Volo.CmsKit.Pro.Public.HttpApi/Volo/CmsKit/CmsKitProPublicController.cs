using Volo.Abp.AspNetCore.Mvc;
using Volo.CmsKit.Localization;

namespace Volo.CmsKit
{
    public abstract class CmsKitProPublicController : AbpController
    {
        protected CmsKitProPublicController()
        {
            LocalizationResource = typeof(CmsKitResource);
        }
    }
}
