using JetBrains.Annotations;
using Volo.Abp.GlobalFeatures;

namespace Volo.CmsKit.GlobalFeatures
{
    public class GlobalCmsKitProFeatures : GlobalModuleFeatures
    {
        public const string ModuleName = "CmsKitPro";

        public NewslettersFeature Newsletter => GetFeature<NewslettersFeature>();

        public ContactFeature Contact => GetFeature<ContactFeature>();
        
        public GlobalCmsKitProFeatures([NotNull] GlobalFeatureManager featureManager)
            : base(featureManager)
        {
            AddFeature(new NewslettersFeature(this));
            AddFeature(new ContactFeature(this));
        }
    }
}
