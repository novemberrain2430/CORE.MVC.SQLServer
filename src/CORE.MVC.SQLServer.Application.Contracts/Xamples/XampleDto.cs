using System;
using Volo.Abp.Application.Dtos;

namespace CORE.MVC.SQLServer.Xamples
{
    public class XampleDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public DateTime? Date1 { get; set; }
        public int Year { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public bool IsConfirm { get; set; }
        public Guid UserId { get; set; }
    }
}