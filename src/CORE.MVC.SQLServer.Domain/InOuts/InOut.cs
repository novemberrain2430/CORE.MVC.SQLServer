using CORE.MVC.SQLServer.Books;
using CORE.MVC.SQLServer.InOuts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace CORE.MVC.SQLServer.InOuts
{
    public class InOut : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid UserId { get; set; }

        public InOutType InOutType { get; set; }

        public DateTime IntOutDate { get; set; }
        public AuthenType AuthenType { get; set; }
        public virtual Guid? TenantId { get; set; }
        public InOut()
        {

        }

    
    
    }
}
