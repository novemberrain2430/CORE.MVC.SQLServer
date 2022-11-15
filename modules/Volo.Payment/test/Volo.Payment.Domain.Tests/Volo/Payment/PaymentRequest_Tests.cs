using System;
using Shouldly;
using Volo.Abp.Guids;
using Volo.Payment.Requests;
using Xunit;

namespace Volo.Payment
{
    public class PaymentRequest_Tests : PaymentTestBase<AbpPaymentDomainModule>
    {
        private readonly IGuidGenerator _generator;

        public PaymentRequest_Tests()
        {
            _generator = GetRequiredService<IGuidGenerator>();
        }

        // Complete
        // Failed
        [Fact]
        public void AddProduct()
        {
            PaymentRequest paymentRequest = new PaymentRequest(_generator.Create());
            paymentRequest.AddProduct("0001", "Test product", PaymentType.OneTime, unitPrice: 10, count: 5, totalPrice: 50);

            paymentRequest.Products.Count.ShouldBe(1);
        }

        [Fact]
        public void Complete_Succeed()
        {
            PaymentRequest paymentRequest = new PaymentRequest(_generator.Create());
            paymentRequest.AddProduct("0001", "Test product", PaymentType.OneTime, unitPrice: 10, count: 5, totalPrice: 50);

            paymentRequest.Complete();
            paymentRequest.State.ShouldBe(PaymentRequestState.Completed);
        }

        [Fact]
        public void Complete_Failed()
        {
            PaymentRequest paymentRequest = new PaymentRequest(_generator.Create());
            paymentRequest.AddProduct("0001", "Test product", PaymentType.OneTime, unitPrice: 10, count: 5, totalPrice: 50);

            paymentRequest.Failed("Unknown Gateway");

            Assert.Throws<ApplicationException>(() => paymentRequest.Complete());
        }

        [Fact]
        public void Failed_Succeed()
        {
            PaymentRequest paymentRequest = new PaymentRequest(_generator.Create());
            paymentRequest.AddProduct("0001", "Test product",  PaymentType.OneTime, unitPrice: 10, count: 5, totalPrice: 50);

            paymentRequest.Failed("Unknown Gateway");

            paymentRequest.State.ShouldBe(PaymentRequestState.Failed);
            paymentRequest.FailReason.ShouldBe("Unknown Gateway");
        }

        [Fact]
        public void Failed_Failed()
        {
            PaymentRequest paymentRequest = new PaymentRequest(_generator.Create());
            paymentRequest.AddProduct("0001", "Test product",  PaymentType.OneTime, unitPrice: 10, count: 5, totalPrice: 50);

            paymentRequest.Complete();

            Assert.Throws<ApplicationException>(() => paymentRequest.Failed());
        }
    }
}
