using System;
using Volo.Abp.Application.Dtos;

namespace Volo.Saas.Host.Dtos
{
    public class SaasTenantDto : ExtensibleEntityDto<Guid>
    {
        public string Name { get; set; }

        public Guid? EditionId { get; set; }

        public DateTime? EditionEndDateUtc { get; set; }

        public string EditionName { get; set; }

        public bool HasDefaultConnectionString { get; set; }

        public TenantActivationState ActivationState { get; set; }

        public DateTime? ActivationEndDate { get; set; }
    }
}
