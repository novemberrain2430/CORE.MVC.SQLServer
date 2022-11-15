using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.ChartJs;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Select2;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Vue;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;
using Volo.Abp.Domain.Entities;
using Volo.Forms.Forms;

namespace Volo.Forms.Web.Pages.Forms.Shared.Components.FormResponses
{
    [Widget(
        StyleFiles = new[] {"/Pages/Forms/Shared/Components/FormResponses/Default.css"},
        StyleTypes = new[] {typeof(Select2StyleContributor)},
        ScriptTypes = new[] {typeof(VueScriptContributor), typeof(ChartjsScriptContributor)},
        ScriptFiles = new[]
        {
            "/Pages/Forms/Shared/Components/FormResponses/Vue-block-response-component.js",
            "/Pages/Forms/Shared/Components/FormResponses/Vue-response-chart.js",
            "/Pages/Forms/Shared/Components/FormResponses/Vue-response-answers.js",
            "/Pages/Forms/Shared/Components/FormResponses/Default.js"
        },
        AutoInitialize = true
    )]
    [ViewComponent(Name = "FormResponses")]
    public class FormResponsesViewComponent : AbpViewComponent
    {
        protected IFormApplicationService FormApplicationService { get; }

        public FormResponsesViewComponent(IFormApplicationService formApplicationService)
        {
            FormApplicationService = formApplicationService;
        }

        public virtual async Task<IViewComponentResult> InvokeAsync(Guid id)
        {
            var form = await FormApplicationService.GetAsync(id);
            if (form == null)
            {
                throw new EntityNotFoundException();
            }

            var vm = new FormResponsesViewModel
            {
                Id = form.Id,
                IsAcceptingResponses = form.IsAcceptingResponses
            };
            return View("~/Pages/Forms/Shared/Components/FormResponses/Default.cshtml", vm);
        }

        public class FormResponsesViewModel
        {
            public Guid Id { get; set; }
            public bool IsAcceptingResponses { get; set; }
        }
    }
}