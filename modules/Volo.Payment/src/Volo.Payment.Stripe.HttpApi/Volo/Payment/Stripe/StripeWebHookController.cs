using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Timing;
using Volo.Payment.Requests;
using Volo.Payment.Subscription;

namespace Volo.Payment.Stripe
{
    [Route("payment/stripe/webhook")]
    public class StripeWebHookController : PaymentStripeController
    {
        protected StripeOptions Options { get; }

        protected IPaymentRequestAppService PaymentRequestAppService { get; }

        protected IDistributedEventBus EventBus { get; }

        public StripeWebHookController(
            IOptions<StripeOptions> options,
            IPaymentRequestAppService paymentRequestAppService,
            IDistributedEventBus eventBus)
        {
            Options = options.Value;
            PaymentRequestAppService = paymentRequestAppService;
            EventBus = eventBus;
        }

        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        [HttpPost]
        public virtual async Task<IActionResult> Webhook()
        {
            using (var streamReader = new StreamReader(HttpContext.Request.Body))
            {
                var json = await streamReader.ReadToEndAsync();

                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    Options.WebhookSecret
                );

                switch (stripeEvent.Type)
                {
                    case Events.CheckoutSessionCompleted:
                    {
                        await HandleCheckoutSessionCompletedAsync(stripeEvent);
                        break;
                    }
                    case Events.CustomerSubscriptionDeleted:
                    {
                        await HandleCustomerSubscriptionDeletedAsync(stripeEvent);
                        break;
                    }
                    case Events.CustomerSubscriptionUpdated:
                    {
                        await HandleCustomerSubscriptionUpdatedAsync(stripeEvent);
                        break;
                    }
                }
            }

            return NoContent();
        }

        private protected virtual async Task HandleCheckoutSessionCompletedAsync(Event stripeEvent)
        {
            var completePaymentRequestDto = new CompletePaymentRequestDto
            {
                GateWay = StripeConsts.GatewayName,
                Id = Guid.Parse(stripeEvent.Data.RawObject.metadata["PaymentRequestId"]?.ToString()),
                ExtraProperties =
                {
                    { "SessionId", stripeEvent.Data.RawObject.id?.ToString()},
                }
            };

            var subscriptionId = stripeEvent.Data.RawObject.subscription?.ToString();
            if (subscriptionId != null)
            {
                var subscription = await new SubscriptionService().GetAsync(subscriptionId);

                completePaymentRequestDto.IsSubscription = true;
                completePaymentRequestDto.SubscriptionInfo = new CompletePaymentRequestSubscriptionDto()
                {
                    ExternalSubscriptionId = subscription.Id,
                    PeriodEndDate = subscription.CurrentPeriodEnd
                };
            }

            await PaymentRequestAppService.CompleteAsync(completePaymentRequestDto);
        }

        private protected virtual async Task HandleCustomerSubscriptionUpdatedAsync(Event stripeEvent)
        {
            var paymentRequest =
                await PaymentRequestAppService.GetByExternalSubscriptionAsync(
                    (string) stripeEvent.Data.RawObject.id);

            var paymentUpdatedEto =
                ObjectMapper.Map<PaymentRequestDto, SubscriptionUpdatedEto>(paymentRequest);

            paymentUpdatedEto.PeriodEndDate =
                ConvertToDateTime((int) stripeEvent.Data.RawObject.current_period_end);

            await EventBus.PublishAsync(paymentUpdatedEto);
        }

        private protected virtual async Task HandleCustomerSubscriptionDeletedAsync(Event stripeEvent)
        {
            var paymentRequest =
                await PaymentRequestAppService.GetByExternalSubscriptionAsync(
                    (string) stripeEvent.Data.RawObject.id);

            var subscriptionCanceledEto =
                ObjectMapper.Map<PaymentRequestDto, SubscriptionCanceledEto>(paymentRequest);

            subscriptionCanceledEto.PeriodEndDate =
                ConvertToDateTime((int) stripeEvent.Data.RawObject.current_period_end);

            await EventBus.PublishAsync(subscriptionCanceledEto);
        }

        protected DateTime ConvertToDateTime(int unixSeconds)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixSeconds).UtcDateTime;
        }
    }
}