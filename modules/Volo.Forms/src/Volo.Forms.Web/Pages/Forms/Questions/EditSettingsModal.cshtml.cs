using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Forms.Forms;

namespace Volo.Forms.Web.Pages.Forms.Questions
{
    public class EditSettingsModalModel : FormsPageModel
    {
        [BindProperty(SupportsGet = true)] 
        public Guid Id { get; set; }
        
        [BindProperty] 
        public UpdateFormSettingInputDto FormSettings { get; set; }
        
        protected IFormApplicationService FormApplicationService { get; }

        public EditSettingsModalModel(IFormApplicationService formApplicationService)
        {
            FormApplicationService = formApplicationService;
        }

        public virtual async Task OnGetAsync()
        {
            var settings = await FormApplicationService.GetSettingsAsync(Id);

            FormSettings = ObjectMapper.Map<FormSettingsDto, UpdateFormSettingInputDto>(settings);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await FormApplicationService.SetSettingsAsync(Id, FormSettings);

            return NoContent();
        }
    }
}