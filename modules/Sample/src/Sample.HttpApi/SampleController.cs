using Sample.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Sample
{
    public abstract class SampleController : AbpController
    {
        protected SampleController()
        {
            LocalizationResource = typeof(SampleResource);
        }
    }
}
