using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Sample.Books;
using Sample.Shared;

namespace Sample.Web.Pages.Sample.Books
{
    public class IndexModel : AbpPageModel
    {
        public string NameFilter { get; set; }
        public string CodeFilter { get; set; }

        private readonly IBooksAppService _booksAppService;

        public IndexModel(IBooksAppService booksAppService)
        {
            _booksAppService = booksAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}