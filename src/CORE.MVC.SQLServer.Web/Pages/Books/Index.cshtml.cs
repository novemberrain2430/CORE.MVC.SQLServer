using CORE.MVC.SQLServer.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CORE.MVC.SQLServer.Web.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly IBookAppService _bookAppService;

        public IndexModel(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}
