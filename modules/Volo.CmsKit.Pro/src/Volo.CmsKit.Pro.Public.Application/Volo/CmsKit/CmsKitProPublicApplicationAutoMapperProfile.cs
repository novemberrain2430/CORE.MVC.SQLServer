using AutoMapper;
using Volo.Abp.AutoMapper;
using Volo.CmsKit.Newsletters;
using Volo.CmsKit.Public.Newsletters;

namespace Volo.CmsKit
{
    public class CmsKitProPublicApplicationAutoMapperProfile : Profile
    {
        public CmsKitProPublicApplicationAutoMapperProfile()
        {
            CreateMap<NewsletterPreferenceDefinition, NewsletterPreferenceDetailsDto>()
                .Ignore(x => x.DisplayPreference)
                .Ignore(x => x.Definition)
                .Ignore(x => x.IsSelectedByEmailAddress);

            CreateMap<NewsletterPreferenceDefinition, NewsletterEmailOptionsDto>()
                .Ignore(x => x.PrivacyPolicyConfirmation)
                .Ignore(x => x.DisplayAdditionalPreferences)
                .Ignore(x => x.AdditionalPreferences);
        }
    }
}