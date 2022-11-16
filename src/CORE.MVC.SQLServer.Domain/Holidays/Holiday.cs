using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace CORE.MVC.SQLServer.Holidays
{
    public class Holiday : Entity<long>,IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }
        public string Name { set; get; }
        public int Month { set; get; }
        public int Year { set; get; }   
        public DateTime? Day { set; get; }  
        public int HeSo { set; get; }
        public string Note { set; get; }    

    }
}
