using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace CORE.MVC.SQLServer.Samples
{
    public interface ISampleRepository : IRepository<Sample, Guid>
    {
        Task<List<Sample>> GetListAsync(
            string filterText = null,
            string name = null,
            DateTime? date1Min = null,
            DateTime? date1Max = null,
            int? yearMin = null,
            int? yearMax = null,
            string code = null,
            string email = null,
            bool? isConfirm = null,
            Guid? userId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            DateTime? date1Min = null,
            DateTime? date1Max = null,
            int? yearMin = null,
            int? yearMax = null,
            string code = null,
            string email = null,
            bool? isConfirm = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default);
    }
}