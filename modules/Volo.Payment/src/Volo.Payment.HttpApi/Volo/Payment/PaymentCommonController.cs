using Volo.Abp.AspNetCore.Mvc;
using Volo.Payment.Localization;

namespace Volo.Payment
{
    public class PaymentCommonController : AbpController
    {
        public PaymentCommonController()
        {
            LocalizationResource = typeof(PaymentResource);
        }
    }
}
