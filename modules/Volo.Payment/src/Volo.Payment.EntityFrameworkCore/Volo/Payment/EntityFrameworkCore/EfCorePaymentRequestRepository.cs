using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Payment.Requests;

namespace Volo.Payment.EntityFrameworkCore
{
    public class EfCorePaymentRequestRepository : EfCoreRepository<IPaymentDbContext, PaymentRequest, Guid>, IPaymentRequestRepository
    {
        public EfCorePaymentRequestRepository(IDbContextProvider<IPaymentDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<PaymentRequest>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }

        public async Task<List<PaymentRequest>> GetListAsync(
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken = default
        )
        {
            return await (await GetDbSetAsync())
                .Where(x => x.State != PaymentRequestState.Completed && x.CreationTime >= startDate &&
                            x.CreationTime <= endDate && x.CreatorId.HasValue)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<PaymentRequest> GetBySubscriptionAsync(string externalSubscriptionId, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .FirstOrDefaultAsync(x => x.ExternalSubscriptionId == externalSubscriptionId, GetCancellationToken(cancellationToken))
                ?? throw new EntityNotFoundException(typeof(PaymentRequest)); ;
        }
    }
}
