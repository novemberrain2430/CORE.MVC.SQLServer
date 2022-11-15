using Volo.Abp.AspNetCore.Mvc;
using Volo.CmsKit.Localization;

namespace Volo.CmsKit
{
    public abstract class CmsKitProController : AbpController
    {
        protected CmsKitProController()
        {
            LocalizationResource = typeof(CmsKitResource);
        }
    }
}
