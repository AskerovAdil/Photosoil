using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using Newtonsoft.Json;

namespace Photosoil.Core.Models
{
    public class File
    {
        public File(){}

        public File(string path, string titleEng, string titleRu)
        {
            Path = path;
            TitleEng = titleEng;
            TitleRu= titleRu;
            LastUpdated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
        public File(string path, string pathResize, string titleEng, string titleRu)
        {
            Path = path;
            PathResize = pathResize;
            TitleEng = titleEng;
            TitleRu = titleRu;
            LastUpdated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Path { get; set; }
        public string? PathResize { get; set; }
        public string? TitleEng { get; set; }
        public string? TitleRu { get; set; }
        public long LastUpdated { get; set; }

        [JsonIgnore]
        public List<News> NewsFiles { get; set; } = new();
        [JsonIgnore]
        public List<News> NewsPhoto { get; set; } = new();

        [JsonIgnore]
        public List<SoilObject> SoilObjects { get; set; } = new();

        [JsonIgnore]
        public List<EcoSystem> EcoSystems { get; set; } = new();

        [JsonIgnore]
        public List<Rules> Rules { get; set; } = new();


        [NotMapped]
        public string FileName
        {
            get
            {
                return string.IsNullOrEmpty(Path) ? "Без_названия" : Path.Split('/').Last();
            }
        }

        [NotMapped]
        public long? TakenDate
        {
            get
            {
                if (string.IsNullOrEmpty(Path))
                    return null;

                var takenDate = ExtractTakenDateFromExif(Path);
                var offset = new DateTimeOffset(takenDate.Value, TimeSpan.Zero);
                long unixTimestamp = offset.ToUnixTimeSeconds();

                return unixTimestamp;
            }
        }

        private DateTime? ExtractTakenDateFromExif(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return null;

            var extension = System.IO.Path.GetExtension(imagePath).ToLowerInvariant();
            if (extension is not (".jpg" or ".jpeg" or ".tiff" or ".png"))
                return null;

            try
            {
                if (!System.IO.File.Exists(imagePath))
                    return null;

                var directories = ImageMetadataReader.ReadMetadata(imagePath);
                var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();

                if (subIfdDirectory != null &&
                    subIfdDirectory.TryGetDateTime(ExifDirectoryBase.TagDateTimeOriginal, out DateTime dateTaken))
                {
                    return dateTaken;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
