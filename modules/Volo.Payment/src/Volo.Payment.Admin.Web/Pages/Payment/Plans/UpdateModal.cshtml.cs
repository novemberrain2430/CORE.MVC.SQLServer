using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Validation;
using Volo.Payment.Admin.Plans;
using Volo.Payment.Plans;

namespace Volo.Payment.Admin.Web.Pages.Payment.Plans
{
    public class UpdateModalModel : PaymentPageModel
    {
        protected IPlanAdminAppService PlanAdminAppService { get; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public PlanUpdateViewModel ViewModel { get; set; }

        public UpdateModalModel(IPlanAdminAppService planAdminAppService)
        {
            PlanAdminAppService = planAdminAppService;
        }

        public async Task OnGetAsync()
        {
            var planDto = await PlanAdminAppService.GetAsync(Id);

            ViewModel = ObjectMapper.Map<PlanDto, PlanUpdateViewModel>(planDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var input = ObjectMapper.Map<PlanUpdateViewModel, PlanCreateUpdateInput>(ViewModel);

            await PlanAdminAppService.UpdateAsync(Id, input);

            return NoContent();
        }

        [AutoMap(typeof(PlanDto))]
        [AutoMap(typeof(PlanCreateUpdateInput), ReverseMap = true)]
        public class PlanUpdateViewModel
        {
            [Required]
            [DynamicMaxLength(typeof(PlanConsts), nameof(PlanConsts.MaxNameLength))]
            public string Name { get; set; }
        }
    }
}
