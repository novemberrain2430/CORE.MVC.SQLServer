using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Saas.Tenants;

namespace Volo.Saas.EntityFrameworkCore
{
    public class EfCoreTenantRepository : EfCoreRepository<ISaasDbContext, Tenant, Guid>, ITenantRepository
    {
        public EfCoreTenantRepository(IDbContextProvider<ISaasDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<Tenant> FindByIdAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken: cancellationToken);
        }

        public virtual async Task<Tenant> FindByNameAsync(
            string name,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .FirstOrDefaultAsync(t => t.Name == name, GetCancellationToken(cancellationToken));
        }

        [Obsolete("Use FindByIdAsync method.")]
        public Tenant FindById(Guid id, bool includeDetails = true)
        {
            return DbSet
                .IncludeDetails(includeDetails)
                .FirstOrDefault(t => t.Id == id);
        }

        [Obsolete("Use FindByNameAsync method.")]
        public Tenant FindByName(string name, bool includeDetails = true)
        {
            return DbSet
                .IncludeDetails(includeDetails)
                .FirstOrDefault(t => t.Name == name);
        }

        public virtual async Task<List<Tenant>> GetListAsync(
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(filter)
                )
                .OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(Tenant.Name) : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Tenant>> GetListWithSeparateConnectionStringAsync(
            string connectionName = ConnectionStrings.DefaultConnectionStringName,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .Where(u => u.ConnectionStrings.Any(c => c.Name == connectionName && c.Value != null))
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(string filter = null, CancellationToken cancellationToken = default)
        {
            return await this
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(filter)
                ).CountAsync(cancellationToken: cancellationToken);
        }

        [Obsolete("Use WithDetailsAsync method.")]
        public override IQueryable<Tenant> WithDetails()
        {
            return GetQueryable().IncludeDetails();
        }

        public override async Task<IQueryable<Tenant>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }
    }
}
