using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace CORE.MVC.SQLServer.Weekends
{
    public class Weekend : Entity<long>,IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }
        public int WeekendID { set; get; }
        public bool IsYes { set; get; }
        public Weekend() 
        {
        }
    }
}
