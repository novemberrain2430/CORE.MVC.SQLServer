using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Payment.Localization;

namespace Volo.Payment.Stripe
{
    public class PaymentStripeController : AbpController
    {
        public PaymentStripeController()
        {
            LocalizationResource = typeof(PaymentResource);
        }
    }
}
