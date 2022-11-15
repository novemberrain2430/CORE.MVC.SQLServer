using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;
using Volo.Payment.Plans;
using Volo.Saas.Editions;
using Volo.Saas.Host.Dtos;
using Volo.Saas.Tenants;

namespace Volo.Saas.Host
{
    [Authorize(SaasHostPermissions.Editions.Default)]
    public class EditionAppService : SaasHostAppServiceBase, IEditionAppService
    {
        protected IEditionRepository EditionRepository { get; }
        protected ITenantRepository TenantRepository { get; }
        protected IPlanRepository PlanRepository { get; }

        public EditionAppService(
            IEditionRepository editionRepository,
            ITenantRepository tenantRepository,
            IPlanRepository planRepository)
        {
            EditionRepository = editionRepository;
            TenantRepository = tenantRepository;
            PlanRepository = planRepository;
        }

        public virtual async Task<EditionDto> GetAsync(Guid id)
        {
            var edition = await EditionRepository.GetAsync(id);

            var editionDto = ObjectMapper.Map<Edition, EditionDto>(edition);

            if (edition.PlanId.HasValue)
            {
                var plan = await PlanRepository.FindAsync(edition.PlanId.Value);
                editionDto.PlanName = plan?.Name;
            }

            return editionDto;
        }

        public virtual async Task<PagedResultDto<EditionDto>> GetListAsync(GetEditionsInput input)
        {
            var count = await EditionRepository.GetCountAsync(input.Filter);
            var editions = await EditionRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);

            var planIds = editions
                .Where(x => x.PlanId.HasValue)
                .Select(x => x.PlanId.Value)
                .Distinct()
                .ToArray();

            var plans = await PlanRepository.GetManyAsync(planIds);

            var editionDtos = ObjectMapper.Map<List<Edition>, List<EditionDto>>(editions);

            foreach (var editionDto in editionDtos.Where(x => x.PlanId.HasValue))
            {
                var plan = plans.FirstOrDefault(x => x.Id == editionDto.PlanId.Value);

                editionDto.PlanName = plan?.Name;
            }

            return new PagedResultDto<EditionDto>(
                count,
                editionDtos
            );
        }

        [Authorize(SaasHostPermissions.Editions.Create)]
        public virtual async Task<EditionDto> CreateAsync(EditionCreateDto input)
        {
            var edition = new Edition(GuidGenerator.Create(), input.DisplayName)
            {
                PlanId = input.PlanId
            };

            input.MapExtraPropertiesTo(edition);

            await EditionRepository.InsertAsync(edition);

            return ObjectMapper.Map<Edition, EditionDto>(edition);
        }

        [Authorize(SaasHostPermissions.Editions.Update)]
        public virtual async Task<EditionDto> UpdateAsync(Guid id, EditionUpdateDto input)
        {
            var edition = await EditionRepository.GetAsync(id);

            edition.SetDisplayName(input.DisplayName);
            edition.PlanId = input.PlanId;
            input.MapExtraPropertiesTo(edition);

            var updatedEdition = await EditionRepository.UpdateAsync(edition);

            return ObjectMapper.Map<Edition, EditionDto>(updatedEdition);
        }

        [Authorize(SaasHostPermissions.Editions.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await EditionRepository.DeleteAsync(id);
        }

        public virtual async Task<GetEditionUsageStatisticsResult> GetUsageStatistics()
        {
            var editions = await EditionRepository.GetListAsync();
            var tenants = await TenantRepository.GetListAsync();

            var result = tenants.GroupBy(info => info.GetActiveEditionId())
                .Select(group => new
                {
                    EditionId = group.Key,
                    Count = group.Count()
                });

            var data = new Dictionary<string, int>();

            foreach (var element in result)
            {
                var displayName = editions.FirstOrDefault(e => e.Id == element.EditionId)?.DisplayName;

                if (displayName != null)
                {
                    data.Add(displayName, element.Count);
                }
            }

            return new GetEditionUsageStatisticsResult()
            {
                Data = data
            };
        }

        public virtual async Task<List<PlanDto>> GetPlanLookupAsync()
        {
            var plans = await PlanRepository.GetListAsync(includeDetails: false);

            return ObjectMapper.Map<List<Plan>, List<PlanDto>>(plans);
        }
    }
}
