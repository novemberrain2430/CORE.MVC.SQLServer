using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample.Books;

namespace Sample.Web.Pages.Sample.Books
{
    public class CreateModalModel : SamplePageModel
    {
        [BindProperty]
        public BookCreateDto Book { get; set; }

        private readonly IBooksAppService _booksAppService;

        public CreateModalModel(IBooksAppService booksAppService)
        {
            _booksAppService = booksAppService;
        }

        public async Task OnGetAsync()
        {
            Book = new BookCreateDto();
            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _booksAppService.CreateAsync(Book);
            return NoContent();
        }
    }
}