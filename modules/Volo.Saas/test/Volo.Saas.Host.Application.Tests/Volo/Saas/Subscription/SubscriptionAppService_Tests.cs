using Shouldly;
using System.Threading.Tasks;
using Volo.Saas.Editions;
using Volo.Saas.Host;
using Xunit;

namespace Volo.Saas.Subscription
{
    public class SubscriptionAppService_Tests : SaasHostApplicationTestBase
    {
        private readonly SaasTestData saasTestData;
        private readonly ISubscriptionAppService subscriptionAppService;

        public SubscriptionAppService_Tests()
        {
            saasTestData = GetRequiredService<SaasTestData>();
            subscriptionAppService = GetRequiredService<ISubscriptionAppService>();
        }

        [Fact]
        public async Task CreateSubscriptionAsync_ShouldWorkProperly()
        {
            var paymentRequest = await subscriptionAppService.CreateSubscriptionAsync(saasTestData.FirstEditionId, saasTestData.FirstTenantId);

            paymentRequest.ShouldNotBeNull();
        }

        [Fact]
        public async Task CreateSubscriptionAsync_ShouldThrowException_ForEditionWithoutPlanId()
        {
            var exception = await Should.ThrowAsync<EditionDoesntHavePlanException>(
                subscriptionAppService.CreateSubscriptionAsync(saasTestData.SecondEditionId, saasTestData.FirstTenantId));

            exception.ShouldNotBeNull();
            exception.EditionId.ShouldBe(saasTestData.SecondEditionId);
        }
    }
}
