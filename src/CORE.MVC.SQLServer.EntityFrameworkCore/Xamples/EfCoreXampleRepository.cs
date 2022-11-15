using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using CORE.MVC.SQLServer.EntityFrameworkCore;

namespace CORE.MVC.SQLServer.Xamples
{
    public class EfCoreXampleRepository : EfCoreRepository<SQLServerDbContext, Xample, Guid>, IXampleRepository
    {
        public EfCoreXampleRepository(IDbContextProvider<SQLServerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Xample>> GetListAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, date1Min, date1Max, yearMin, yearMax, code, email, isConfirm, userId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? XampleConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, date1Min, date1Max, yearMin, yearMax, code, email, isConfirm, userId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Xample> ApplyFilter(
            IQueryable<Xample> query,
            string filterText,
            string name = null,
            DateTime? date1Min = null,
            DateTime? date1Max = null,
            int? yearMin = null,
            int? yearMax = null,
            string code = null,
            string email = null,
            bool? isConfirm = null,
            Guid? userId = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.Contains(filterText) || e.Code.Contains(filterText) || e.Email.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(date1Min.HasValue, e => e.Date1 >= date1Min.Value)
                    .WhereIf(date1Max.HasValue, e => e.Date1 <= date1Max.Value)
                    .WhereIf(yearMin.HasValue, e => e.Year >= yearMin.Value)
                    .WhereIf(yearMax.HasValue, e => e.Year <= yearMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(email), e => e.Email.Contains(email))
                    .WhereIf(isConfirm.HasValue, e => e.IsConfirm == isConfirm)
                    .WhereIf(userId.HasValue, e => e.UserId == userId);
        }
    }
}