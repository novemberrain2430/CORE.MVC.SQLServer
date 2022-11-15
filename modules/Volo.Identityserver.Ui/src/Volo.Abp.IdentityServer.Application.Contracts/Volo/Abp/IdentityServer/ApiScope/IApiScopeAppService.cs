using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.IdentityServer.ApiScope.Dtos;

namespace Volo.Abp.IdentityServer.ApiScope
{
    public interface IApiScopeAppService :
        ICrudAppService<ApiScopeWithDetailsDto, Guid,GetApiScopeListInput,CreateApiScopeDto,UpdateApiScopeDto>
    {
        Task<List<ApiScopeWithDetailsDto>> GetAllListAsync();
    }
}
