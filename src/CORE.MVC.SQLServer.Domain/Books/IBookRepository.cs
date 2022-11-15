using CORE.MVC.SQLServer.Xamples;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace CORE.MVC.SQLServer.Books
{
    public interface IBookRepository : IRepository<Book, Guid>
    {
        Task<List<Book>> GetListAsync(
           string filterText = null,
           string sorting = null,
           int maxResultCount = int.MaxValue,
           int skipCount = 0,
           CancellationToken cancellationToken = default
       );

        Task<long> GetCountAsync(
            string filterText = null,
            CancellationToken cancellationToken = default);

    }
}
