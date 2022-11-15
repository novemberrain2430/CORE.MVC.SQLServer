using Volo.Abp.Application.Dtos;

namespace Volo.Payment.Admin.Plans
{
    public class GatewayPlanGetListInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}