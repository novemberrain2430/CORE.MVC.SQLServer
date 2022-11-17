using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace CORE.MVC.SQLServer.InOuts
{
    public class InOutDto : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }

        public InOutType InOutType { get; set; }

        public DateTime IntOutDate { get; set; }
        public AuthenType AuthenType { get; set; }
        public virtual Guid? TenantId { get; set; }
    }
}
