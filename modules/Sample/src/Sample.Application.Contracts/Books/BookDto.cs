using System;
using Volo.Abp.Application.Dtos;

namespace Sample.Books
{
    public class BookDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}