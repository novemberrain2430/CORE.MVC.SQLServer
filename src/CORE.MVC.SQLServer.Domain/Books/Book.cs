using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace CORE.MVC.SQLServer.Books
{
    public class Book : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
        public Guid AuthorId { get; set; }
        public virtual Guid? TenantId { get; set; }
        public Book()
        {

        }

    }
}
