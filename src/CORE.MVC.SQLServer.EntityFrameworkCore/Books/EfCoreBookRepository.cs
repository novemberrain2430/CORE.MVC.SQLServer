using CORE.MVC.SQLServer.EntityFrameworkCore;
using CORE.MVC.SQLServer.Xamples;
using Elasticsearch.Net;
using Microsoft.EntityFrameworkCore;
using Sample.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using static IdentityServer4.Models.IdentityResources;

namespace CORE.MVC.SQLServer.Books
{
    public class EfCoreBookRepository : EfCoreRepository<SQLServerDbContext, Book, Guid>, IBookRepository
    {
        public EfCoreBookRepository(IDbContextProvider<SQLServerDbContext> dbContextProvider)
             : base(dbContextProvider)
        {

        }
        public async Task<long> GetCountAsync(string filterText = null, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Book>> GetListAsync(
             string filterText = null,
             string sorting = null,
             int maxResultCount = int.MaxValue,
             int skipCount = 0,
             CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText);
            //query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? BookConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }
        protected virtual IQueryable<Book> ApplyFilter(
            IQueryable<Book> query,
            string filterText
           )
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.Contains(filterText));
        }
    }
}
