using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Volo.Saas.Tenants
{
    public class TenantManager : DomainService, ITenantManager
    {
        protected ITenantRepository TenantRepository { get; }

        public TenantManager(ITenantRepository tenantRepository)
        {
            TenantRepository = tenantRepository;
        }

        public virtual async Task<Tenant> CreateAsync(string name, Guid? editionId = null)
        {
            Check.NotNull(name, nameof(name));

            await ValidateNameAsync(name);
            return new Tenant(GuidGenerator.Create(), name, editionId);
        }

        public virtual async Task ChangeNameAsync(Tenant tenant, string name)
        {
            Check.NotNull(tenant, nameof(tenant));
            Check.NotNull(name, nameof(name));

            await ValidateNameAsync(name, tenant.Id);
            tenant.SetName(name);
        }

        protected virtual async Task ValidateNameAsync(string name, Guid? expectedId = null)
        {
            var tenant = await TenantRepository.FindByNameAsync(name);
            if (tenant != null && tenant.Id != expectedId)
            {
                throw new BusinessException("Volo.Saas:DuplicateTenantName").WithData("Name", name);
            }
        }

        public virtual Task<bool> IsActiveAsync(Tenant tenant)
        {
            return Task.FromResult(tenant.ActivationState switch
            {
                TenantActivationState.Active => true,
                TenantActivationState.Passive => false,
                TenantActivationState.ActiveWithLimitedTime => tenant.ActivationEndDate >= Clock.Now,
                _ => false
            });
        }

        [Obsolete("Use IsActiveAsync method.")]
        public bool IsActive(Tenant tenant)
        {
            return tenant.ActivationState switch
            {
                TenantActivationState.Active => true,
                TenantActivationState.Passive => false,
                TenantActivationState.ActiveWithLimitedTime => tenant.ActivationEndDate >= Clock.Now,
                _ => false
            };
        }
    }
}
