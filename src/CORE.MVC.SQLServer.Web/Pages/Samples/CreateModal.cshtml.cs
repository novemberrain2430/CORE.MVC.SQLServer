using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CORE.MVC.SQLServer.Samples;

namespace CORE.MVC.SQLServer.Web.Pages.Samples
{
    public class CreateModalModel : SQLServerPageModel
    {
        [BindProperty]
        public SampleCreateDto Sample { get; set; }

        private readonly ISamplesAppService _samplesAppService;

        public CreateModalModel(ISamplesAppService samplesAppService)
        {
            _samplesAppService = samplesAppService;
        }

        public async Task OnGetAsync()
        {
            Sample = new SampleCreateDto();
            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _samplesAppService.CreateAsync(Sample);
            return NoContent();
        }
    }
}