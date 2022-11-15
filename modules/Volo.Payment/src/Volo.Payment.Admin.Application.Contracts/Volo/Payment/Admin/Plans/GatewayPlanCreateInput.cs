using System.ComponentModel.DataAnnotations;
using Volo.Abp.Data;

namespace Volo.Payment.Admin.Plans
{
    public class GatewayPlanCreateInput : IHasExtraProperties
    {
        [Required]
        public string Gateway { get; set; }

        [Required]
        public string ExternalId { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; }
    }
}
