using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;

namespace Volo.CmsKit.Newsletters
{
    public interface INewsletterRecordRepository : IBasicRepository<NewsletterRecord, Guid>
    {
        Task<List<NewsletterSummaryQueryResultItem>> GetListAsync(
            string preference = null,
            string source = null,
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            CancellationToken cancellationToken = default);

        Task<NewsletterRecord> FindByEmailAddressAsync(
            [NotNull] string emailAddress,
            bool includeDetails = true,
            CancellationToken cancellationToken = default);

        Task<int> GetCountByFilterAsync( 
            [CanBeNull] string preference = null,
            [CanBeNull] string source = null,
            CancellationToken cancellationToken = default);
    }
}