using System;

namespace Volo.Abp.TextTemplateManagement
{
    public class TextTemplateManagementOptions
    {
        /// <summary>
        /// Default value: 1 Hour
        /// Gets or sets how long a cached content can be inactive (e.g. not accessed) before it will be removed.
        /// </summary>
        public TimeSpan MinimumCacheDuration { get; set; } = TimeSpan.FromHours(1);
    }
}