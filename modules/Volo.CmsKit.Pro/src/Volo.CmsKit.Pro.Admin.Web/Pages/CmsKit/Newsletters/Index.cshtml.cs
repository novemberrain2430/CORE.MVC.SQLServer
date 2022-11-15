using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.CmsKit.Admin.Newsletters;

namespace Volo.CmsKit.Pro.Admin.Web.Pages.CmsKit.Newsletters
{
    public class IndexModel : AbpPageModel
    {
        public SelectList PreferencesSelectList { get; set; }
        
        [SelectItems(nameof(PreferencesSelectList))]
        public string Preference { get; set; }
        
        [DisplayName("Source")]
        public string Source { get; set; }

        private readonly INewsletterRecordAdminAppService _newsletterRecordAdminAppService;

        public IndexModel(INewsletterRecordAdminAppService newsletterRecordAdminAppService)
        {
            _newsletterRecordAdminAppService = newsletterRecordAdminAppService;    
        }
        
        public async Task OnGetAsync()
        {
            var newsletterPreferences = await _newsletterRecordAdminAppService.GetNewsletterPreferencesAsync();
            newsletterPreferences.AddFirst("");
            
            PreferencesSelectList = new SelectList(newsletterPreferences);
        }
    }
}