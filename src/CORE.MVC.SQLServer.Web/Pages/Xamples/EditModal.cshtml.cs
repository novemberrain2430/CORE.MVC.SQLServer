using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using CORE.MVC.SQLServer.Xamples;

namespace CORE.MVC.SQLServer.Web.Pages.Xamples
{
    public class EditModalModel : SQLServerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public XampleUpdateDto Xample { get; set; }

        private readonly IXamplesAppService _xamplesAppService;

        public EditModalModel(IXamplesAppService xamplesAppService)
        {
            _xamplesAppService = xamplesAppService;
        }

        public async Task OnGetAsync()
        {
            var xample = await _xamplesAppService.GetAsync(Id);
            Xample = ObjectMapper.Map<XampleDto, XampleUpdateDto>(xample);

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _xamplesAppService.UpdateAsync(Id, Xample);
            return NoContent();
        }
    }
}