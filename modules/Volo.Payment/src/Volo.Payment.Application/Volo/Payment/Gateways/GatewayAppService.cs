using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace Volo.Payment.Gateways
{
    public class GatewayAppService : PaymentAppServiceBase, IGatewayAppService
    {
        protected IOptions<PaymentOptions> PaymentOptions { get; }
        protected IStringLocalizerFactory StringLocalizerFactory { get; }

        public GatewayAppService(
            IOptions<PaymentOptions> paymentOptions,
            IStringLocalizerFactory stringLocalizerFactory)
        {
            PaymentOptions = paymentOptions;
            StringLocalizerFactory = stringLocalizerFactory;
        }

        public virtual Task<List<GatewayDto>> GetGatewayConfigurationAsync()
        {
            return Task.FromResult(
                PaymentOptions.Value.Gateways
                    .Select(g => new GatewayDto
                    {
                        Name = g.Value.Name, 
                        DisplayName = g.Value.DisplayName.Localize(StringLocalizerFactory)
                    })
                    .ToList()
                );
        }

        public virtual Task<List<GatewayDto>> GetSubscriptionSupportedGatewaysAsync()
        {
            var subscriptionSupportedGateways = PaymentOptions.Value.Gateways
                .Where(g => g.Value.IsSubscriptionSupported)
                .Select(g => new GatewayDto
                {
                    Name = g.Value.Name, 
                    DisplayName = g.Value.DisplayName.Localize(StringLocalizerFactory)
                })
                .ToList();

            return Task.FromResult(subscriptionSupportedGateways);
        }
    }
}
