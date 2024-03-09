using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photosoil.Core.Models;
using Photosoil.Service.Helpers.ViewModel;
using Photosoil.Core.Models.Second;
using Photosoil.Service.Helpers.ViewModel.Response;
using Photosoil.Service.Helpers.ViewModel.Request;

namespace Photosoil.Service.Helpers
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<SoilObject, SoilObjectVM>().ReverseMap()
                .ForMember(x => x.Terms, x => x.Ignore())
                .ForMember(x => x.Publications, x => x.Ignore())
                .ForMember(x => x.EcoSystems, x => x.Ignore())
                .ForMember(x => x.Photo, x => x.Ignore())
                .ForMember(x => x.PhotoId, x => x.Ignore());
            ;
            CreateMap<SoilObject, SoilMass>().ReverseMap()
                .ForMember(x => x.Photo, x => x.Ignore())
                .ForMember(x => x.PhotoId, x => x.Ignore());



            CreateMap<Classification, ClassificationVM>().ReverseMap()
                .ForMember(x => x.Terms, x => x.MapFrom(x => x.Terms));



            CreateMap<SoilObject, SoilObjectResponse>().ReverseMap()
                .ForMember(x => x.Terms, x => x.MapFrom(x => x.Classification));
            CreateMap<EcoSystem, EcoSystemResponse>().ReverseMap();
            CreateMap<Publication, PublicationResponse>().ReverseMap();

            CreateMap<Term, TermsResponse>();


            CreateMap<Article, ArticleVM>();
            CreateMap<Classification, ClassificationVM>().ReverseMap()
                .ForMember(x => x.Terms, x => x.Ignore());

            CreateMap<EcoSystemVM, EcoSystem>()
                .ForMember(x => x.PhotoId, x => x.Ignore())
                .ForMember(x => x.Photo, x => x.Ignore())
                .ForMember(x => x.Publications, x => x.Ignore())
                .ForMember(x => x.SoilObjects, x => x.Ignore());

            CreateMap<PublicationVM, Publication>()
                .ForMember(x => x.FileId, x => x.Ignore())
                .ForMember(x => x.File, x => x.Ignore())
                .ForMember(x => x.EcoSystems, x => x.Ignore())
                .ForMember(x => x.SoilObjects, x => x.Ignore());


            CreateMap<Term, TermsVM>().ReverseMap()
                .ForMember(x => x.Classification, x => x.Ignore());
            CreateMap<ArticleVM, Article>()
                .ForMember(x => x.Photo, x => x.Ignore())
                .ForMember(x => x.PhotoId, x => x.Ignore());
            CreateMap<AuthorVM, Author>()
                .ForMember(x => x.Photo, x => x.Ignore())
                .ForMember(x => x.PhotoId, x => x.Ignore());

        }
    }
}
