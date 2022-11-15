using System;

namespace Volo.FileManagement.Files
{
    [Serializable]
    public class FileDownloadTokenCacheItem
    {
        public Guid FileDescriptorId { get; set; }
    }
}