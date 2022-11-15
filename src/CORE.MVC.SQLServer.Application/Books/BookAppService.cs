using CORE.MVC.SQLServer.Samples;
using CORE.MVC.SQLServer.Xamples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace CORE.MVC.SQLServer.Books
{
    //public class BookAppService :
    //     ApplicationService,IBookAppService //implement the IBookAppService
    //{
    //    private readonly IBookRepository _bookRepository;
    //    public BookAppService(IBookRepository bookRepository)
    //    {
    //        _bookRepository = bookRepository;
    //    }

    //    public virtual async Task<BookDto> CreateAsync(CreateUpdateBookDto input)
    //    {
    //        var book = ObjectMapper.Map<CreateUpdateBookDto, Book>(input);
    //        book.TenantId = CurrentTenant.Id;
    //        book = await _bookRepository.InsertAsync(book, autoSave: true);
    //        return ObjectMapper.Map<Book, BookDto>(book);
    //    }

    //    public virtual async Task DeleteAsync(Guid id)
    //    {
    //        await _bookRepository.DeleteAsync(id);
    //    }

    //    public virtual async Task<BookDto> GetAsync(Guid id)
    //    {
    //        return ObjectMapper.Map<Book, BookDto>(await _bookRepository.GetAsync(id));
    //    }

    //    public virtual async Task<PagedResultDto<BookDto>> GetListAsync(GetBooksInput input)
    //    {
    //        var totalCount = await _bookRepository.GetCountAsync(input.FilterText);
    //        var items = await _bookRepository.GetListAsync(input.FilterText);

    //        return new PagedResultDto<BookDto>
    //        {
    //            TotalCount = totalCount,
    //            Items = ObjectMapper.Map<List<Book>, List<BookDto>>(items)
    //        };
    //    }

    //    public virtual async Task<BookDto> UpdateAsync(Guid id, CreateUpdateBookDto input)
    //    {
    //        var book = await _bookRepository.GetAsync(id);
    //        ObjectMapper.Map(input, book);
    //        book = await _bookRepository.UpdateAsync(book, autoSave: true);
    //        return ObjectMapper.Map<Book, BookDto>(book);
    //    }
    //}
    public class BookAppService :
        CrudAppService<
            Book, //The Book entity
            BookDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateBookDto>, //Used to create/update a book
        IBookAppService //implement the IBookAppService
    {
        public BookAppService(IRepository<Book, Guid> repository)
            : base(repository)
        {

        }
    }
}
