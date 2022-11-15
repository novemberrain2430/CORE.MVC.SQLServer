using System;
using Sample.Shared;
using Volo.Abp.AutoMapper;
using Sample.Books;
using AutoMapper;

namespace Sample
{
    public class SampleApplicationAutoMapperProfile : Profile
    {
        public SampleApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<BookCreateDto, Book>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
            CreateMap<BookUpdateDto, Book>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
            CreateMap<Book, BookDto>();
        }
    }
}