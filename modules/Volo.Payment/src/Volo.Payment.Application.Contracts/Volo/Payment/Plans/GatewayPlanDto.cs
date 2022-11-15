using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;

namespace Volo.Payment.Plans
{
    public class GatewayPlanDto : EntityDto, IHasExtraProperties
    {
        public Guid PlanId { get; set; }

        public string Gateway { get; set; }

        public string ExternalId { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; }

        public GatewayPlanDto()
        {
            ExtraProperties = new();
        }
    }
}
