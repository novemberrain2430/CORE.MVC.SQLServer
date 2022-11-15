using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CORE.MVC.SQLServer.Data
{
    /* This is used if database provider does't define
     * ISQLServerDbSchemaMigrator implementation.
     */
    public class NullSQLServerDbSchemaMigrator : ISQLServerDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}