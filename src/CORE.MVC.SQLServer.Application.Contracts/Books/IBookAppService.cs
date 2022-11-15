using CORE.MVC.SQLServer.Xamples;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CORE.MVC.SQLServer.Books
{
    //public interface IBookAppService :
    //     IApplicationService
    //{
    //    Task<PagedResultDto<BookDto>> GetListAsync(GetBooksInput input);

    //    Task<BookDto> GetAsync(Guid id);

    //    Task DeleteAsync(Guid id);

    //    Task<BookDto> CreateAsync(CreateUpdateBookDto input);

    //    Task<BookDto> UpdateAsync(Guid id, CreateUpdateBookDto input);
    //}
      public interface IBookAppService :
        ICrudAppService< //Defines CRUD methods
            BookDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateBookDto> //Used to create/update a book
    {

    }
}
