using System;
using Volo.Abp.DependencyInjection;

namespace Volo.CmsKit.Pro
{
    public class CmsKitProTestData : ISingletonDependency
    {
        public Guid NewsletterEmailId { get; } = Guid.NewGuid();

        public string Email { get; } = "info@abp.io";

        public string Preference { get; } = "Community";

        public string Source { get; } = "Community.ArticleRead";
    }
}