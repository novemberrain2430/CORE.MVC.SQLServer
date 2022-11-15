using System;
using System.Collections.Generic;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Payment.Gateways;
using Volo.Payment.Requests;

namespace Volo.Payment.Iyzico
{
    public class IyzicoPaymentGateway : IPaymentGateway, ITransientDependency
    {
        private readonly IyzicoOptions _options;

        public IyzicoPaymentGateway(IOptions<IyzicoOptions> options)
        {
            _options = options.Value;
        }

        public bool IsValid(PaymentRequest paymentRequest, Dictionary<string, object> properties)
        {
            var token = properties["token"]?.ToString();

            var request = new RetrieveCheckoutFormRequest
            {
                ConversationId = paymentRequest.Id.ToString(),
                Token = token
            };

            var checkoutForm = CheckoutForm.Retrieve(request, new Iyzipay.Options
            {
                ApiKey = _options.ApiKey,
                SecretKey = _options.SecretKey,
                BaseUrl = _options.BaseUrl
            });

            if (!checkoutForm.ErrorCode.IsNullOrEmpty())
            {
                throw new UserFriendlyException(checkoutForm.ErrorCode, "Your payment is not verified !");
            }

            return checkoutForm.PaymentStatus == "SUCCESS";
        }
    }
}
