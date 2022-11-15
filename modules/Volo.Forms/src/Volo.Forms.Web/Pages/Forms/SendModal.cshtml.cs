using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Forms.Forms;

namespace Volo.Forms.Web.Pages.Forms
{
    public class SendModalModel : FormsPageModel
    {
        [BindProperty(SupportsGet = true)] 
        public Guid Id { get; set; }
        
        [BindProperty] 
        public SendFormInfoModel Form { get; set; }
        
        protected IFormApplicationService FormApplicationService { get; }
        protected IHttpContextAccessor HttpContextAccessor { get; }
        

        public SendModalModel(IFormApplicationService formApplicationService, IHttpContextAccessor httpContextAccessor)
        {
            FormApplicationService = formApplicationService;
            HttpContextAccessor = httpContextAccessor;
        }

        public virtual async Task OnGetAsync()
        {
            var form = await FormApplicationService.GetAsync(Id);

            var link = GenerateLink(form.Id);
            var message = L["Form:SendFormInvitation"].Value + $"\n<br />\n<a href=\"{link}\">{form.Title}</a>";
            
            Form = new SendFormInfoModel
            {
                Id = form.Id,
                Link = link,
                Body = message,
                Subject = form.Title
            };
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            return NoContent();
        }

        public class SendFormInfoModel
        {
            [HiddenInput] 
            public Guid Id { get; set; }
            public string Link { get; set; }
            [TextArea(Rows = 3)]
            public string Body { get; set; }
            public string Subject { get; set; }
            [Required]
            [EmailAddress]
            public string To { get; set; }
        }

        private string GenerateLink(Guid formId)
        {
            var request = HttpContextAccessor.HttpContext?.Request;
            var scheme = request?.Scheme;
            var host = request?.Host.Value;
            return $"{scheme}://{host}/Forms/{formId}/ViewForm";
        }
    }
}