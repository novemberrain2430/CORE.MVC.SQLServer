using System;
using Volo.Abp.Application.Dtos;

namespace Volo.Saas.Host.Dtos
{
    public class EditionDto : ExtensibleEntityDto<Guid>
    {
        public string DisplayName { get; set; }

        public Guid? PlanId { get; set; }

        public string PlanName { get; set; }
    }
}
