using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace CORE.MVC.SQLServer.TinhCongs
{
    public class TinhCongDto : Entity<long>
    {
        [Required]
        public string MaNhanVien { set; get; }
        [Required]
        public string TenNhanVien { set; get; }
        public int MaChamCong { set; get; }
        public DateTime? Ngay { set; get; } = DateTime.Now;
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
