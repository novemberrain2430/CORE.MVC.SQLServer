using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Saas.Host.Dtos;

namespace Volo.Saas.Host
{
    [Controller]
    [RemoteService(Name = SaasHostRemoteServiceConsts.RemoteServiceName)]
    [Area("saas")]
    [ControllerName("Tenant")]
    [Route("/api/saas/tenants")]
    public class TenantController : AbpController, ITenantAppService
    {
        protected ITenantAppService Service { get; }

        public TenantController(ITenantAppService service)
        {
            Service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<SaasTenantDto> GetAsync(Guid id)
        {
            return Service.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<SaasTenantDto>> GetListAsync(GetTenantsInput input)
        {
            return Service.GetListAsync(input);
        }

        [HttpPost]
        public virtual Task<SaasTenantDto> CreateAsync(SaasTenantCreateDto input)
        {
            ValidateModel();
            return Service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<SaasTenantDto> UpdateAsync(Guid id, SaasTenantUpdateDto input)
        {
            return Service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return Service.DeleteAsync(id);
        }

        [HttpGet]
        [Route("databases")]
        public Task<SaasTenantDatabasesDto> GetDatabasesAsync()
        {
            return Service.GetDatabasesAsync();
        }

        [HttpGet]
        [Route("{id}/connection-strings")]
        public Task<SaasTenantConnectionStringsDto> GetConnectionStringsAsync(Guid id)
        {
            return Service.GetConnectionStringsAsync(id);
        }

        [HttpPut]
        [Route("{id}/connection-strings")]
        public Task UpdateConnectionStringsAsync(Guid id, SaasTenantConnectionStringsDto input)
        {
            return Service.UpdateConnectionStringsAsync(id, input);
        }

        [HttpPost]
        [Route("{id}/apply-database-migrations")]
        public Task ApplyDatabaseMigrationsAsync(Guid id)
        {
            return Service.ApplyDatabaseMigrationsAsync(id);
        }
    }
}
