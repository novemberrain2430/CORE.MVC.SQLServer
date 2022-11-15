using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using CORE.MVC.SQLServer.Samples;

namespace CORE.MVC.SQLServer.Web.Pages.Samples
{
    public class EditModalModel : SQLServerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public SampleUpdateDto Sample { get; set; }

        private readonly ISamplesAppService _samplesAppService;

        public EditModalModel(ISamplesAppService samplesAppService)
        {
            _samplesAppService = samplesAppService;
        }

        public async Task OnGetAsync()
        {
            var sample = await _samplesAppService.GetAsync(Id);
            Sample = ObjectMapper.Map<SampleDto, SampleUpdateDto>(sample);

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _samplesAppService.UpdateAsync(Id, Sample);
            return NoContent();
        }
    }
}