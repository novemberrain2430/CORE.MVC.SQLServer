using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Volo.Payment.Plans
{
    [RemoteService(Name = AbpPaymentCommonRemoteServiceConsts.RemoteServiceName)]
    [Area("payment")]
    [Route("api/payment/plans")]
    public class PlanController : PaymentCommonController, IPlanAppService
    {
        protected IPlanAppService PlanAppService { get; }

        public PlanController(IPlanAppService planAppService)
        {
            PlanAppService = planAppService;
        }

        [HttpGet]
        [Route("{planId}/{gateway}")]
        public Task<GatewayPlanDto> GetGatewayPlanAsync(Guid planId, string gateway)
        {
            return PlanAppService.GetGatewayPlanAsync(planId, gateway);
        }

        [HttpGet]
        public Task<List<PlanDto>> GetPlanListAsync()
        {
            return PlanAppService.GetPlanListAsync();
        }
    }
}
