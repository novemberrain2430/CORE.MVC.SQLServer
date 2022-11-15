using System.Threading.Tasks;

namespace CORE.MVC.SQLServer.Data
{
    public interface ISQLServerDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}