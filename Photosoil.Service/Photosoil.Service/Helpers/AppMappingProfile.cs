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
using Newtonsoft.Json;
using Photosoil.Core.Models.Base;
using Photosoil.Service.Models;

namespace Photosoil.Service.Helpers
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<SoilObjectVM, SoilObject>()
                .ForMember(x => x.Authors, x => x.Ignore())
                .ForMember(x => x.Translations, x => x.Ignore())
                .ForMember(x => x.Terms, x => x.Ignore())
                .ForMember(x => x.ObjectPhoto, x => x.Ignore())
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

            CreateMap<SoilObject, SoilResponseById>()
                .ForMember(x => x.UserEmail, x => x.MapFrom(x=>x.User.Email ))
                ;

            CreateMap<News, NewsResponseById>()
    .ForMember(x => x.UserEmail, x => x.MapFrom(x => x.User.Email))
    ;

            CreateMap<EcoSystem, EcoSystemResponseById>()
.ForMember(x => x.UserEmail, x => x.MapFrom(x => x.User.Email))
;
            CreateMap<SoilObject, SoilObjectVM>()
                .ForMember(x=>x.Authors, x=>x.MapFrom(y=>y.Authors.Select(x=>x.Id)))
                .ForMember(x=>x.EcoSystems, x=>x.MapFrom(y=>y.EcoSystems.Select(x=>x.Id)))
                .ForMember(x=>x.Publications, x =>x.MapFrom(y=>y.Publications.Select(x=>x.Id)))
                .ForMember(x=>x.SoilTerms, x=>x.MapFrom(y=>y.Terms.Select(x=>x.Id)))
                .ForMember(x=>x.ObjectPhoto, x=>x.MapFrom(y=>y.ObjectPhoto.Select(x=>x.Id)))

                ;


            //CreateMap<SoilObjectVM, SoilObject>()
            //    .ForMember(x => x.PhotoId, x => x.MapFrom(y => y.PhotoId))
            //    .ForMember(x => x.Translations, x => x.Ignore())
            //    .ForMember(x => x.Photo, x => x.Ignore())
            //    .ForMember(x => x.ObjectPhoto, x => new List<Core.Models.File>())
            //    .ForMember(x => x.Publications, x => new List<Publication>())
            //    .ForMember(x => x.EcoSystems, x => new List<EcoSystem>())
            //    .ForMember(x => x.Terms, x => new List<Term>())
            //    .ForMember(x => x.Authors, x => new List<Author>());




            CreateMap<SoilResponse,SoilObject>().ReverseMap()
                .ForMember(x=>x.Authors, x=>x.MapFrom(x=>x.Authors.Select(x=>x.Id)))
                .ForMember(x => x.Terms, x => x.MapFrom(x => x.Terms.Select(x=>x.Id))
                );


            CreateMap<EcoSystemResponseAll, EcoSystem>().ReverseMap()
                .ForMember(x => x.Authors, x => x.MapFrom(x => x.Authors.Select(x => x.Id))
                );



            CreateMap<EcoSystem, EcoSystemResponse>().ReverseMap();

            CreateMap<Term, TermsResponse>();


            CreateMap<Article, ArticleVM>();
            CreateMap<Classification, ClassificationVM>().ReverseMap()
                .ForMember(x => x.Terms, x => x.Ignore());

            CreateMap<EcoSystemVM, EcoSystem>()
                .ForMember(x => x.Authors, x => x.Ignore())
                .ForMember(x => x.PhotoId, x => x.Ignore())
                .ForMember(x => x.Photo, x => x.Ignore())
                .ForMember(x => x.Translations, x => x.Ignore())
                .ForMember(x => x.ObjectPhoto, x => x.Ignore())
                .ForMember(x => x.PhotoId, x => x.MapFrom(y=>y.PhotoId))
                .ForMember(x => x.Publications, x => x.Ignore())
                .ForMember(x => x.SoilObjects, x => x.Ignore());

            CreateMap<EcoSystem, EcoSystemVM>()
                .ForMember(x => x.ObjectPhoto, x => x.MapFrom(y => y.ObjectPhoto.Select(x => x.Id)))
                .ForMember(x => x.Authors, x => x.MapFrom(y => y.Authors.Select(x => x.Id)))
                .ForMember(x => x.Publications, x => x.MapFrom(y => y.Publications.Select(x => x.Id)))
                .ForMember(x => x.SoilObjects, x => x.MapFrom(y => y.SoilObjects.Select(x => x.Id)))
                .ForMember(x => x.PhotoId, x => x.MapFrom(y => y.PhotoId));


            CreateMap<News, NewsVM>()
                .ForMember(x => x.ObjectPhoto, x => x.MapFrom(y => y.ObjectPhoto.Select(x => x.Id)))
                .ForMember(x => x.Files, x => x.MapFrom(y => y.Files.Select(x => x.Id)))
                .ForMember(x => x.Tags, x => x.MapFrom(y => y.Tags.Select(x => x.Id)));
            CreateMap<RulesVM, Rules>()
                .ForMember(x => x.Files, x => x.Ignore());
            CreateMap<NewsVM, News>()
                .ForMember(x => x.ObjectPhoto, x => x.Ignore())
                .ForMember(x => x.Tags, x => x.Ignore())
                .ForMember(x => x.Files, x => x.Ignore());

            CreateMap<EcoSystem, EcoSystem>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.ObjectPhoto, x => new List<Core.Models.File>())
                .ForMember(x => x.Authors, x => new List<Author>())
                .ForMember(x => x.SoilObjects, x => new List<SoilObject>())
                .ForMember(x => x.Publications, x => new List<Publication>());



            CreateMap<Publication, PublicationVM>()
                .ForMember(x => x.EcoSystems, x => x.MapFrom(y => y.EcoSystems.Select(x => x.Id)))
                .ForMember(x => x.SoilObjects, x => x.MapFrom(y => y.SoilObjects.Select(x => x.Id)))
                .ForMember(x => x.FileId, x => x.MapFrom(x => x.FileId));

            CreateMap<PublicationVM, Publication>()
                //.ForMember(x => x.Coordinates, x => x.MapFrom(y => JsonConvert.SerializeObject(y.Coordinates)))
                .ForMember(x => x.FileId, x => x.Ignore())
                .ForMember(x => x.File, x => x.Ignore())
                .ForMember(x => x.EcoSystems, x => x.Ignore())
                .ForMember(x => x.SoilObjects, x => x.Ignore());
            CreateMap<Publication, PublicationResponseById>()
                                .ForMember(x => x.File, x => x.MapFrom(x => x.File))

                //.ForMember(x => x.Coordinates, x => x.MapFrom(y => JsonConvert.DeserializeObject<Coordinate[]>(y.Coordinates)))
                .ForMember(x => x.UserEmail, x => x.MapFrom(x=>x.User.Email ))
                .ForMember(x => x.EcoSystems, x => x.MapFrom(x=>x.EcoSystems))
                .ForMember(x => x.SoilObjects, x => x.MapFrom(x=>x.SoilObjects));

            CreateMap<Publication, PublicationResponse>();
                //.ForMember(x => x.Coordinates, x => x.MapFrom(y => JsonConvert.DeserializeObject<Coordinate[]>(y.Coordinates)));


            CreateMap<Term, TermsVM>().ReverseMap()
                .ForMember(x => x.Classification, x => x.Ignore());
            CreateMap<ArticleVM, Article>()
                .ForMember(x => x.Photo, x => x.Ignore())
                .ForMember(x => x.PhotoId, x => x.Ignore());
            CreateMap<AuthorVM, Author>()
                .ForMember(x => x.OtherProfiles, x => x.MapFrom(y => JsonConvert.SerializeObject(y.OtherProfiles)))
                .ForMember(x => x.Contacts, x => x.MapFrom(y => JsonConvert.SerializeObject(y.Contacts)))
                .ForMember(x => x.PhotoId, x => x.Ignore());




            CreateMap<Author, AuthorResponseById>()
                .ForMember(x => x.DataRu, x => x.MapFrom(y => y.DataRu))
                .ForMember(x => x.DataEng, x => x.MapFrom(y => y.DataEng))
                .ForMember(x => x.EcoSystems, x => x.MapFrom(y => y.EcoSystems))
                .ForMember(x => x.SoilObjects, x => x.MapFrom(y => y.SoilObjects))
                .ForMember(x => x.OtherProfiles, x => x.MapFrom(y => JsonConvert.DeserializeObject<string[]>(y.OtherProfiles)))
                .ForMember(x => x.Contacts, x => x.MapFrom(y => JsonConvert.DeserializeObject<string[]>(y.Contacts)));

            CreateMap<Author, AuthorResponse>()
                .ForMember(x => x.DataRu, x => x.MapFrom(y => y.DataRu))
                .ForMember(x => x.DataEng, x => x.MapFrom(y => y.DataEng))
                .ForMember(x => x.OtherProfiles, x => x.MapFrom(y => JsonConvert.DeserializeObject<string[]>(y.OtherProfiles)))
                .ForMember(x => x.Contacts, x => x.MapFrom(y => JsonConvert.DeserializeObject<string[]>(y.Contacts)))
                .ForMember(x => x.PhotoId, x => x.Ignore());

        }
    }
}
