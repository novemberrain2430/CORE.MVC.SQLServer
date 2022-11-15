using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Auditing;

namespace Volo.Saas.Host.Dtos
{
    public class SaasTenantConnectionStringsDto
    {
        [StringLength(TenantConnectionStringConsts.MaxValueLength)]
        [DisableAuditing]
        public string Default { get; set; }

        public List<SaasTenantDatabaseConnectionStringsDto> Databases { get; set; }
    }
}
