using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace CORE.MVC.SQLServer.CodeChamCongs
{
    public class CodeChamCong : Entity<long>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }
        public string DiTre { set; get; }
        public string VeSom { set; get; }
        public string DungGio { set; get; }
        public string TangCa { set; get; }
        public string ThieuGioVao { set; get; }
        public string ThieuGioRa { set; get; }
        public string Vang { set; get; }
        public string ChuaXepCa { set; get; }
        public string PhepNam { set; get; }
        public string Le { set; get; }
        public string CongTac { set; get; }
        public string VeTre { set; get; }
        public string CuoiTuan { set; get; }
    }
}
