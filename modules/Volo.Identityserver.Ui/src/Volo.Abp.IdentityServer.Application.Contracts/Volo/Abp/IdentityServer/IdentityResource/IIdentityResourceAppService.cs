using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.IdentityServer.IdentityResource.Dtos;

namespace Volo.Abp.IdentityServer.IdentityResource
{
    public interface IIdentityResourceAppService :
        ICrudAppService<IdentityResourceWithDetailsDto, Guid, GetIdentityResourceListInput, CreateIdentityResourceDto, UpdateIdentityResourceDto>
    {
        Task<List<IdentityResourceWithDetailsDto>> GetAllListAsync();

        Task CreateStandardResourcesAsync();
    }
}
