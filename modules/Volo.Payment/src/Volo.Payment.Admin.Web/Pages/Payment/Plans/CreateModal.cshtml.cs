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
    public class CreateModalModel : PaymentPageModel
    {
        protected IPlanAdminAppService PlanAdminAppService { get; }

        [BindProperty]
        public PlanCreateViewModel ViewModel { get; set; }

        public CreateModalModel(IPlanAdminAppService planAdminAppService)
        {
            PlanAdminAppService = planAdminAppService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var input = ObjectMapper.Map<PlanCreateViewModel, PlanCreateUpdateInput>(ViewModel);

            await PlanAdminAppService.CreateAsync(input);

            return NoContent();
        }

        [AutoMap(typeof(PlanCreateUpdateInput), ReverseMap = true)]
        public class PlanCreateViewModel
        {
            [Required]
            [DynamicMaxLength(typeof(PlanConsts), nameof(PlanConsts.MaxNameLength))]
            public string Name { get; set; }
        }
    }
}
