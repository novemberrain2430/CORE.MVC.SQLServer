using CORE.MVC.SQLServer.Xamples;
using System;
using CORE.MVC.SQLServer.Shared;
using Volo.Abp.AutoMapper;
using CORE.MVC.SQLServer.Samples;
using AutoMapper;
using CORE.MVC.SQLServer.Users;
using CORE.MVC.SQLServer.User;
using CORE.MVC.SQLServer.Books;
using CORE.MVC.SQLServer.Authors;

namespace CORE.MVC.SQLServer
{
    public class SQLServerApplicationAutoMapperProfile : Profile
    {
        public SQLServerApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            //CreateMap<SampleCreateDto, Sample>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id).Ignore(x => x.TenantId);
            //CreateMap<SampleUpdateDto, Sample>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id).Ignore(x => x.TenantId);
            //CreateMap<Sample, SampleDto>();
            //CreateMap<AppUser, AppUserDto>().Ignore(x => x.ExtraProperties);
            //CreateMap<AppUser, AppUserDto>().Ignore(x => x.ExtraProperties);
            CreateMap<XampleCreateDto, Xample>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id).Ignore(x => x.TenantId);
            CreateMap<XampleUpdateDto, Xample>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id).Ignore(x => x.TenantId);
            CreateMap<Xample, XampleDto>();
            CreateMap<Book, BookDto>();
            CreateMap<CreateUpdateBookDto, Book>();
            CreateMap<Author, AuthorDto>();
        }
    }
}