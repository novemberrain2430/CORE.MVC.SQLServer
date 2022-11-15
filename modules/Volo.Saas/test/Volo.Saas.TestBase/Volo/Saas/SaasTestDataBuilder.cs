using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Threading;
using Volo.Payment.Plans;
using Volo.Saas.Editions;
using Volo.Saas.Tenants;

namespace Volo.Saas
{
    public class SaasTestDataBuilder : ITransientDependency
    {
        private readonly SaasTestData _saasTestData;
        private readonly ITenantRepository _tenantRepository;
        private readonly IEditionRepository _editionRepository;
        private readonly ITenantManager _tenantManager;
        private readonly IPlanRepository _planRepository;

        public SaasTestDataBuilder(
            SaasTestData saasTestData,
            ITenantRepository tenantRepository,
            IEditionRepository editionRepository,
            ITenantManager tenantManager, 
            IPlanRepository planRepository)
        {
            _saasTestData = saasTestData;
            _tenantRepository = tenantRepository;
            _editionRepository = editionRepository;
            _tenantManager = tenantManager;
            _planRepository = planRepository;
        }

        public void Build()
        {
            AsyncHelper.RunSync(AddEditionsAsync);
            AsyncHelper.RunSync(AddTenantsAsync);
            AsyncHelper.RunSync(AddPlansAsync);
        }

        protected virtual async Task AddEditionsAsync()
        {
            await _editionRepository.InsertAsync(new Edition(_saasTestData.FirstEditionId, "FirstEdition")
            {
                PlanId = _saasTestData.FirstPlanId
            });

            await _editionRepository.InsertAsync(new Edition(_saasTestData.SecondEditionId, "SecondEdition"));
        }

        protected virtual async Task AddTenantsAsync()
        {
            var acme = await _tenantManager.CreateAsync(_saasTestData.FirstTenantName);
            _saasTestData.FirstTenantId = acme.Id;
            acme.ConnectionStrings.Add(new TenantConnectionString(acme.Id, ConnectionStrings.DefaultConnectionStringName, "DefaultConnString-Value"));
            acme.ConnectionStrings.Add(new TenantConnectionString(acme.Id, "MyConnString", "MyConnString-Value"));
            await _tenantRepository.InsertAsync(acme);

            var volosoft = await _tenantManager.CreateAsync(_saasTestData.SecondTenantName);
            _saasTestData.SecondTenantId = volosoft.Id;
            volosoft.EditionId = (await _editionRepository.FindAsync(_saasTestData.FirstEditionId)).Id;
            await _tenantRepository.InsertAsync(volosoft);
        }

        protected virtual async Task AddPlansAsync()
        {
            await _planRepository.InsertAsync(new Plan(_saasTestData.FirstPlanId, _saasTestData.FirstPlanName));
        }
    }
}
