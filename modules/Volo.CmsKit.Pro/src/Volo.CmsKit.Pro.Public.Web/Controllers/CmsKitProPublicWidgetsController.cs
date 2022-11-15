using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.CmsKit.Pro.Public.Web.Pages.Public.Shared.Components.Newsletter;
using Volo.CmsKit.Pro.Public.Web.Pages.Public.Shared.Components.Contact;

namespace Volo.CmsKit.Pro.Public.Web.Controllers
{
    public class CmsKitProPublicWidgetsController : CmsKitProPublicController
    {
        public IActionResult Contact()
        {
            return ViewComponent(typeof(ContactViewComponent));
        }
    }
}