using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Payment.Plans;
using Volo.Payment.Requests;

namespace Volo.Payment.Stripe.Pages.Payment.Stripe
{
    public class PrePaymentModel : AbpPageModel
    {
        private static readonly Dictionary<PaymentType, string> modeMapping = new Dictionary<PaymentType, string>
        {
            { PaymentType.OneTime, "payment" },
            { PaymentType.Subscription, "subscription" }
        };

        [BindProperty] public Guid PaymentRequestId { get; set; }

        public string PublishableKey { get; set; }

        public string SessionId { get; set; }

        protected PaymentRequestWithDetailsDto PaymentRequest { get; set; }

        protected PaymentWebOptions PaymentWebOptions { get; }

        protected StripeOptions StripeOptions { get; }

        protected IPaymentRequestAppService PaymentRequestAppService { get; }

        protected IPlanAppService PlanAppService { get; }

        private readonly IPurchaseParameterListGenerator _purchaseParameterListGenerator;

        public PrePaymentModel(
            IOptions<PaymentWebOptions> paymentWebOptions,
            IOptions<StripeOptions> stripeOptions,
            IPaymentRequestAppService paymentRequestAppService,
            IPlanAppService planAppService,
            IPurchaseParameterListGenerator purchaseParameterListGenerator)
        {
            PaymentWebOptions = paymentWebOptions.Value;
            StripeOptions = stripeOptions.Value;
            PaymentRequestAppService = paymentRequestAppService;
            PlanAppService = planAppService;
            _purchaseParameterListGenerator = purchaseParameterListGenerator;
        }

        public virtual ActionResult OnGet()
        {
            return BadRequest();
        }

        public virtual async Task OnPostAsync()
        {
            PaymentRequest = await PaymentRequestAppService.GetAsync(PaymentRequestId);
            var purchaseParameters = _purchaseParameterListGenerator.GetExtraParameterConfiguration(PaymentRequest);

            var lineItems = new List<SessionLineItemOptions>();

            foreach (var product in PaymentRequest.Products)
            {
                var lineItem = new SessionLineItemOptions
                {
                    Quantity = product.Count,
                };

                if (product.PaymentType == PaymentType.Subscription)
                {
                    var gatewayPlan = await PlanAppService.GetGatewayPlanAsync(product.PlanId, StripeConsts.GatewayName);
                    lineItem.Price = gatewayPlan.ExternalId;
                }

                if (product.PaymentType == PaymentType.OneTime)
                {
                    lineItem.PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = Convert.ToDecimal(product.UnitPrice) * 100,
                        Currency = purchaseParameters.Currency,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Name,
                            Metadata = new Dictionary<string, string>
                            {
                                { "ProductCode", product.Code }
                            }
                        }
                    };
                }

                lineItems.Add(lineItem);
            }

            var options = new SessionCreateOptions
            {
                Locale = purchaseParameters.Locale,
                PaymentMethodTypes = purchaseParameters.PaymentMethodTypes,
                LineItems = lineItems,

                Mode = modeMapping[PaymentRequest.Products[0].PaymentType],

                SuccessUrl = PaymentWebOptions.RootUrl.RemovePostFix("/") + StripeConsts.PostPaymentUrl +
                             "?SessionId={CHECKOUT_SESSION_ID}",
                CancelUrl = PaymentWebOptions.RootUrl,

                Metadata = new Dictionary<string, string>
                {
                    {"PaymentRequestId", PaymentRequestId.ToString()}
                }
            };

            var sessionService = new SessionService();
            var session = await sessionService.CreateAsync(options);

            PublishableKey = StripeOptions.PublishableKey;
            SessionId = session.Id;
        }
    }
}
