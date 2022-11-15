using Sample.Localization;
using Volo.Abp.Application.Services;

namespace Sample
{
    public abstract class SampleAppService : ApplicationService
    {
        protected SampleAppService()
        {
            LocalizationResource = typeof(SampleResource);
            ObjectMapperContext = typeof(SampleApplicationModule);
        }
    }
}
