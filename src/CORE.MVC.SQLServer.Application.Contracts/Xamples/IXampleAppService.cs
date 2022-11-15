using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CORE.MVC.SQLServer.Xamples
{
    public interface IXamplesAppService : IApplicationService
    {
        Task<PagedResultDto<XampleDto>> GetListAsync(GetXamplesInput input);

        Task<XampleDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<XampleDto> CreateAsync(XampleCreateDto input);

        Task<XampleDto> UpdateAsync(Guid id, XampleUpdateDto input);
    }
}