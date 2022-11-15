using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Payment.Plans;
using Volo.Saas.Host.Dtos;

namespace Volo.Saas.Host
{
    public interface IEditionAppService : ICrudAppService<EditionDto, Guid, GetEditionsInput, EditionCreateDto, EditionUpdateDto>
    {
        Task<GetEditionUsageStatisticsResult> GetUsageStatistics();
        Task<List<PlanDto>> GetPlanLookupAsync();
    }
}
