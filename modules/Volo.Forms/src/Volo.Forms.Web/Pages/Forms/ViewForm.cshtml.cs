using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Forms.Responses;

namespace Volo.Forms.Web.Pages.Forms
{
    public class ViewFormModel : FormsPageModel
    {
        [Required]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        
        protected IResponseAppService ResponseAppService { get; }
        public string Title { get; set; }

        public ViewFormModel(IResponseAppService responseAppService)
        {
            ResponseAppService = responseAppService;
        }

        public virtual async Task<IActionResult> OnGet()
        {
            var form = await ResponseAppService.GetFormDetailsAsync(Id);
            
            Title = form.Title;
            
            if (!form.RequiresLogin)
            {
                return await Task.FromResult<IActionResult>(Page());
            }

            if (!CurrentUser.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login", new
                {
                    ReturnUrl = $"/Forms/{Id}/ViewForm"
                });
            }

            return await Task.FromResult<IActionResult>(Page());
        }

        public virtual Task<IActionResult> OnPostAsync()
        {
            return Task.FromResult<IActionResult>(Page());
        }
    }
}