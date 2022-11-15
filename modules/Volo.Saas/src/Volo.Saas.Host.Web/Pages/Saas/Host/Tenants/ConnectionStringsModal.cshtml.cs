using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Saas.Host.Dtos;

namespace Volo.Saas.Host.Pages.Saas.Host.Tenants
{
    public class ConnectionStringsModal : SaasHostPageModel
    {
        [BindProperty]
        public TenantConnectionStringsModel ConnectionStrings { get; set; }

        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public List<SelectListItem> DatabaseSelectListItems { get; set; }

        protected ITenantAppService TenantAppService { get; }

        public ConnectionStringsModal(ITenantAppService tenantAppService)
        {
            TenantAppService = tenantAppService;
        }

        public virtual async Task OnGetAsync(Guid id)
        {
            ConnectionStrings = ObjectMapper.Map<SaasTenantConnectionStringsDto, TenantConnectionStringsModel>(await TenantAppService.GetConnectionStringsAsync(id));

            ConnectionStrings.Id = id;
            ConnectionStrings.UseSharedDatabase = ConnectionStrings.Default.IsNullOrWhiteSpace() &&
                                                  (ConnectionStrings.Databases.IsNullOrEmpty() ||
                                                  ConnectionStrings.Databases.All(x => x.ConnectionString.IsNullOrWhiteSpace()));

            DatabaseSelectListItems = ConnectionStrings.Databases.Where(x => x.ConnectionString.IsNullOrWhiteSpace())
                .Select(x => new SelectListItem(x.DatabaseName, x.DatabaseName))
                .ToList();
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            if (ConnectionStrings.UseSharedDatabase)
            {
                await TenantAppService.UpdateConnectionStringsAsync(ConnectionStrings.Id, null);
            }
            else
            {
                var input = ObjectMapper.Map<TenantConnectionStringsModel, SaasTenantConnectionStringsDto>(ConnectionStrings);
                await TenantAppService.UpdateConnectionStringsAsync(ConnectionStrings.Id, input);
            }

            return NoContent();
        }

        public class TenantConnectionStringsModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            public bool UseSharedDatabase { get; set; }

            [StringLength(TenantConnectionStringConsts.MaxValueLength)]
            public string Default { get; set; }

            public List<TenantDatabaseConnectionStringsModel> Databases { get; set; }
        }

        public class TenantDatabaseConnectionStringsModel
        {
            public string DatabaseName { get; set; }

            [StringLength(TenantConnectionStringConsts.MaxValueLength)]
            public string ConnectionString { get; set; }
        }
    }
}
