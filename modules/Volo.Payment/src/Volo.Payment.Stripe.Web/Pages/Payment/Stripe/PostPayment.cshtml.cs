using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using Volo.Payment.Requests;

namespace Volo.Payment.Stripe.Pages.Payment.Stripe
{
    public class PostPaymentModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SessionId { get; set; }

        protected IPaymentRequestAppService PaymentRequestAppService { get; }

        public ILogger<PostPaymentModel> Logger { get; set; }

        private readonly IOptions<PaymentWebOptions> _paymentWebOptions;
        private readonly IPurchaseParameterListGenerator _purchaseParameterListGenerator;

        public PostPaymentModel(
            IPaymentRequestAppService paymentRequestAppService,
            IOptions<PaymentWebOptions> paymentWebOptions,
            IPurchaseParameterListGenerator purchaseParameterListGenerator)
        {
            PaymentRequestAppService = paymentRequestAppService;
            _paymentWebOptions = paymentWebOptions;
            _purchaseParameterListGenerator = purchaseParameterListGenerator;
            Logger = NullLogger<PostPaymentModel>.Instance;
        }

        public virtual async Task<IActionResult> OnGetAsync()
        {
            if (SessionId.IsNullOrWhiteSpace())
            {
                return BadRequest();
            }

            var sessionService = new SessionService();
            var session = await sessionService.GetAsync(SessionId);

            var paymentRequestId = Guid.Parse(session.Metadata["PaymentRequestId"]);

            Logger.LogInformation("Stripe session object: " + session.ToJson());

            var completePaymentRequestDto = new CompletePaymentRequestDto
            {
                GateWay = StripeConsts.GatewayName,
                Id = paymentRequestId,
                ExtraProperties =
                {
                    { "SessionId", session.Id },
                }
            };

            if (session.SubscriptionId != null)
            {
                var subscription = await new SubscriptionService().GetAsync(session.SubscriptionId);
                
                completePaymentRequestDto.IsSubscription = true;
                completePaymentRequestDto.SubscriptionInfo = new CompletePaymentRequestSubscriptionDto()
                {
                    ExternalSubscriptionId = subscription.Id,
                    PeriodEndDate = subscription.CurrentPeriodEnd
                };
            }

            await PaymentRequestAppService.CompleteAsync(completePaymentRequestDto);

            if (!_paymentWebOptions.Value.CallbackUrl.IsNullOrWhiteSpace())
            {
                var callbackUrl = _paymentWebOptions.Value.CallbackUrl + "?paymentRequestId=" + paymentRequestId;
                var paymentRequest = await PaymentRequestAppService.GetAsync(paymentRequestId);
                var extraPaymentParameters = _purchaseParameterListGenerator.GetExtraParameterConfiguration(paymentRequest);

                if (!extraPaymentParameters.AdditionalCallbackParameters.IsNullOrEmpty())
                {
                    callbackUrl += "&" + extraPaymentParameters.AdditionalCallbackParameters;
                }

                Response.Redirect(callbackUrl);
            }

            return Page();
        }


        public virtual IActionResult OnPost()
        {
            return BadRequest();
        }
    }
}
