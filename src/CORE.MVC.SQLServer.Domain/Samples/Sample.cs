using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace CORE.MVC.SQLServer.Samples
{
    public class Sample : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        public virtual DateTime? Date1 { get; set; }

        public virtual int Year { get; set; }

        [CanBeNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string Email { get; set; }

        public virtual bool IsConfirm { get; set; }

        public virtual Guid UserId { get; set; }

        public Sample()
        {

        }

        public Sample(Guid id, string name, int year, string code, string email, bool isConfirm, Guid userId, DateTime? date1 = null)
        {
            Id = id;
            Check.NotNull(name, nameof(name));
            Check.Length(code, nameof(code), SampleConsts.CodeMaxLength, 0);
            Check.NotNull(email, nameof(email));
            Name = name;
            Year = year;
            Code = code;
            Email = email;
            IsConfirm = isConfirm;
            UserId = userId;
            Date1 = date1;
        }
    }
}