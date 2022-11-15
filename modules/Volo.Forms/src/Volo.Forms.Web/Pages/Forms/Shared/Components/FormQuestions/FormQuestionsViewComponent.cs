using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Select2;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.VeeValidate;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.Vue;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;
using Volo.Abp.Domain.Entities;
using Volo.Forms.Forms;

namespace Volo.Forms.Web.Pages.Forms.Shared.Components.FormQuestions
{
    [Widget(
        StyleFiles = new[] {"/Pages/Forms/Shared/Components/FormQuestions/Default.css"},
        StyleTypes = new[] {typeof(Select2StyleContributor)},
        ScriptTypes = new[] {typeof(VueScriptContributor), typeof(VeeValidateScriptContributor)},
        ScriptFiles = new[]
        {
            "/Pages/Forms/Shared/Components/FormQuestions/Vue-question-choice.js",
            "/Pages/Forms/Shared/Components/FormQuestions/Vue-question-types.js",
            "/Pages/Forms/Shared/Components/FormQuestions/Vue-question-item.js",
            "/Pages/Forms/Shared/Components/FormQuestions/Default.js"
        },
        AutoInitialize = true
    )]
    [ViewComponent(Name = "FormQuestions")]
    public class FormQuestionsViewComponent : AbpViewComponent
    {
        protected IFormApplicationService FormApplicationService { get; }

        public FormQuestionsViewComponent(IFormApplicationService formApplicationService)
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

            var vm = new FormQuestionsViewModel()
            {
                Id = form.Id,
                IsAcceptingResponses = form.IsAcceptingResponses
            };
            return View("~/Pages/Forms/Shared/Components/FormQuestions/Default.cshtml", vm);
        }

        public class FormQuestionsViewModel
        {
            public Guid Id { get; set; }
            public bool IsAcceptingResponses { get; set; }
        }
    }
}