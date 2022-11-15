using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Volo.Saas.Editions
{
    public class EditionManager : DomainService
    {
        protected IEditionRepository EditionRepository { get; }

        public EditionManager(IEditionRepository editionRepository)
        {
            EditionRepository = editionRepository;
        }

        public virtual async Task<Edition> GetEditionForSubscriptionAsync(Guid id)
        {
            var edition = await EditionRepository.GetAsync(id);

            await CheckEditionForSubscriptionAsync(edition);

            return edition;
        }

        public virtual Task CheckEditionForSubscriptionAsync(Edition edition)
        {
            if (!edition.PlanId.HasValue)
            {
                throw new EditionDoesntHavePlanException(edition.Id);
            }
            return Task.CompletedTask;
        }
    }
}
