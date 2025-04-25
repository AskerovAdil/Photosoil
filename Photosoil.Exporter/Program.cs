using Microsoft.AspNetCore.Http;
using Mysqlx.Crud;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json;
using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using Photosoil.Core.Models.Second;
using Photosoil.Exporter.Data;
using Photosoil.Exporter.Enum;
using Photosoil.Exporter.Models.LinkModels;
using Photosoil.Service.Helpers.ViewModel.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Photosoil.Exporter
{
    internal class Program
    {
        //public async Task<string> CompressAndSaveImageAsync(IFormFile imageFile, string path, int quality = 10)
        //{
        //    string compressedPath = Path.Combine(
        //        Path.GetDirectoryName(path),
        //        $"resize-{Path.GetFileNameWithoutExtension(path)}{Path.GetExtension(path)}"
        //    ).Replace("\\", "/");
        //
        //    var imageStream = imageFile.OpenReadStream();
        //    var image = Image.Load(imageStream);
        //    var encoder = new JpegEncoder
        //    {
        //        Quality = quality,
        //    };
        //
        //    await Task.Run(() => image.Save(compressedPath, encoder));
        //    return compressedPath;
        //}
        static void Main()
        {

            using (var db = new GluceverDb())
            {
                var soilsGroup = db.node.Where(x => x.type == "ground").GroupBy(x => x.tnid == 0 ? x.nid : x.tnid).ToList();
                var authors = db.node.Where(x => x.type == "author").GroupBy(x => x.tnid == 0 ? x.nid : x.tnid).ToList();

                var classificationList = db.taxonomy_vocabulary.Where(x=>x.vid != 2 && x.vid != 3 && x.vid != 14 && x.vid != 1 && x.vid != 13).ToList();
                var termList = db.taxonomy_term_data.Where(x => x.vid != 2 && x.vid != 3 && x.vid != 14 && x.vid != 1 && x.vid != 13).ToList();
                var translationId = 1;

                // var pictureList = db.file_managed.Where(x => x.type == "image").ToList();

                var photoList = db.field_revision_field_photos.GroupBy(x=>x.field_photos_fid).ToList();
                var pictureList = db.field_revision_field_pic.GroupBy(x => x.field_pic_fid).ToList();
                var pictureAuthorList = db.field_revision_field_picture.ToList();

                #region Pictures
                var pictureRequest = new List<Core.Models.File>();
                var photosRequest = new List<Core.Models.File>();
                foreach (var el in pictureList)
                {
                    var titleEng = "";
                    var titleRu = "";
                    //Айдишники сущностей к котором принаджит, нужно в почвах
                    var entityIds = new List<int>();
                    foreach (var photo in el)
                    {
                        var lang = db.node.FirstOrDefault(x => x.nid == photo.entity_id)?.language;
                        if (lang == "ru") titleRu = photo.field_pic_title;
                        else titleEng = photo.field_pic_title;
                        entityIds.Add((int)photo.entity_id);
                    }
                    var fid = el.First().field_pic_fid;
                    var filename = Path.GetFileName(db.file_managed.FirstOrDefault(x => x.fid == fid)?.uri);

                    var path = $"/Storage/olddata/{filename}";
                    pictureRequest.Add(new Core.Models.File(path, path, titleEng, titleRu) { Id = (int)fid, entity_ids = entityIds });
                }
                foreach (var el in photoList)
                {
                    var titleEng = "";
                    var titleRu = "";
                    var entityIds = new List<int>();
                    foreach (var photo in el)
                    {
                        var lang = db.node.FirstOrDefault(x => x.nid == photo.entity_id)?.language;
                        if (lang == "ru") titleRu = photo.field_photos_title;
                        else titleEng = photo.field_photos_title;
                        entityIds.Add((int)photo.entity_id);
                    }
                    var fid = el.First().field_photos_fid;
                    var filename = Path.GetFileName(db.file_managed.FirstOrDefault(x => x.fid == fid)?.uri);
                    var path = $"/Storage/olddata/{filename}";

                    photosRequest.Add(new Core.Models.File(path, path, titleEng, titleRu) { Id = (int)fid, entity_ids = entityIds });
                }

                foreach (var el in pictureAuthorList)
                {
                    var titleEng = "";
                    var titleRu = "";
                    var entityIds = new List<int>() { (int)el.entity_id};

                    var fid = el.field_picture_fid;
                    var filename = Path.GetFileName(db.file_managed.FirstOrDefault(x => x.fid == fid)?.uri);
                    var path = $"/Storage/olddata/{filename}";

                    photosRequest.Add(new Core.Models.File(path, path, titleEng, titleRu) { Id = (int)fid, entity_ids = entityIds });
                }

                #endregion

                #region Taxonomia

                var classificationRequest = new List<Classification>();
                var order = 1;
                foreach (var classification in classificationList)
                {
                    string nameEng = null;
                    var translationMode = TranslationMode.OnlyRu;
                    switch (classification.name)
                    {
                        case "Природная зона":
                            nameEng = "Natural zone";
                            translationMode = TranslationMode.Neutral;
                            break;
                        case "Основные квалификаторы (WRB 2014)":
                            translationMode = TranslationMode.Neutral;
                            nameEng = "Principal qualifiers (WRB 2014)";
                            break;
                        case "Реферативные почвенные группы (WRB 2014)":
                            translationMode = TranslationMode.Neutral;
                            nameEng = "Reference Soil Groups (WRB 2014)";
                            break;
                    }

                    var classificationReq = new Classification() 
                    {
                        Id = (int)classification.vid,
                        IsMulti = true,
                        TranslationMode = translationMode,
                        Order = order,
                        NameRu = classification.name,
                        NameEng = nameEng
                    };
                    classificationRequest.Add(classificationReq);
                    order++;
                }

                var termRequest = new List<Term>();
                order = 1;
                foreach (var term in termList)
                {
                    var nameEng = TermTranslation.ReferenceSoilGroups.FirstOrDefault(x => x.Key == term.name).Value;
                    var termReq = new Term()
                    {
                        Id = (int)term.tid,
                        ClassificationId = (int)term.vid,
                        NameRu = term.name,
                        NameEng = nameEng,
                        Order = order
                    };
                    termRequest.Add(termReq);
                    order++;
                }

                #endregion


                #region Authors
                var authorsRequest = new List<Author>();
                var translationRequest = new List<Translation>();
                translationId = 1;
                foreach (var authorGroup in authors)
                {
                    var nodeId = authorGroup.First().nid;
                    var authorDate = authorGroup.First();
                    var authorRu = authorGroup.FirstOrDefault(x=>x.language == "ru");
                    var photoId = photosRequest
                        .FirstOrDefault(photo => photo.entity_ids.Contains((int)authorRu.nid));

                    var authorReq = new Author()
                    {
                        Id = (int)nodeId,
                        PhotoId = photoId?.Id,
                        CreatedDate = UnixTimeToDateTime(authorDate.created),
                        AuthorType = AuthorType.Author,
                    };

                    foreach (var author in authorGroup)
                    {
                        translationRequest.Add(
                            new Translation()
                            {
                                Id = translationId,
                                Name = author.title,
                                Description = db.field_data_body.FirstOrDefault(x => x.entity_id == author.nid)?.body_value
                            });

                        if (author.language == "ru") {authorReq.DataRuId = translationId; authorReq.DataEngId = translationId; }
                        else { authorReq.DataEngId = translationId; }

                        translationId++;
                    }
                    authorsRequest.Add(authorReq);
                }

                #endregion

                #region SoilsObject
                var soilRequests = new List<SoilObject>();
                var soilTranslationRequests = new List<SoilTranslation>();
                var soilObjectTermRequests = new List<SoilObjectTerm>();
                var fileSoilObjectRequests = new List<FileSoilObject>();
                var authorSoilObjectRequests = new List<AuthorSoilObject>(); 
                translationId = 1;
                foreach (var soilGroup in soilsGroup)
                {
                    var soilId = (int)soilGroup.First().nid;
                    var soilDate = soilGroup.First();

                    var translations = new List<Core.Models.Second.SoilTranslation>();
                    foreach (var soil in soilGroup)
                    {
                        if (soil.title is null || soil.title == "")
                        {
                            Console.WriteLine(soil.nid);
                            throw new ArgumentException();

                        }
                        soilTranslationRequests.Add(new SoilTranslation()
                        {
                            Id = translationId,
                            LastUpdated = UnixTimeToDateTime(soilDate.changed),
                            Name = soil.title,
                            IsEnglish = soil.language == "ru" ? false : true,
                            IsVisible = true,
                            GeographicLocation = db.field_data_field_position.FirstOrDefault(x => x.entity_id == soil.nid)?.field_position_value,
                            ReliefLocation = db.field_data_field_relief_position.FirstOrDefault(x => x.entity_id == soil.nid)?.field_relief_position_value,
                            PlantCommunity = db.field_data_field_flora.FirstOrDefault(x => x.entity_id == soil.nid)?.field_flora_value,
                            SoilFeatures = db.field_data_field_flora_details.FirstOrDefault(x => x.entity_id == soil.nid)?.field_flora_details_value,
                            AssociatedSoilComponents = db.field_data_field_flora_stuff.FirstOrDefault(x => x.entity_id == soil.nid)?.field_flora_stuff_value,
                            Comments = db.field_data_field_comments.FirstOrDefault(x => x.entity_id == soil.nid)?.field_comments_value,
                            Code = db.field_data_field_code.FirstOrDefault(x => x.entity_id == soil.nid)?.field_code_value,
                            SoilId = soilId
                        });
                        translationId++;
                    }
                    //Авторы
                    var nodeId = soilGroup.First().nid;
                    var authorIds = db.field_revision_field_author.Where(x => x.entity_id == nodeId)?.ToList().Select(x=>x.field_author_target_id);
                    var authorNames = db.node.Where(x => authorIds.Contains(x.nid))?.ToList().Select(x=>x.title);

                    //Термины
                    var termsAllIds = new List<int>();
                    termsAllIds.AddRange(db.field_revision_field_env_zone.Where(x => x.entity_id == nodeId)?.Select(x => (int)x.field_env_zone_tid).AsEnumerable());
                    termsAllIds.AddRange(db.field_revision_field_0408rus.Where(x => x.entity_id == nodeId)?.Select(x => (int)x.field_0408rus_tid).AsEnumerable());
                    termsAllIds.AddRange(db.field_revision_field_0408rus_types.Where(x => x.entity_id == nodeId)?.Select(x => (int)x.field_0408rus_types_tid).AsEnumerable());
                    termsAllIds.AddRange(db.field_revision_field_0408rus_subtypes.Where(x => x.entity_id == nodeId)?.Select(x => (int)x.field_0408rus_subtypes_tid).AsEnumerable());
                    termsAllIds.AddRange(db.field_revision_field_ref_wrb_14.Where(x => x.entity_id == nodeId)?.Select(x => (int)x.field_ref_wrb_14_tid).AsEnumerable());
                    termsAllIds.AddRange(db.field_revision_field_main_wrb_14.Where(x => x.entity_id == nodeId)?.Select(x => (int)x.field_main_wrb_14_tid).AsEnumerable());

                    //Связи почвы и термины
                    soilObjectTermRequests.AddRange(termsAllIds.Select(x => new SoilObjectTerm() { SoilObjectsId = soilId, TermsId = x }));
                    var element_type = (int)db.field_revision_field_element_type.FirstOrDefault(x => x.entity_id == nodeId)?.field_element_type_tid;
                    SoilObjectType typeSoilObject;
                    if (element_type == 4)
                        typeSoilObject = SoilObjectType.SoilDynamics;
                    else if (element_type == 3)
                        typeSoilObject = SoilObjectType.SoilMorphologicalElements;
                    else
                        typeSoilObject = SoilObjectType.SoilProfiles;

                    //Связи почвы и фоток
                    var photoIds = photosRequest.Where(x => x.entity_ids.Contains(soilId)).ToList();
                    fileSoilObjectRequests.AddRange(photoIds.Select(x => new FileSoilObject() { SoilObjectsId = soilId, ObjectPhotoId = x.Id }));

                    //Основное фото почвы
                    var pictureId = pictureRequest.FirstOrDefault(x => x.entity_ids.Contains(soilId));

                    //Авторы
                    var trs = translationRequest.Where(x => authorNames.Contains(x.Name)).Select(x=> x.Id).ToList();
                    var authorRequestList = authorsRequest.Where(x => trs.Contains(x.DataEngId.Value) || trs.Contains(x.DataRuId.Value));
                    authorSoilObjectRequests.AddRange(authorRequestList.Select(x => new AuthorSoilObject() { AuthorsId = x.Id, SoilObjectsId = soilId }).ToList());

                    var soilReq = new SoilObject()
                    {
                        Id = soilId,
                        CreatedDate = UnixTimeToDateTime(soilDate.created),
                        //Authors = authorsRequest.Where(x => authorNames.Contains(x.DataRu?.Name) || authorNames.Contains(x.DataEng?.Name)).ToList(),
                        Latitude = db.field_data_field_location.FirstOrDefault(x => x.entity_id == nodeId)?.field_location_lat.ToString().Replace(',', '.'),
                        Longtitude = db.field_data_field_location.FirstOrDefault(x => x.entity_id == nodeId)?.field_location_lon.ToString().Replace(',', '.'),
                        //Terms = termsAll,
                        ObjectType = typeSoilObject,
                        PhotoId = pictureId.Id,
                        //ObjectPhoto = photoIds
                    };
                    soilRequests.Add(soilReq);
                }

                //23503: insert or update on table "SoilObjectTerm" violates foreign key constraint "FK_SoilObjectTerm_Term_TermsId"
                //DETAIL: Detail redacted as it may contain sensitive data. Specify 'Include Error Detail' in the connection string to include this information.

                var listTerm = new List<Term>();
                var listSooil = new List<SoilObject>();
                foreach (var el in soilObjectTermRequests)
                {
                    var term12 = termRequest.FirstOrDefault(x => x.Id == el.TermsId);
                    var soil = soilRequests.FirstOrDefault(x => x.Id == el.SoilObjectsId);
                    if (soil == null)
                        listSooil.Add(new SoilObject() { Id = el.SoilObjectsId});
                    if (term12 == null)
                        listTerm.Add(new Term() { Id = el.TermsId });
                }

                var listAuthor = new List<Author>();
                var listSoilAuthor = new List<SoilObject>();
                foreach (var el in authorSoilObjectRequests)
                {
                    var author = authorsRequest.FirstOrDefault(x => x.Id == el.AuthorsId);
                    var soil = soilRequests.FirstOrDefault(x => x.Id == el.SoilObjectsId);
                    if (soil == null)
                        listSoilAuthor.Add(new SoilObject() { Id = el.SoilObjectsId });
                    if (author == null)
                        listAuthor.Add(new Author() { Id = el.AuthorsId});
                }

                #endregion
                System.IO.File.WriteAllText("D:\\Photosoil\\Photosoil.API\\wwwroot\\SeedData\\soilTranslation.json", JsonConvert.SerializeObject(soilTranslationRequests));
                System.IO.File.WriteAllText("D:\\Photosoil\\Photosoil.API\\wwwroot\\SeedData\\authorTranslation.json", JsonConvert.SerializeObject(translationRequest));
                System.IO.File.WriteAllText("D:\\Photosoil\\Photosoil.API\\wwwroot\\SeedData\\soils.json", JsonConvert.SerializeObject(soilRequests));
                System.IO.File.WriteAllText("D:\\Photosoil\\Photosoil.API\\wwwroot\\SeedData\\authors.json", JsonConvert.SerializeObject(authorsRequest));
                System.IO.File.WriteAllText("D:\\Photosoil\\Photosoil.API\\wwwroot\\SeedData\\classification.json", JsonConvert.SerializeObject(classificationRequest));
                System.IO.File.WriteAllText("D:\\Photosoil\\Photosoil.API\\wwwroot\\SeedData\\terms.json", JsonConvert.SerializeObject(termRequest));
                System.IO.File.WriteAllText("D:\\Photosoil\\Photosoil.API\\wwwroot\\SeedData\\Pictures.json", JsonConvert.SerializeObject(pictureRequest.Concat(photosRequest)));
                System.IO.File.WriteAllText("D:\\Photosoil\\Photosoil.API\\wwwroot\\SeedData\\ExcludentTerm.json", JsonConvert.SerializeObject(listTerm));
                System.IO.File.WriteAllText("D:\\Photosoil\\Photosoil.API\\wwwroot\\SeedData\\ExcludentSoil.json", JsonConvert.SerializeObject(listSooil));
                System.IO.File.WriteAllText("D:\\Photosoil\\Photosoil.API\\wwwroot\\SeedData\\ExcludentAuthor.json", JsonConvert.SerializeObject(listAuthor));
                System.IO.File.WriteAllText("D:\\Photosoil\\Photosoil.API\\wwwroot\\SeedData\\ExcludentSoilAuthor.json", JsonConvert.SerializeObject(listSoilAuthor));

                //Link tables
                System.IO.File.WriteAllText("D:\\Photosoil\\Photosoil.API\\wwwroot\\SeedData\\Link\\soilObjectTerm.json", JsonConvert.SerializeObject(soilObjectTermRequests));
                System.IO.File.WriteAllText("D:\\Photosoil\\Photosoil.API\\wwwroot\\SeedData\\Link\\fileSoilObject.json", JsonConvert.SerializeObject(fileSoilObjectRequests));
                System.IO.File.WriteAllText("D:\\Photosoil\\Photosoil.API\\wwwroot\\SeedData\\Link\\authorSoilObject.json", JsonConvert.SerializeObject(authorSoilObjectRequests)); 
            }


        }
        public static long UnixTimeToDateTime(long unixTime)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTime).ToLocalTime(); // Преобразует в локальное время
            return unixTime;
        }

        private Core.Models.Second.SoilTranslation FillSoilTranslation(GluceverDb db)
        {

            return null;
        }

    }
}