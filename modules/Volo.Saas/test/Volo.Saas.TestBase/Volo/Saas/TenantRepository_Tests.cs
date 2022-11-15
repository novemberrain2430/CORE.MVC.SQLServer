using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Volo.Saas.Tenants;
using Xunit;

namespace Volo.Saas
{
    public abstract class TenantRepository_Tests<TStartupModule> : SaasTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        public ITenantRepository TenantRepository { get; }

        public  SaasTestData TestData { get; }
        
        protected TenantRepository_Tests()
        {
            TenantRepository = GetRequiredService<ITenantRepository>();
            TestData = GetRequiredService<SaasTestData>();
        }

        [Fact]
        public async Task FindByNameAsync()
        {
            var tenant = await TenantRepository.FindByNameAsync(TestData.FirstTenantName);
            tenant.ShouldNotBeNull();

            tenant = await TenantRepository.FindByNameAsync("undefined-tenant");
            tenant.ShouldBeNull();

            tenant = await TenantRepository.FindByNameAsync(TestData.FirstTenantName, includeDetails: true);
            tenant.ShouldNotBeNull();
            tenant.ConnectionStrings.Count.ShouldBeGreaterThanOrEqualTo(2);
        }

        [Fact]
        public async Task FindAsync()
        {
            var tenantId = (await TenantRepository.FindByNameAsync(TestData.FirstTenantName)).Id;

            var tenant = await TenantRepository.FindAsync(tenantId);
            tenant.ShouldNotBeNull();

            tenant = await TenantRepository.FindAsync(Guid.NewGuid());
            tenant.ShouldBeNull();

            tenant = await TenantRepository.FindAsync(tenantId, includeDetails: true);
            tenant.ShouldNotBeNull();
            tenant.ConnectionStrings.Count.ShouldBeGreaterThanOrEqualTo(2);
        }

        [Fact]
        public async Task GetListAsync()
        {
            var tenants = await TenantRepository.GetListAsync();
            tenants.ShouldContain(t => t.Name == TestData.FirstTenantName);
            tenants.ShouldContain(t => t.Name == TestData.SecondTenantName);
        }

        [Fact]
        public async Task Should_Eager_Load_Tenant_Collections()
        {
            var role = await TenantRepository.FindByNameAsync(TestData.FirstTenantName);
            role.ConnectionStrings.ShouldNotBeNull();
            role.ConnectionStrings.Any().ShouldBeTrue();
        }

        [Fact]
        public async Task GetListWithSeparateConnectionStringAsync()
        {
            var tenants = await TenantRepository.GetListWithSeparateConnectionStringAsync();
            tenants.Count.ShouldBeGreaterThanOrEqualTo(1);
            tenants.ShouldContain(t => t.Name == TestData.FirstTenantName);
        }
    }
}