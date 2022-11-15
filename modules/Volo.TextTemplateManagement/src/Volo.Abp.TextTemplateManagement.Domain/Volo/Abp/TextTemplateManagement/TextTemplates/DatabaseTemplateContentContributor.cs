using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.TextTemplating;

namespace Volo.Abp.TextTemplateManagement.TextTemplates
{
    public class DatabaseTemplateContentContributor : ITemplateContentContributor, ITransientDependency
    {
        protected readonly ITextTemplateContentRepository ContentRepository;
        protected readonly IDistributedCache<string, TemplateContentCacheKey> Cache;

        protected readonly TextTemplateManagementOptions Options;
        
        public DatabaseTemplateContentContributor(
            ITextTemplateContentRepository contentRepository, 
            IDistributedCache<string, TemplateContentCacheKey> cache, 
            IOptions<TextTemplateManagementOptions> options)
        {
            ContentRepository = contentRepository;
            Cache = cache;
            Options = options.Value;
        }

        public virtual async Task<string> GetOrNullAsync(TemplateContentContributorContext context)
        {
            return await Cache.GetOrAddAsync(
                new TemplateContentCacheKey(context.TemplateDefinition.Name, context.Culture),
                async () => await GetTemplateContentFromDbOrNullAsync(context),
                () => new DistributedCacheEntryOptions
                {
                    SlidingExpiration = Options.MinimumCacheDuration
                }
            );
        }

        protected virtual async Task<string> GetTemplateContentFromDbOrNullAsync(TemplateContentContributorContext context)
        {
            var template = await ContentRepository.FindAsync(
                context.TemplateDefinition.Name,
                context.Culture
            );

            return template?.Content;
        }
    }
}