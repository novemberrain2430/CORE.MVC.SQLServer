using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volo.Payment.Plans
{
    public class PlanAppService : PaymentAppServiceBase, IPlanAppService
    {
        protected IPlanRepository PlanRepository { get; }

        public PlanAppService(IPlanRepository planRepository)
        {
            PlanRepository = planRepository;
        }

        public async Task<GatewayPlanDto> GetGatewayPlanAsync(Guid planId, string gateway)
        {
            var gatewayPlan = await PlanRepository.GetGatewayPlanAsync(planId, gateway);

            return ObjectMapper.Map<GatewayPlan, GatewayPlanDto>(gatewayPlan);
        }

        public async Task<List<PlanDto>> GetPlanListAsync()
        {
            var plans = await PlanRepository.GetListAsync();

            return ObjectMapper.Map<List<Plan>, List<PlanDto>>(plans);
        }
    }
}
