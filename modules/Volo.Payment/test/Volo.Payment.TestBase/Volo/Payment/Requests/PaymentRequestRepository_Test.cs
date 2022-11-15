using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Modularity;
using Xunit;

namespace Volo.Payment.Requests
{
    public abstract class PaymentRequestRepository_Test<TStartupModule> : PaymentTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly PaymentTestData testData;
        private readonly IPaymentRequestRepository paymentRequestRepository;

        public PaymentRequestRepository_Test()
        {
            testData = GetRequiredService<PaymentTestData>();

            paymentRequestRepository = GetRequiredService<IPaymentRequestRepository>();
        }

        [Fact]
        public async Task GetBySubscriptionAsync_ShouldWorkProperly()
        {
            var paymentRequest = await paymentRequestRepository.GetBySubscriptionAsync(testData.PaymentRequest_1_SubscriptionId);

            paymentRequest.ShouldNotBeNull();
            paymentRequest.Id.ShouldBe(testData.PaymentRequest_1_Id);
        }

        [Fact]
        public async Task GetBySubscriptionAsync_ShouldThrowEntityNotFoundException_WithWrongSubscriptionId()
        {
            var nonExistingSubscrtiptionId = "some-wrong-id";
            var exception = await Should.ThrowAsync<EntityNotFoundException>(paymentRequestRepository.GetBySubscriptionAsync(nonExistingSubscrtiptionId));

            exception.ShouldNotBeNull();
            exception.EntityType.ShouldBe(typeof(PaymentRequest));
        }
    }
}
