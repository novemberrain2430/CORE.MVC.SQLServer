using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using CORE.MVC.SQLServer.Samples;

namespace CORE.MVC.SQLServer.Controllers.Samples
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Sample")]
    [Route("api/app/samples")]

    public class SampleController : AbpController, ISamplesAppService
    {
        private readonly ISamplesAppService _samplesAppService;

        public SampleController(ISamplesAppService samplesAppService)
        {
            _samplesAppService = samplesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<SampleDto>> GetListAsync(GetSamplesInput input)
        {
            return _samplesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<SampleDto> GetAsync(Guid id)
        {
            return _samplesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<SampleDto> CreateAsync(SampleCreateDto input)
        {
            return _samplesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<SampleDto> UpdateAsync(Guid id, SampleUpdateDto input)
        {
            return _samplesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _samplesAppService.DeleteAsync(id);
        }
    }
}