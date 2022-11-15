using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp;
using Volo.Saas.Host.Dtos;
using Xunit;

namespace Volo.Saas.Host
{
    public class TenantAppService_Tests : SaasHostApplicationTestBase
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantAppService_Tests()
        {
            _tenantAppService = GetRequiredService<ITenantAppService>();
        }

        [Fact]
        public async Task GetAsync()
        {
            var tenantInDb = UsingDbContext(dbContext => dbContext.Tenants.First());
            var tenant = await _tenantAppService.GetAsync(tenantInDb.Id);
            tenant.Name.ShouldBe(tenantInDb.Name);
        }

        [Fact]
        public async Task GetListAsync()
        {
            var result = await _tenantAppService.GetListAsync(new GetTenantsInput());
            result.TotalCount.ShouldBeGreaterThan(0);

            var acme = result.Items.FirstOrDefault(t => t.Name == "acme");
            acme.ShouldNotBeNull();
            acme.HasDefaultConnectionString.ShouldBeTrue();

            var volosoft = result.Items.FirstOrDefault(t => t.Name == "volosoft");
            volosoft.ShouldNotBeNull();
            volosoft.HasDefaultConnectionString.ShouldBeFalse();
        }

        [Fact]
        public async Task GetListAsync_Filtered()
        {
            var result = await _tenantAppService.GetListAsync(new GetTenantsInput { Filter = "volo" });
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldNotContain(t => t.Name == "acme");
            result.Items.ShouldContain(t => t.Name == "volosoft");
        }

        [Fact]
        public async Task GetListAsync_Sorted_Descending_By_Name()
        {
            var result = await _tenantAppService.GetListAsync(new GetTenantsInput { Sorting = "Name DESC" });
            result.TotalCount.ShouldBeGreaterThan(0);
            var tenants = result.Items.ToList();

            tenants.ShouldContain(t => t.Name == "acme");
            tenants.ShouldContain(t => t.Name == "volosoft");

            tenants.FindIndex(t => t.Name == "acme").ShouldBeGreaterThan(tenants.FindIndex(t => t.Name == "volosoft"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            var tenancyName = Guid.NewGuid().ToString("N").ToLowerInvariant();
            var tenant = await _tenantAppService.CreateAsync(new SaasTenantCreateDto
            {
                Name = tenancyName,
                AdminEmailAddress = "admin@admin.com",
                AdminPassword = "123456",
                ActivationState = TenantActivationState.Passive
            });
            tenant.Name.ShouldBe(tenancyName);
            tenant.Id.ShouldNotBe(default(Guid));
            tenant.ActivationState.ShouldBe(TenantActivationState.Passive);
        }

        [Fact]
        public async Task CreateAsync_Should_Not_Allow_Duplicate_Names()
        {
            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _tenantAppService.CreateAsync(new SaasTenantCreateDto { Name = "acme", AdminEmailAddress = "admin@admin.com", AdminPassword = "123456" });
            });
        }

        [Fact]
        public async Task UpdateAsync()
        {
            var acme = UsingDbContext(dbContext => dbContext.Tenants.Single(t => t.Name == "acme"));
            var activationEndDate = DateTime.Now.AddDays(3);

            var result = await _tenantAppService.UpdateAsync(acme.Id, new SaasTenantUpdateDto
            {
                Name = "acme-renamed",
                ActivationState = TenantActivationState.ActiveWithLimitedTime,
                ActivationEndDate = activationEndDate
            });
            result.Id.ShouldBe(acme.Id);
            result.Name.ShouldBe("acme-renamed");
            result.ActivationState.ShouldBe(TenantActivationState.ActiveWithLimitedTime);
            result.ActivationEndDate.ShouldBe(activationEndDate);

            var acmeUpdated = UsingDbContext(dbContext => dbContext.Tenants.Single(t => t.Id == acme.Id));
            acmeUpdated.Name.ShouldBe("acme-renamed");
        }

        [Fact]
        public async Task UpdateAsync_Should_Not_Allow_Duplicate_Names()
        {
            var acme = UsingDbContext(dbContext => dbContext.Tenants.Single(t => t.Name == "acme"));

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _tenantAppService.UpdateAsync(acme.Id, new SaasTenantUpdateDto { Name = "volosoft" });
            });
        }

        [Fact]
        public async Task DeleteAsync()
        {
            var acme = UsingDbContext(dbContext => dbContext.Tenants.Single(t => t.Name == "acme"));

            await _tenantAppService.DeleteAsync(acme.Id);

            UsingDbContext(dbContext =>
            {
                dbContext.Tenants.Any(t => t.Id == acme.Id).ShouldBeFalse();
            });
        }
    }
}
