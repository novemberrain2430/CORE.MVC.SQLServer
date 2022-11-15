using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Auditing;
using Volo.Abp.ObjectExtending;
using Volo.Saas.Host.Dtos;

namespace Volo.Saas.Host.Pages.Saas.Host.Tenants
{
    public class CreateModalModel : SaasHostPageModel
    {
        [BindProperty]
        public TenantInfoModel Tenant { get; set; }

        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public List<SelectListItem> DatabaseSelectListItems { get; set; }

        public List<SelectListItem> EditionsComboboxItems { get; set; } = new List<SelectListItem>();

        protected ITenantAppService TenantAppService { get; }
        protected IEditionAppService EditionAppService { get; }

        public CreateModalModel(ITenantAppService tenantAppService, IEditionAppService editionAppService)
        {
            TenantAppService = tenantAppService;
            EditionAppService = editionAppService;
        }

        public virtual async Task OnGetAsync()
        {
            Tenant = new TenantInfoModel
            {
                UseSharedDatabase = true,
                ConnectionStrings = new TenantConnectionStringsModel()
            };

            DatabaseSelectListItems = new List<SelectListItem>();
            foreach (var database in (await TenantAppService.GetDatabasesAsync()).Databases)
            {
                DatabaseSelectListItems.Add(new SelectListItem(database, database));
            }

            var editions = await EditionAppService.GetListAsync(new GetEditionsInput { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount });

            EditionsComboboxItems.Add(new SelectListItem("", "",true));
            EditionsComboboxItems.AddRange(editions.Items
                .Select(e => new SelectListItem(e.DisplayName, e.Id.ToString())).ToList());
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            var input = ObjectMapper.Map<TenantInfoModel, SaasTenantCreateDto>(Tenant);

            if (Tenant.UseSharedDatabase)
            {
                input.ConnectionStrings = null;
            }

            await TenantAppService.CreateAsync(input);

            return NoContent();
        }

        public class TenantInfoModel : ExtensibleObject
        {
            [Required]
            [StringLength(TenantConsts.MaxNameLength)]
            public string Name { get; set; }

            [SelectItems(nameof(EditionsComboboxItems))]
            public Guid? EditionId { get; set; }

            [Required]
            [EmailAddress]
            [StringLength(256)]
            public string AdminEmailAddress { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [StringLength(128)]
            [DisableAuditing]
            public string AdminPassword { get; set; }

            public bool UseSharedDatabase { get; set; }

            public TenantConnectionStringsModel ConnectionStrings { get; set; }

            public TenantActivationState ActivationState { get; set; }

            public DateTime? ActivationEndDate { get; set; }
        }

        public class TenantConnectionStringsModel
        {
            [StringLength(TenantConnectionStringConsts.MaxValueLength)]
            [DisableAuditing]
            public string Default { get; set; }

            public List<TenantDatabaseConnectionStringsModel> Databases { get; set; }
        }

        public class TenantDatabaseConnectionStringsModel
        {
            public string DatabaseName { get; set; }

            [StringLength(TenantConnectionStringConsts.MaxValueLength)]
            [DisableAuditing]
            public string ConnectionString { get; set; }
        }
    }
}
