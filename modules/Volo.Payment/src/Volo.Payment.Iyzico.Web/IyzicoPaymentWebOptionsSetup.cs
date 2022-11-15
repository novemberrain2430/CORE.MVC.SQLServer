using Microsoft.Extensions.Options;
using System;

namespace Volo.Payment.Iyzico
{
    public class IyzicoPaymentWebOptionsSetup : IConfigureOptions<PaymentWebOptions>
    {
        protected IyzicoOptions IyzicoOptions { get; }

        public IyzicoPaymentWebOptionsSetup(IOptions<IyzicoOptions> iyzicoOptions)
        {
            IyzicoOptions = iyzicoOptions.Value;
        }

        public void Configure(PaymentWebOptions options)
        {
            options.Gateways.Add(
                new PaymentGatewayWebConfiguration(
                    IyzicoConsts.GatewayName,
                    IyzicoConsts.PrePaymentUrl,
                    isSubscriptionSupported: false,
                    options.RootUrl.RemovePostFix("/") + IyzicoConsts.PostPaymentUrl,
                    IyzicoOptions.Recommended,
                    IyzicoOptions.ExtraInfos
                )
            );
        }
    }
}
