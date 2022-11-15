using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.IdentityServer.ApiResource.Dtos;

namespace Volo.Abp.IdentityServer.ApiResource
{
    public interface IApiResourceAppService : ICrudAppService<ApiResourceWithDetailsDto, Guid, GetApiResourceListInput, CreateApiResourceDto, UpdateApiResourceDto>
    {

        Task<List<ApiResourceWithDetailsDto>> GetAllListAsync();
    }
}
