using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.CmsKit.Admin.Contact;

namespace Volo.CmsKit.Pro.Admin.Web.Pages.CmsKit.Components.CmsKitProSettingGroup
{
    public class CmsKitProSettingGroupViewComponent : AbpViewComponent
    {
        protected IContactSettingsAppService ContactSettingsAppService { get; }
        
        public CmsKitProSettingGroupViewComponent(IContactSettingsAppService contactSettingsAppService)
        {
            ContactSettingsAppService = contactSettingsAppService;
        }

        public virtual async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await ContactSettingsAppService.GetAsync();

            return View("~/Pages/CmsKit/Components/CmsKitProSettingGroup/Default.cshtml", model);
        }
    }
}