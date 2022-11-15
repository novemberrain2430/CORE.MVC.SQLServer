using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using CORE.MVC.SQLServer.Samples;
using CORE.MVC.SQLServer.Shared;

namespace CORE.MVC.SQLServer.Web.Pages.Samples
{
    public class IndexModel : AbpPageModel
    {
        public string NameFilter { get; set; }
        public DateTime? Date1FilterMin { get; set; }

        public DateTime? Date1FilterMax { get; set; }
        public int? YearFilterMin { get; set; }

        public int? YearFilterMax { get; set; }
        public string CodeFilter { get; set; }
        public string EmailFilter { get; set; }
        [SelectItems(nameof(IsConfirmBoolFilterItems))]
        public string IsConfirmFilter { get; set; }

        public List<SelectListItem> IsConfirmBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        public string UserIdFilter { get; set; }

        private readonly ISamplesAppService _samplesAppService;

        public IndexModel(ISamplesAppService samplesAppService)
        {
            _samplesAppService = samplesAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}