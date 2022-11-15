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
using CORE.MVC.SQLServer.Samples;

namespace CORE.MVC.SQLServer.Samples
{
    [RemoteService(IsEnabled = false)]
    //[Authorize(SQLServerPermissions.Samples.Default)]
    public class SamplesAppService : ApplicationService, ISamplesAppService
    {
        private readonly ISampleRepository _sampleRepository;

        public SamplesAppService(ISampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        public virtual async Task<PagedResultDto<SampleDto>> GetListAsync(GetSamplesInput input)
        {
            var totalCount = await _sampleRepository.GetCountAsync(input.FilterText, input.Name, input.Date1Min, input.Date1Max, input.YearMin, input.YearMax, input.Code, input.Email, input.IsConfirm, input.UserId);
            var items = await _sampleRepository.GetListAsync(input.FilterText, input.Name, input.Date1Min, input.Date1Max, input.YearMin, input.YearMax, input.Code, input.Email, input.IsConfirm, input.UserId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<SampleDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Sample>, List<SampleDto>>(items)
            };
        }

        public virtual async Task<SampleDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Sample, SampleDto>(await _sampleRepository.GetAsync(id));
        }

        [Authorize(SQLServerPermissions.Samples.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _sampleRepository.DeleteAsync(id);
        }

        [Authorize(SQLServerPermissions.Samples.Create)]
        public virtual async Task<SampleDto> CreateAsync(SampleCreateDto input)
        {

            var sample = ObjectMapper.Map<SampleCreateDto, Sample>(input);
            sample.TenantId = CurrentTenant.Id;
            sample = await _sampleRepository.InsertAsync(sample, autoSave: true);
            return ObjectMapper.Map<Sample, SampleDto>(sample);
        }

        [Authorize(SQLServerPermissions.Samples.Edit)]
        public virtual async Task<SampleDto> UpdateAsync(Guid id, SampleUpdateDto input)
        {

            var sample = await _sampleRepository.GetAsync(id);
            ObjectMapper.Map(input, sample);
            sample = await _sampleRepository.UpdateAsync(sample, autoSave: true);
            return ObjectMapper.Map<Sample, SampleDto>(sample);
        }
    }
}