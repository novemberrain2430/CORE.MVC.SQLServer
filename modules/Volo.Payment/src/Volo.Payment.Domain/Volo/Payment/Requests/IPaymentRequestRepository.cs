using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;

namespace Volo.Payment.Requests
{
    public interface IPaymentRequestRepository : IBasicRepository<PaymentRequest, Guid>
    {
        Task<List<PaymentRequest>> GetListAsync(
            [NotNull] DateTime startDate,
            [NotNull] DateTime endDate,
            CancellationToken cancellationToken = default
        );

        Task<PaymentRequest> GetBySubscriptionAsync(string externalSubscriptionId, CancellationToken cancellationToken = default);
    }
}
