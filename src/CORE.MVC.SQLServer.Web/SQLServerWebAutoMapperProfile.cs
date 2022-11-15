using CORE.MVC.SQLServer.Xamples;
using CORE.MVC.SQLServer.Samples;
using AutoMapper;
using CORE.MVC.SQLServer.Books;
using CORE.MVC.SQLServer.Authors;

namespace CORE.MVC.SQLServer.Web
{
    public class SQLServerWebAutoMapperProfile : Profile
    {
        public SQLServerWebAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Web project.

            CreateMap<SampleDto, SampleUpdateDto>();

            CreateMap<XampleDto, XampleUpdateDto>();
            CreateMap<BookDto, CreateUpdateBookDto>();
            CreateMap<Pages.Authors.CreateModalModel.CreateAuthorViewModel,
                    CreateAuthorDto>();
            CreateMap<AuthorDto, Pages.Authors.EditModalModel.EditAuthorViewModel>();
            CreateMap<Pages.Authors.EditModalModel.EditAuthorViewModel,
                      UpdateAuthorDto>();
        }
    }
}