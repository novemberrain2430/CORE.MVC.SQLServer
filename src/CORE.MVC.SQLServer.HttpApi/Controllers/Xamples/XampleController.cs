using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using CORE.MVC.SQLServer.Xamples;

namespace CORE.MVC.SQLServer.Controllers.Xamples
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Xample")]
    [Route("api/app/xamples")]

    public class XampleController : AbpController, IXamplesAppService
    {
        private readonly IXamplesAppService _xamplesAppService;

        public XampleController(IXamplesAppService xamplesAppService)
        {
            _xamplesAppService = xamplesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<XampleDto>> GetListAsync(GetXamplesInput input)
        {
            return _xamplesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<XampleDto> GetAsync(Guid id)
        {
            return _xamplesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<XampleDto> CreateAsync(XampleCreateDto input)
        {
            return _xamplesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<XampleDto> UpdateAsync(Guid id, XampleUpdateDto input)
        {
            return _xamplesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _xamplesAppService.DeleteAsync(id);
        }
    }
}