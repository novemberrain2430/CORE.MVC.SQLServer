using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace CORE.MVC.SQLServer.TinhCongs
{
    public class TinhCong  : Entity<long>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }
        public string MaNhanVien { set; get; }
        public string TenNhanVien { set; get; }
        public int MaChamCong { set; get; }
        public DateTime? Ngay { set; get; }
        public string Thu { set; get; }
        public string Ca { set; get; }
        public string GioVao { set; get; }
        public string GioRa { set; get; }
        public string Cong { set; get; }
        public string Gio { set; get; }
        public int? Tre { set; get; }
        public int? VeSom { set; get; }
        public int? VeTre { set; get; }
        public string TC1 { set; get; }
        public string TC2 { set; get; }
        public string TC3 { set; get; }
        public string TC4 { set; get; }
        public string TongGio { set; get; }
        public string DemCong { set; get; }
        public string KyHieu { set; get; }
        public string KyHieuPhu { set; get; }
        public string PhongBan { set; get; }
        public string MaNguoiDung { set; get; }
    }
}
