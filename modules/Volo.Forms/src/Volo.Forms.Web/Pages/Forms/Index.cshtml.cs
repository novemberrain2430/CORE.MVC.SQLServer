using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Volo.Forms.Web.Pages.Forms
{
    public class IndexModel : FormsPageModel
    {
        public IndexModel()
        {
        }

        public virtual Task<IActionResult> OnGetAsync()
        {
            return Task.FromResult<IActionResult>(Page());
        }

        public virtual Task<IActionResult> OnPostAsync()
        {
            return Task.FromResult<IActionResult>(Page());
        }
    }
}