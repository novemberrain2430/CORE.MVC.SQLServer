using AutoMapper;
using Volo.Abp.AutoMapper;
using Volo.CmsKit.Admin.Newsletters;
using Volo.CmsKit.Admin.Tags;
using Volo.CmsKit.Newsletters;
using Volo.CmsKit.Tags;

namespace Volo.CmsKit
{
    public class CmsKitProAdminApplicationAutoMapperProfile : Profile
    {
        public CmsKitProAdminApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<NewsletterRecord, NewsletterRecordWithDetailsDto>()
                .ForMember(s => s.Preferences, c => c.MapFrom(m => m.Preferences));

            CreateMap<NewsletterPreference, NewsletterPreferenceDto>();

            CreateMap<NewsletterSummaryQueryResultItem, NewsletterRecordDto>();

            CreateMap<NewsletterSummaryQueryResultItem, NewsletterRecordCsvDto>()
                .Ignore(x => x.SecurityCode);

            CreateMap<TagDto, TagCreateDto>();

            CreateMap<TagDto, TagUpdateDto>();

            CreateMap<TagCreateDto, Tag>(MemberList.Source)
                .Ignore(x => x.Id);

            CreateMap<TagUpdateDto, Tag>(MemberList.Source);
        }
    }
}