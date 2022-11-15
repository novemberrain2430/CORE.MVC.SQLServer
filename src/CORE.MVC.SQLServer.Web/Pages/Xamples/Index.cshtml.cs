using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using CORE.MVC.SQLServer.Xamples;
using CORE.MVC.SQLServer.Shared;

namespace CORE.MVC.SQLServer.Web.Pages.Xamples
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

        private readonly IXamplesAppService _xamplesAppService;

        public IndexModel(IXamplesAppService xamplesAppService)
        {
            _xamplesAppService = xamplesAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}