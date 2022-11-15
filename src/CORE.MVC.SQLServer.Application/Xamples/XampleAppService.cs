using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using CORE.MVC.SQLServer.Permissions;
using CORE.MVC.SQLServer.Xamples;

namespace CORE.MVC.SQLServer.Xamples
{
    [RemoteService(IsEnabled = false)]
    [Authorize(SQLServerPermissions.Xamples.Default)]
    public class XamplesAppService : ApplicationService, IXamplesAppService
    {
        private readonly IXampleRepository _xampleRepository;

        public XamplesAppService(IXampleRepository xampleRepository)
        {
            _xampleRepository = xampleRepository;
        }

        public virtual async Task<PagedResultDto<XampleDto>> GetListAsync(GetXamplesInput input)
        {
            var totalCount = await _xampleRepository.GetCountAsync(input.FilterText, input.Name, input.Date1Min, input.Date1Max, input.YearMin, input.YearMax, input.Code, input.Email, input.IsConfirm, input.UserId);
            var items = await _xampleRepository.GetListAsync(input.FilterText, input.Name, input.Date1Min, input.Date1Max, input.YearMin, input.YearMax, input.Code, input.Email, input.IsConfirm, input.UserId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<XampleDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Xample>, List<XampleDto>>(items)
            };
        }

        public virtual async Task<XampleDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Xample, XampleDto>(await _xampleRepository.GetAsync(id));
        }

        [Authorize(SQLServerPermissions.Xamples.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _xampleRepository.DeleteAsync(id);
        }

        [Authorize(SQLServerPermissions.Xamples.Create)]
        public virtual async Task<XampleDto> CreateAsync(XampleCreateDto input)
        {

            var xample = ObjectMapper.Map<XampleCreateDto, Xample>(input);
            xample.TenantId = CurrentTenant.Id;
            xample = await _xampleRepository.InsertAsync(xample, autoSave: true);
            return ObjectMapper.Map<Xample, XampleDto>(xample);
        }

        [Authorize(SQLServerPermissions.Xamples.Edit)]
        public virtual async Task<XampleDto> UpdateAsync(Guid id, XampleUpdateDto input)
        {

            var xample = await _xampleRepository.GetAsync(id);
            ObjectMapper.Map(input, xample);
            xample = await _xampleRepository.UpdateAsync(xample, autoSave: true);
            return ObjectMapper.Map<Xample, XampleDto>(xample);
        }
    }
}