using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.Data;
using Volo.Abp.EventBus.Distributed;
using Volo.Payment.Gateways;
using Volo.Payment.Requests;
using Volo.Payment.Subscription;

namespace Volo.Payment.Requests
{
    public class PaymentRequestAppService : PaymentAppServiceBase, IPaymentRequestAppService
    {
        protected IPaymentRequestRepository PaymentRequestRepository { get; }
        private readonly IServiceProvider _serviceProvider;
        private readonly IDistributedEventBus _distributedEventBus;

        private readonly PaymentOptions _paymentGatewayOptions;
        private readonly IOptions<PaymentOptions> _paymentOptions;

        public PaymentRequestAppService(
            IPaymentRequestRepository paymentRequestRepository,
            IServiceProvider serviceProvider,
            IDistributedEventBus distributedEventBus,
            IOptions<PaymentOptions> paymentGatewayOptions,
            IOptions<PaymentOptions> paymentOptions)
        {
            PaymentRequestRepository = paymentRequestRepository;
            _serviceProvider = serviceProvider;
            _distributedEventBus = distributedEventBus;
            _paymentOptions = paymentOptions;
            _paymentGatewayOptions = paymentGatewayOptions.Value;
        }

        public async Task<PaymentRequestWithDetailsDto> CreateAsync(PaymentRequestCreateDto input)
        {
            var paymentRequest = new PaymentRequest(GuidGenerator.Create());

            foreach (var extraProperty in input.ExtraProperties)
            {
                paymentRequest.SetProperty(extraProperty.Key, extraProperty.Value);
            }

            foreach (var productDto in input.Products)
            {
                paymentRequest.AddProduct(
                    productDto.Code,
                    productDto.Name,
                    productDto.PaymentType,
                    productDto.UnitPrice,
                    productDto.Count,
                    productDto.PlanId,
                    productDto.TotalPrice,
                    productDto.ExtraProperties);
            }

            var insertedPaymentRequest = await PaymentRequestRepository.InsertAsync(paymentRequest, autoSave: true);

            return ObjectMapper.Map<PaymentRequest, PaymentRequestWithDetailsDto>(insertedPaymentRequest);
        }

        public async Task<PaymentRequestWithDetailsDto> GetAsync(Guid id)
        {
            var paymentRequest = await PaymentRequestRepository.GetAsync(id);
            return await GetPaymentRequestWithDetailsDtoAsync(paymentRequest);
        }

        public async Task<PaymentRequestDto> GetByExternalSubscriptionAsync(string externalSubscriptionId)
        {
            var paymentRequest = await PaymentRequestRepository.GetBySubscriptionAsync(externalSubscriptionId);

            return ObjectMapper.Map<PaymentRequest, PaymentRequestDto>(paymentRequest);
        }

        public async Task<PaymentRequestWithDetailsDto> CompleteAsync(CompletePaymentRequestDto input)
        {
            var paymentRequest = await PaymentRequestRepository.GetAsync(input.Id);

            if (paymentRequest.State == PaymentRequestState.Completed)
            {
                return await GetPaymentRequestWithDetailsDtoAsync(paymentRequest);
            }
            
            using (var scope = _serviceProvider.CreateScope())
            {
                var paymentGatewayType = _paymentGatewayOptions.Gateways
                    .Single(pg => pg.Key == input.GateWay)
                    .Value
                    .PaymentGatewayType;

                var paymentGateway = scope.ServiceProvider.GetService(paymentGatewayType) as IPaymentGateway;

                if (paymentGateway == null)
                {
                    throw new Exception("Invalid payment gateway type.");
                }

                if (!paymentGateway.IsValid(paymentRequest, input.ExtraProperties))
                {
                    throw new Exception("Your payment is not valid.");
                }
            }

            foreach (var property in input.ExtraProperties)
            {
                paymentRequest.SetProperty(property.Key, property.Value);
            }

            paymentRequest.Gateway = input.GateWay;
            paymentRequest.Complete();

            if (input.IsSubscription)
            {
                paymentRequest.SetExternalSubscriptionId(input.SubscriptionInfo.ExternalSubscriptionId);
                
                var subscriptionCreatedEto = ObjectMapper.Map<PaymentRequest, SubscriptionCreatedEto>(paymentRequest);
                subscriptionCreatedEto.PeriodEndDate = input.SubscriptionInfo.PeriodEndDate;

                await _distributedEventBus.PublishAsync(subscriptionCreatedEto);
            }

            await PaymentRequestRepository.UpdateAsync(paymentRequest, autoSave: true);

            await _distributedEventBus.PublishAsync(
                new PaymentRequestCompletedEto(
                    id: paymentRequest.Id,
                    gateway: paymentRequest.Gateway,
                    currency: paymentRequest.Currency,
                    products: ObjectMapper.Map<ICollection<PaymentRequestProduct>, List<PaymentRequestProductCompletedEto>>(paymentRequest.Products),
                    extraProperties: paymentRequest.ExtraProperties
                )
            );

            return await GetPaymentRequestWithDetailsDtoAsync(paymentRequest);
        }

        public async Task<PaymentRequestWithDetailsDto> SetCurrencyAsync(SetPaymentRequestCurrencyDto input)
        {
            var paymentRequest = await PaymentRequestRepository.GetAsync(input.Id);
            paymentRequest.Currency = input.Currency;

            return await GetPaymentRequestWithDetailsDtoAsync(paymentRequest);
        }

        [Obsolete("Use IGatewayAppService.GetGatewayConfigurationAsync() instead of this.")]
        public Task<PaymentGatewayConfigurationDictionary> GetGatewayConfigurationAsync()
        {
            return Task.FromResult(_paymentOptions.Value.Gateways);
        }

        private async Task<PaymentRequestWithDetailsDto> GetPaymentRequestWithDetailsDtoAsync(
            PaymentRequest paymentRequest)
        {
            return ObjectMapper.Map<PaymentRequest, PaymentRequestWithDetailsDto>(
                await PaymentRequestRepository.GetAsync(paymentRequest.Id, includeDetails: true)
            );
        }
    }
}
