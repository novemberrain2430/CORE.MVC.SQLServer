using System;
using System.ComponentModel.DataAnnotations;

namespace Volo.Payment.Admin.Plans
{
    [Serializable]
    public class PlanCreateUpdateInput
    {
        [Required]
        public string Name { get; set; }
    }
}
