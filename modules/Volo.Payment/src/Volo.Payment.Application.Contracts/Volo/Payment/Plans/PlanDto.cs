using System;
using Volo.Abp.Application.Dtos;

namespace Volo.Payment.Plans
{
    [Serializable]
    public class PlanDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
