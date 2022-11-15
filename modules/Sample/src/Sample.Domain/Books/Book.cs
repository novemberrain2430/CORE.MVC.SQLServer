using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace Sample.Books
{
    public class Book : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }

        [CanBeNull]
        public virtual string Code { get; set; }

        public Book()
        {

        }

        public Book(Guid id, string name, string code)
        {
            Id = id;
            Check.NotNull(name, nameof(name));
            Name = name;
            Code = code;
        }
    }
}