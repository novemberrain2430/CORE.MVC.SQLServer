using Sample.Books;
using AutoMapper;

namespace Sample.Web
{
    public class SampleWebAutoMapperProfile : Profile
    {
        public SampleWebAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<BookDto, BookUpdateDto>();
        }
    }
}