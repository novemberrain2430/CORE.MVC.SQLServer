using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace CORE.MVC.SQLServer.Absents
{
    public class Absent : Entity<long>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }
        public string Name { get; set; }    
        public string Code { set; get; }
        public string Active { set; get; }

    }
}
