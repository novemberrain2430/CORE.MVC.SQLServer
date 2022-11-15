using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CORE.MVC.SQLServer.Xamples;

namespace CORE.MVC.SQLServer.Web.Pages.Xamples
{
    public class CreateModalModel : SQLServerPageModel
    {
        [BindProperty]
        public XampleCreateDto Xample { get; set; }

        private readonly IXamplesAppService _xamplesAppService;

        public CreateModalModel(IXamplesAppService xamplesAppService)
        {
            _xamplesAppService = xamplesAppService;
        }

        public async Task OnGetAsync()
        {
            Xample = new XampleCreateDto();
            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _xamplesAppService.CreateAsync(Xample);
            return NoContent();
        }
    }
}