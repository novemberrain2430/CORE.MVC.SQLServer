using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;

namespace Volo.Payment.Gateways
{

    [RemoteService(Name = AbpPaymentCommonRemoteServiceConsts.RemoteServiceName)]
    [Area("payment")]
    [Route("api/payment/gateways")]
    public class GatewayController : PaymentCommonController, IGatewayAppService
    {
        protected IGatewayAppService GatewayAppService { get; }

        public GatewayController(IGatewayAppService gatewayAppService)
        {
            GatewayAppService = gatewayAppService;
        }

        [HttpGet]
        public Task<List<GatewayDto>> GetGatewayConfigurationAsync()
        {
            return GatewayAppService.GetGatewayConfigurationAsync();
        }

        [HttpGet]
        [Route("subscription-supported")]
        public Task<List<GatewayDto>> GetSubscriptionSupportedGatewaysAsync()
        {
            return GatewayAppService.GetSubscriptionSupportedGatewaysAsync();
        }
    }
}
