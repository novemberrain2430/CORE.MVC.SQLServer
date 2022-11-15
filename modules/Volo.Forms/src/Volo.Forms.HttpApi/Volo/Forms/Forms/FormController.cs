using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Forms.Localization;
using Volo.Forms.Permissions;
using Volo.Forms.Questions;
using Volo.Forms.Responses;

namespace Volo.Forms.Forms
{
    [RemoteService(Name = FormsRemoteServiceConsts.RemoteServiceName)]
    [Area("form")]
    [ControllerName("Form")]
    [Route("api/forms")]
    [Authorize(FormsPermissions.Forms.Default)]
    public class FormController : AbpController, IFormApplicationService
    {
        protected IFormApplicationService FormApplicationService { get; }

        public FormController(IFormApplicationService formApplicationService)
        {
            FormApplicationService = formApplicationService;
            LocalizationResource = typeof(FormsResource);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<FormDto>> GetListAsync(GetFormListInputDto input)
        {
            return FormApplicationService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}/responses")]
        public Task<PagedResultDto<FormResponseDetailedDto>> GetResponsesAsync(Guid id, GetResponseListInputDto input)
        {
            return FormApplicationService.GetResponsesAsync(id, input);
        }

        [HttpGet]
        [Route("{id}/responses-count")]
        public Task<long> GetResponsesCountAsync(Guid id)
        {
            return FormApplicationService.GetResponsesCountAsync(id);
        }

        [HttpDelete]
        [Route("{id}/responses")]
        public Task DeleteAllResponsesOfFormAsync(Guid id)
        {
            return FormApplicationService.DeleteAllResponsesOfFormAsync(id);
        }

        [HttpPost]
        [Route("/invite")]
        public Task SendInviteEmailAsync(FormInviteEmailInputDto input)
        {
            return FormApplicationService.SendInviteEmailAsync(input);
        }

        [HttpGet]
        [Route("{id}/download-responses-csv")]
        public async Task<IActionResult> ExportCsvAsync(Guid id, GetResponseListInputDto input)
        {
            var form = await FormApplicationService.GetAsync(id);
            var questions = form.Questions.OrderBy(q => q.Index).ToList();
            var responseList =
                await FormApplicationService.GetResponsesAsync(id, new GetResponseListInputDto
                {
                    Sorting = "id asc"
                });

            var headers = questions.Select(q => q.Title).ToList();
            headers.AddFirst("Date");

            var csvConfiguration = new CsvConfiguration(new CultureInfo(CultureInfo.CurrentUICulture.Name))
            {
                ShouldQuote = (field, context) => true
            };
            
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(stream: memoryStream, encoding: new UTF8Encoding(true)))
                {
                    using (var csvWriter = new CsvWriter(streamWriter, csvConfiguration))
                    {
                        foreach (var header in headers)
                        {
                            csvWriter.WriteField(header);
                        }
                        
                        foreach (var response in responseList.Items)
                        {
                            await csvWriter.NextRecordAsync();

                            var date = response.LastModificationTime ?? response.CreationTime;
                            
                            csvWriter.WriteField(date.ToString("yyyy-MM-dd HH:mm:ss"));
                            
                            foreach (var question in questions)
                            {
                                var questionResponse = response.Answers.FirstOrDefault(x => x.QuestionId == question.Id);
                                
                                csvWriter.WriteField(questionResponse?.Value ?? string.Empty);
                            }
                        }
                    }

                    await memoryStream.FlushAsync();

                    var file = File(memoryStream.ToArray(), "text/csv",
                        $"{form.Title}.csv");

                    return file;
                }
            }
        }

        [HttpPost]
        public virtual Task<FormDto> CreateAsync(CreateFormDto input)
        {
            return FormApplicationService.CreateAsync(input);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public virtual Task<FormWithDetailsDto> GetAsync(Guid id)
        {
            return FormApplicationService.GetAsync(id);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<FormDto> UpdateAsync(Guid id, UpdateFormDto input)
        {
            return FormApplicationService.UpdateAsync(id, input);
        }

        [HttpPut]
        [Route("{id}/settings")]
        public virtual Task SetSettingsAsync(Guid formId, UpdateFormSettingInputDto input)
        {
            return FormApplicationService.SetSettingsAsync(formId, input);
        }

        [HttpGet]
        [Route("{id}/settings")]
        public virtual Task<FormSettingsDto> GetSettingsAsync(Guid formId)
        {
            return FormApplicationService.GetSettingsAsync(formId);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}/questions")]
        public virtual Task<List<QuestionDto>> GetQuestionsAsync(Guid id, GetQuestionListDto input)
        {
            return FormApplicationService.GetQuestionsAsync(id, input);
        }

        [HttpPost]
        [Route("{id}/questions")]
        public virtual Task<QuestionDto> CreateQuestionAsync(Guid id, CreateQuestionDto input)
        {
            return FormApplicationService.CreateQuestionAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return FormApplicationService.DeleteAsync(id);
        }
    }
}