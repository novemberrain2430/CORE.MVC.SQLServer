using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CORE.MVC.SQLServer.Migrations
{
    public partial class Created_CC_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "App_CC_Absents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_CC_Absents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "App_CC_CodeChamCongs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DiTre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VeSom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DungGio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TangCa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThieuGioVao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThieuGioRa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChuaXepCa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhepNam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Le = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CongTac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VeTre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CuoiTuan = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_CC_CodeChamCongs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "App_CC_Holidays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HeSo = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_CC_Holidays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "App_CC_InOuts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InOutType = table.Column<int>(type: "int", nullable: false),
                    IntOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthenType = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_CC_InOuts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "App_CC_Shifts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<int>(type: "int", nullable: false),
                    GioVaoLamViec = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GioKetThucLamViec = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GioBatDauNghiTrua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GioKetThucNghiTrua = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TongGioNghiTrua = table.Column<float>(type: "real", nullable: false),
                    TongGioTrongCaLamViec = table.Column<float>(type: "real", nullable: false),
                    CongTinh = table.Column<float>(type: "real", nullable: false),
                    BatDauVao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KetThucVao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BatDauRa = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KetThucRa = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KhongCoGioRa = table.Column<int>(type: "int", nullable: false),
                    KhongCoGioVao = table.Column<int>(type: "int", nullable: false),
                    XemCaDem = table.Column<bool>(type: "bit", nullable: true),
                    TinhBuTru = table.Column<bool>(type: "bit", nullable: true),
                    TruGioDiTre = table.Column<bool>(type: "bit", nullable: true),
                    TruGioVeSom = table.Column<bool>(type: "bit", nullable: true),
                    ChoPhepDiTre = table.Column<int>(type: "int", nullable: false),
                    BatDauTinhDiTre = table.Column<bool>(type: "bit", nullable: true),
                    ChoPhepVeSom = table.Column<int>(type: "int", nullable: false),
                    BatDauTinhVeSom = table.Column<bool>(type: "bit", nullable: true),
                    XemCaNayLaTangCa = table.Column<bool>(type: "bit", nullable: true),
                    TangCaMuc = table.Column<int>(type: "int", nullable: false),
                    XemCuoiTuanLaTangCa = table.Column<bool>(type: "bit", nullable: true),
                    TangCaCuoiTuanMuc = table.Column<int>(type: "int", nullable: false),
                    XemNgayLeLaTangCa = table.Column<bool>(type: "bit", nullable: true),
                    TangCaNgayLeMuc = table.Column<int>(type: "int", nullable: false),
                    TangCaTruocGioLamViec = table.Column<bool>(type: "bit", nullable: true),
                    SoPhutTangCaTruocGioLamViec = table.Column<int>(type: "int", nullable: false),
                    TangCaSauGioLamViec = table.Column<bool>(type: "bit", nullable: true),
                    SoPhutTangCaSauGioLamViec = table.Column<int>(type: "int", nullable: false),
                    TongGioLamDatDen = table.Column<bool>(type: "bit", nullable: true),
                    SoPhutTongGioLamDatDen = table.Column<int>(type: "int", nullable: false),
                    TangCaTruocGioLamViecDatDen = table.Column<int>(type: "int", nullable: false),
                    PhutTruTangCaTruoc = table.Column<int>(type: "int", nullable: false),
                    TangCaSauGioLamViecDatDen = table.Column<int>(type: "int", nullable: false),
                    PhutTruTangCaSau = table.Column<int>(type: "int", nullable: false),
                    GioiHanTangCa1 = table.Column<int>(type: "int", nullable: false),
                    GioiHanTangCa2 = table.Column<int>(type: "int", nullable: false),
                    GioiHanTangCa3 = table.Column<int>(type: "int", nullable: false),
                    GioiHanTangCa4 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_CC_Shifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "App_CC_TinhCongs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaChamCong = table.Column<int>(type: "int", nullable: false),
                    Ngay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Thu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GioVao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GioRa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tre = table.Column<int>(type: "int", nullable: true),
                    VeSom = table.Column<int>(type: "int", nullable: true),
                    VeTre = table.Column<int>(type: "int", nullable: true),
                    TC1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TC2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TC3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TC4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongGio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DemCong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KyHieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KyHieuPhu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhongBan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaNguoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_CC_TinhCongs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "App_CC_Weekends",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WeekendID = table.Column<int>(type: "int", nullable: false),
                    IsYes = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_CC_Weekends", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_App_CC_Absents_Name",
                table: "App_CC_Absents",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_App_CC_Holidays_Name",
                table: "App_CC_Holidays",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_App_CC_Shifts_Name",
                table: "App_CC_Shifts",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "App_CC_Absents");

            migrationBuilder.DropTable(
                name: "App_CC_CodeChamCongs");

            migrationBuilder.DropTable(
                name: "App_CC_Holidays");

            migrationBuilder.DropTable(
                name: "App_CC_InOuts");

            migrationBuilder.DropTable(
                name: "App_CC_Shifts");

            migrationBuilder.DropTable(
                name: "App_CC_TinhCongs");

            migrationBuilder.DropTable(
                name: "App_CC_Weekends");
        }
    }
}
