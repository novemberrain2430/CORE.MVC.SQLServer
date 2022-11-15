using Volo.Abp.AspNetCore.Mvc;
using Volo.CmsKit.Localization;

namespace Volo.CmsKit
{
    public abstract class CmsKitProAdminController : AbpController
    {
        protected CmsKitProAdminController()
        {
            LocalizationResource = typeof(CmsKitResource);
        }
    }
}
