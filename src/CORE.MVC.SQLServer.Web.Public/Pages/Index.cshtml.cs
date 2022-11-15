using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace CORE.MVC.SQLServer.Web.Public.Pages
{
    public class IndexModel : SQLServerPublicPageModel
    {
        public void OnGet()
        {

        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}
