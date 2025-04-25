namespace Photosoil.Exporter.Models.LinkModels
{
    public class SoilObjectTerm
    {
        public int SoilObjectsId { get;set; }
        public int TermsId { get;set; }
    }

    public class FileSoilObject
    {
        public int SoilObjectsId { get; set; }
        public int ObjectPhotoId { get; set; }
    }

    public class AuthorSoilObject
    {
        public int AuthorsId { get; set; }
        public int SoilObjectsId { get; set; }
    }
}
