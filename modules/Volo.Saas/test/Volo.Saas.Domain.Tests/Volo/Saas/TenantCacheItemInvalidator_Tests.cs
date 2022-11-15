using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Caching;
using Volo.Abp.MultiTenancy;
using Volo.Saas.Tenants;
using Xunit;

namespace Volo.Saas
{
    public class TenantCacheItemInvalidator_Tests : SaasDomainTestBase
    {
        private readonly IDistributedCache<TenantCacheItem> _cache;
        private readonly ITenantStore _tenantStore;
        private readonly ITenantRepository _tenantRepository;

        public TenantCacheItemInvalidator_Tests()
        {
            _cache = GetRequiredService<IDistributedCache<TenantCacheItem>>();
            _tenantStore = GetRequiredService<ITenantStore>();
            _tenantRepository = GetRequiredService<ITenantRepository>();
        }

        [Fact]
        public async Task Get_Tenant_Should_Cached()
        {
            var acme = await _tenantRepository.FindByNameAsync("acme");
            acme.ShouldNotBeNull();

            (await _cache.GetAsync(TenantCacheItem.CalculateCacheKey(acme.Id, null))).ShouldBeNull();
            (await _cache.GetAsync(TenantCacheItem.CalculateCacheKey(null, acme.Name))).ShouldBeNull();

            await _tenantStore.FindAsync(acme.Id);
            (await _cache.GetAsync(TenantCacheItem.CalculateCacheKey(acme.Id, null))).ShouldNotBeNull();

            await _tenantStore.FindAsync(acme.Name);
            (await _cache.GetAsync(TenantCacheItem.CalculateCacheKey(null, acme.Name))).ShouldNotBeNull();


            var volosoft = await _tenantRepository.FindByNameAsync("volosoft");
            volosoft.ShouldNotBeNull();

            (await _cache.GetAsync(TenantCacheItem.CalculateCacheKey(volosoft.Id, null))).ShouldBeNull();
            (await _cache.GetAsync(TenantCacheItem.CalculateCacheKey(null, volosoft.Name))).ShouldBeNull();

            await _tenantStore.FindAsync(volosoft.Id);
            (await _cache.GetAsync(TenantCacheItem.CalculateCacheKey(volosoft.Id, null))).ShouldNotBeNull();

            await _tenantStore.FindAsync(volosoft.Name);
            (await _cache.GetAsync(TenantCacheItem.CalculateCacheKey(null, volosoft.Name))).ShouldNotBeNull();
        }

        [Fact]
        public async Task Cache_Should_Invalidator_When_Tenant_Changed()
        {
            var acme = await _tenantRepository.FindByNameAsync("acme");
            acme.ShouldNotBeNull();

            // FindAsync will cache tenant.
            await _tenantStore.FindAsync(acme.Id);
            await _tenantStore.FindAsync(acme.Name);

            (await _cache.GetAsync(TenantCacheItem.CalculateCacheKey(acme.Id, null))).ShouldNotBeNull();
            (await _cache.GetAsync(TenantCacheItem.CalculateCacheKey(null, acme.Name))).ShouldNotBeNull();

            await _tenantRepository.DeleteAsync(acme);

            (await _cache.GetAsync(TenantCacheItem.CalculateCacheKey(acme.Id, null))).ShouldBeNull();
            (await _cache.GetAsync(TenantCacheItem.CalculateCacheKey(null, acme.Name))).ShouldBeNull();


            var volosoft = await _tenantRepository.FindByNameAsync("volosoft");
            volosoft.ShouldNotBeNull();

            // Find will cache tenant.
            await _tenantStore.FindAsync(volosoft.Id);
            await _tenantStore.FindAsync(volosoft.Name);

            (await _cache.GetAsync(TenantCacheItem.CalculateCacheKey(volosoft.Id, null))).ShouldNotBeNull();
            (await _cache.GetAsync(TenantCacheItem.CalculateCacheKey(null, volosoft.Name))).ShouldNotBeNull();

            await _tenantRepository.DeleteAsync(volosoft);

            (await _cache.GetAsync(TenantCacheItem.CalculateCacheKey(volosoft.Id, null))).ShouldBeNull();
            (await _cache.GetAsync(TenantCacheItem.CalculateCacheKey(null, volosoft.Name))).ShouldBeNull();
        }
    }
}
