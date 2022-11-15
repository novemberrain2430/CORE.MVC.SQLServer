using System;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Timing;
using Volo.Payment.Requests;

namespace Volo.Payment.TwoCheckout.Pages.Payment.TwoCheckout
{
    public class PurchaseUrlGenerator : IPurchaseUrlGenerator, ITransientDependency
    {
        private readonly TwoCheckoutOptions _options;
        private readonly PaymentWebOptions _paymentGatewayOptions;
        private readonly TwoCheckoutHashCalculator _twoCheckoutHashCalculator;

        public PurchaseUrlGenerator(
            IOptions<TwoCheckoutOptions> options,
            IOptions<PaymentWebOptions> paymentGatewayOptions,
            TwoCheckoutHashCalculator twoCheckoutHashCalculator)
        {
            _twoCheckoutHashCalculator = twoCheckoutHashCalculator;
            _options = options.Value;
            _paymentGatewayOptions = paymentGatewayOptions.Value;
        }

        public string GetCurrency(PaymentRequestWithDetailsDto paymentRequest)
        {
            return _options.CurrencyCode;
        }

        public TwoCheckoutPaymentRequestExtraParameterConfiguration GetExtraParameterConfiguration(
            PaymentRequestWithDetailsDto paymentRequest)
        {
            var configuration = new TwoCheckoutPaymentRequestExtraParameterConfiguration
            {
                Currency = _options.CurrencyCode,
                Language = _options.LanguageCode
            };

            if (!paymentRequest.ExtraProperties.ContainsKey(TwoCheckoutConsts.GatewayName))
            {
                return configuration;
            }

            var json = paymentRequest.ExtraProperties[TwoCheckoutConsts.GatewayName].ToString();
            var overrideConfiguration = Newtonsoft.Json.JsonConvert
                .DeserializeObject<TwoCheckoutPaymentRequestExtraParameterConfiguration>(json);

            if (!overrideConfiguration.Currency.IsNullOrWhiteSpace())
            {
                configuration.Currency = overrideConfiguration.Currency;
            }

            if (!overrideConfiguration.Language.IsNullOrWhiteSpace())
            {
                configuration.Language = overrideConfiguration.Language;
            }

            if (!overrideConfiguration.AdditionalCallbackParameters.IsNullOrEmpty())
            {
                configuration.AdditionalCallbackParameters = overrideConfiguration.AdditionalCallbackParameters;
            }

            return configuration;
        }

        public string GetUrl(PaymentRequestWithDetailsDto paymentRequest)
        {
            var checkoutUrl = _options.CheckoutUrl.EnsureEndsWith('?');
            var backRefUrl = _paymentGatewayOptions.Gateways[TwoCheckoutConsts.GatewayName].PostPaymentUrl +
                             "?paymentRequestId=" + paymentRequest.Id;

            var hashQueryStringParameters = "PRODS=" + GetTwoCheckoutProductCodes(paymentRequest) + "&";
            hashQueryStringParameters += "QTY=" + GetTwoCheckoutProductCounts(paymentRequest) + "&";
            foreach (var product in paymentRequest.Products)
            {
                var productCode = product.ExtraProperties
                    .GetPaymentRequestProductExtraPropertyConfiguration()
                    .ProductCode;
                
                var price = $"{product.TotalPrice:0.00}";
                hashQueryStringParameters += "PRICES" + productCode + "[" + _options.CurrencyCode + "]=" + price + "&";
            }

            checkoutUrl += hashQueryStringParameters;
            checkoutUrl += "BACK_REF=" + WebUtility.UrlEncode(backRefUrl) + "&";

            if (!_options.CurrencyCode.IsNullOrEmpty())
            {
                checkoutUrl += "CURRENCY=" + _options.CurrencyCode + "&";
            }

            if (!_options.LanguageCode.IsNullOrEmpty())
            {
                checkoutUrl += "LANGUAGES=" + _options.LanguageCode + "&";
            }

            if (_options.TestOrder)
            {
                checkoutUrl += "DOTEST=1&";
            }

            var hash = _twoCheckoutHashCalculator.GetMd5HashForQueryStringParameters(
                hashQueryStringParameters.EnsureEndsWith('&') + "BACK_REF=" + backRefUrl
            );

            checkoutUrl += "PHASH=" + hash + "&";
            checkoutUrl += "CLEAN_CART=1&";
            checkoutUrl += "CARD=1&";
            checkoutUrl += "SECURE_CART=1&";
            checkoutUrl += "REF=" + paymentRequest.Id + "&";

            return checkoutUrl.TrimEnd('&');
        }

        private string GetTwoCheckoutProductCodes(PaymentRequestWithDetailsDto paymentRequest)
        {
            return string.Join(",",
                paymentRequest.Products.Select(
                    product => product.ExtraProperties
                        .GetPaymentRequestProductExtraPropertyConfiguration()
                        .ProductCode
                )
            );
        }

        private string GetTwoCheckoutProductCounts(PaymentRequestWithDetailsDto paymentRequest)
        {
            return string.Join(",", paymentRequest.Products.Select(product => product.Count));
        }
    }
}
