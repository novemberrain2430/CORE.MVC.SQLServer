using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CORE.MVC.SQLServer.Samples
{
    public interface ISamplesAppService : IApplicationService
    {
        Task<PagedResultDto<SampleDto>> GetListAsync(GetSamplesInput input);

        Task<SampleDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<SampleDto> CreateAsync(SampleCreateDto input);

        Task<SampleDto> UpdateAsync(Guid id, SampleUpdateDto input);
    }
}