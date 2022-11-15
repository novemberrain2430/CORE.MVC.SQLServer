using System.ComponentModel.DataAnnotations;
using Volo.Abp.Content;
using Volo.Abp.Validation;

namespace Volo.FileManagement.Files
{
    public class CreateFileInputWithStream
    {
        [Required]
        [DynamicStringLength(typeof(FileDescriptorConsts), nameof(FileDescriptorConsts.MaxNameLength))]
        public string Name { get; set; }

        public IRemoteStreamContent File { get; set; }
    }
}
