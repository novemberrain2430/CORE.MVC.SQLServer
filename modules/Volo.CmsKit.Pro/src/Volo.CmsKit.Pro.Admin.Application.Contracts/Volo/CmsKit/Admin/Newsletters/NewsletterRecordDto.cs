using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;

namespace Volo.CmsKit.Admin.Newsletters
{
    public class NewsletterRecordDto : EntityDto<Guid>, IHasCreationTime
    {
        public string EmailAddress { get; set; }

        public DateTime CreationTime { get; set; }
    }
}