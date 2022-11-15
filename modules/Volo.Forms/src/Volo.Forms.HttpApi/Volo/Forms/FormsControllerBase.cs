using Volo.Abp.AspNetCore.Mvc;
using Volo.Forms.Localization;

namespace Volo.Forms
{
    public class FormsControllerBase : AbpController
    {
        public FormsControllerBase()
        {
            LocalizationResource = typeof(FormsResource);
        }
    }
}
