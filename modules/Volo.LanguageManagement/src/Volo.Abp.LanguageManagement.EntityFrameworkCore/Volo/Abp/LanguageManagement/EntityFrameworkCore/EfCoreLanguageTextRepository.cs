using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace Volo.Abp.LanguageManagement.EntityFrameworkCore
{
    public class EfCoreLanguageTextRepository : EfCoreRepository<ILanguageManagementDbContext, LanguageText, Guid>, ILanguageTextRepository
    {
        public EfCoreLanguageTextRepository(IDbContextProvider<ILanguageManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual List<LanguageText> GetList(
            string resourceName,
            string cultureName)
        {
            //GetList should be sync because DynamicResourceLocalizer must use it in a sync way!
#pragma warning disable 618
            using (Volo.Abp.Uow.UnitOfWorkManager.DisableObsoleteDbContextCreationWarning.SetScoped(true))
            {
                return DbSet
                    .Where(l => l.ResourceName == resourceName && l.CultureName == cultureName)
                    .ToList();
            }
#pragma warning restore 618
        }
    }
}
