﻿using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Volo.Payment.Requests
{
    public interface IPaymentRequestAppService : IApplicationService
    {
        Task<PaymentRequestWithDetailsDto> CreateAsync(PaymentRequestCreateDto input);

        Task<PaymentRequestWithDetailsDto> GetAsync(Guid id);

        Task<PaymentRequestDto> GetByExternalSubscriptionAsync(string externalSubscriptionId);

        Task<PaymentRequestWithDetailsDto> CompleteAsync(CompletePaymentRequestDto input);

        Task<PaymentRequestWithDetailsDto> SetCurrencyAsync(SetPaymentRequestCurrencyDto input);

        [Obsolete("Use IGatewayAppService.GetGatewayConfigurationAsync() instead of this.")]
        Task<PaymentGatewayConfigurationDictionary> GetGatewayConfigurationAsync();
    }
}
