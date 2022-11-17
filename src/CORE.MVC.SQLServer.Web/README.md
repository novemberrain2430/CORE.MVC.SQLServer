## How To Run Project
B1: Cài đặt ABP CLI
Open CMD by administrator và chạy câu lệnh: dotnet tool install -g Volo.Abp.Cli
B2: Đăng kí tài khoản ABP trên abp.io
B3.  login your account using the ABP CLI:
abp login <username>
It will ask a password, so you must enter the password of your account.
B4: Đổi chuỗi kết nối ở file appsettings.json trên Project CORE.MVC.SQLServer.DbMigrator và CORE.MVC.SQLServer.Web
B5: set default Start up project trên CORE.MVC.SQLServer.DbMigrator và run project để migrate DB.
B6: set default Start up project trên CORE.MVC.SQLServer.Web
B7: đăng nhập bằng tài khoản admin/1q2w3E*
