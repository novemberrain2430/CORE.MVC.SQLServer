using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace CORE.MVC.SQLServer.Shifts
{
    public class Shift : Entity<long>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }
        public int Name { set; get; }
        public DateTime? GioVaoLamViec { set; get; }
        public DateTime? GioKetThucLamViec { set; get; }
        public DateTime? GioBatDauNghiTrua { set; get; }
        public DateTime? GioKetThucNghiTrua { set; get; }
        public float TongGioNghiTrua { set; get; }
        public float TongGioTrongCaLamViec { set; get; }
        public float CongTinh { set; get; }
        public DateTime? BatDauVao { set; get; }
        public DateTime? KetThucVao { set; get; }
        public DateTime? BatDauRa { set; get; }
        public DateTime? KetThucRa { set; get; }
        public int KhongCoGioRa { set; get; }
        public int KhongCoGioVao { set; get; }
        public bool? XemCaDem { set; get; }
        public bool? TinhBuTru { set; get; }
        public bool? TruGioDiTre { set; get; }
        public bool? TruGioVeSom { set; get; }
        public int ChoPhepDiTre { set; get; }
        public bool? BatDauTinhDiTre { set; get; }
        public int ChoPhepVeSom { set; get; }
        public bool? BatDauTinhVeSom { set; get; }
        public bool? XemCaNayLaTangCa { set; get; }
        public int TangCaMuc { set; get; }
        public bool? XemCuoiTuanLaTangCa { set; get; }
        public int TangCaCuoiTuanMuc { set; get; }
        public bool? XemNgayLeLaTangCa { set; get; }
        public int TangCaNgayLeMuc { set; get; }
        public bool? TangCaTruocGioLamViec { set; get; }
        public int SoPhutTangCaTruocGioLamViec { set; get; }
        public bool? TangCaSauGioLamViec { set; get; }
        public int SoPhutTangCaSauGioLamViec { set; get; }
        public bool? TongGioLamDatDen { set; get; }
        public int SoPhutTongGioLamDatDen { set; get; }
        public int TangCaTruocGioLamViecDatDen { set; get; }
        public int PhutTruTangCaTruoc { set; get; }
        public int TangCaSauGioLamViecDatDen { set; get; }
        public int PhutTruTangCaSau { set; get; }
        public int GioiHanTangCa1 { set; get; }
        public int GioiHanTangCa2 { set; get; }
        public int GioiHanTangCa3 { set; get; }
        public int GioiHanTangCa4 { set; get; }
    }
}
