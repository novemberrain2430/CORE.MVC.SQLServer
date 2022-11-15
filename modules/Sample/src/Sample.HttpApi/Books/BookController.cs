using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Sample.Books;

namespace Sample.Books
{
    [RemoteService(Name = "Sample")]
    [Area("sample")]
    [ControllerName("Book")]
    [Route("api/sample/books")]
    public class BookController : AbpController, IBooksAppService
    {
        private readonly IBooksAppService _booksAppService;

        public BookController(IBooksAppService booksAppService)
        {
            _booksAppService = booksAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<BookDto>> GetListAsync(GetBooksInput input)
        {
            return _booksAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<BookDto> GetAsync(Guid id)
        {
            return _booksAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<BookDto> CreateAsync(BookCreateDto input)
        {
            return _booksAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<BookDto> UpdateAsync(Guid id, BookUpdateDto input)
        {
            return _booksAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _booksAppService.DeleteAsync(id);
        }
    }
}