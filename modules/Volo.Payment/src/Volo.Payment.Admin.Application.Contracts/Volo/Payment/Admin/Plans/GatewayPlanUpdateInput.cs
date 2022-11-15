using System;
using System.ComponentModel.DataAnnotations;

namespace Volo.Payment.Admin.Plans
{
    [Serializable]
    public class GatewayPlanUpdateInput
    {
        [Required]
        public string ExternalId { get; set; }
    }
}
