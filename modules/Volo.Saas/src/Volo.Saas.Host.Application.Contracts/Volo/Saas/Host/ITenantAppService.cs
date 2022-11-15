using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Saas.Host.Dtos;

namespace Volo.Saas.Host
{
    public interface ITenantAppService : ICrudAppService<SaasTenantDto, Guid, GetTenantsInput, SaasTenantCreateDto, SaasTenantUpdateDto>
    {
        Task<SaasTenantDatabasesDto> GetDatabasesAsync();

        Task<SaasTenantConnectionStringsDto> GetConnectionStringsAsync(Guid id);

        Task UpdateConnectionStringsAsync(Guid id, SaasTenantConnectionStringsDto input);

        Task ApplyDatabaseMigrationsAsync(Guid id);
    }
}
