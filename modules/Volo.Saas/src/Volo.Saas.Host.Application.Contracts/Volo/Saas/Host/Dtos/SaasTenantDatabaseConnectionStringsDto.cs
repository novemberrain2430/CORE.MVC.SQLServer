using System.ComponentModel.DataAnnotations;
using Volo.Abp.Auditing;

namespace Volo.Saas.Host.Dtos
{
    public class SaasTenantDatabaseConnectionStringsDto
    {
        public string DatabaseName { get; set; }

        [StringLength(TenantConnectionStringConsts.MaxValueLength)]
        [DisableAuditing]
        public string ConnectionString { get; set; }
    }
}
