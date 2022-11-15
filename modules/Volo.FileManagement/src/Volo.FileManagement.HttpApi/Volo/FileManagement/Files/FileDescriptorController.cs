using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Content;
using Volo.FileManagement.Authorization;

namespace Volo.FileManagement.Files
{
    [RemoteService(Name = FileManagementRemoteServiceConsts.RemoteServiceName)]
    [Area("fileManagement")]
    [ControllerName("FileDescriptors")]
    [Route("api/file-management/file-descriptor")]
    [Authorize(FileManagementPermissions.FileDescriptor.Default)]
    public class FileDescriptorController : AbpController, IFileDescriptorAppService
    {
        protected IFileDescriptorAppService FileDescriptorAppService { get; }

        public FileDescriptorController(IFileDescriptorAppService fileDescriptorAppService)
        {
            FileDescriptorAppService = fileDescriptorAppService;
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public virtual async Task<FileDescriptorDto> GetAsync(Guid id)
        {
            return await FileDescriptorAppService.GetAsync(id);
        }

        [HttpGet]
        public virtual async Task<ListResultDto<FileDescriptorDto>> GetListAsync(Guid? directoryId)
        {
            return await FileDescriptorAppService.GetListAsync(directoryId);
        }

        [HttpPost]
        [Route("upload")]
        [Authorize(FileManagementPermissions.FileDescriptor.Create)]
        public virtual Task<FileDescriptorDto> CreateAsync(Guid? directoryId, CreateFileInputWithStream inputWithStream)
        {
            return FileDescriptorAppService.CreateAsync(directoryId, inputWithStream);
        }

        [HttpPost]
        [Route("move")]
        [Authorize(FileManagementPermissions.FileDescriptor.Update)]
        public virtual async Task<FileDescriptorDto> MoveAsync(MoveFileInput input)
        {
            return await FileDescriptorAppService.MoveAsync(input);
        }

        [HttpPost]
        [Route("pre-upload-info")]
        public virtual async Task<List<FileUploadPreInfoDto>> GetPreInfoAsync(List<FileUploadPreInfoRequest> input)
        {
            return await FileDescriptorAppService.GetPreInfoAsync(input);
        }

        [HttpGet]
        [Route("content")]
        public virtual async Task<byte[]> GetContentAsync(Guid id)
        {
            return await FileDescriptorAppService.GetContentAsync(id);
        }

        [HttpGet]
        [Route("download/{id}/token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync(Guid id)
        {
            return FileDescriptorAppService.GetDownloadTokenAsync(id);
        }

        [HttpPost]
        [Route("{id}")]
        [Authorize(FileManagementPermissions.FileDescriptor.Update)]
        public virtual async Task<FileDescriptorDto> RenameAsync(Guid id, RenameFileInput input)
        {
            return await FileDescriptorAppService.RenameAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(FileManagementPermissions.FileDescriptor.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await FileDescriptorAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("download/{id}")]
        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> DownloadAsync(Guid id, string token)
        {
            var fileDescriptor = await FileDescriptorAppService.GetAsync(id);

            Response.Headers.Add("Content-Disposition", $"attachment;filename=\"{fileDescriptor.Name}\"");
            Response.Headers.Add("Accept-Ranges", "bytes");
            Response.ContentType = fileDescriptor.MimeType;

            return await FileDescriptorAppService.DownloadAsync(id, token);
        }
    }
}
