using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Sample.Books;

namespace Sample.Web.Pages.Sample.Books
{
    public class EditModalModel : SamplePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public BookUpdateDto Book { get; set; }

        private readonly IBooksAppService _booksAppService;

        public EditModalModel(IBooksAppService booksAppService)
        {
            _booksAppService = booksAppService;
        }

        public async Task OnGetAsync()
        {
            var book = await _booksAppService.GetAsync(Id);
            Book = ObjectMapper.Map<BookDto, BookUpdateDto>(book);

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _booksAppService.UpdateAsync(Id, Book);
            return NoContent();
        }
    }
}