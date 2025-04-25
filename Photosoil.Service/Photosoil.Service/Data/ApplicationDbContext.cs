
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using Photosoil.Core.Models.LinkModels;
using Photosoil.Core.Models.Second;

namespace Photosoil.Service.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public virtual DbSet<SoilObject> SoilObjects { get; set; }
        public virtual DbSet<Core.Models.File> Photo { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<EcoSystem> EcoSystem { get; set; }
        public virtual DbSet<Publication> Publication { get; set; }
        public virtual DbSet<ApplicationUser> User { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Tag> Tags{ get; set; }
        public virtual DbSet<Rules> Rules{ get; set; }


        public virtual DbSet<Term> Term { get; set; }
        public virtual DbSet<Classification> Classification { get; set; }

        public virtual DbSet<SoilTranslation> SoilTranslations { get; set; }
        public virtual DbSet<PublicationTranslation> PublicationTranslations { get; set; }
        public virtual DbSet<EcoTranslation> EcoTranslations { get; set; }
        public virtual DbSet<NewsTranslation> NewsTranslations { get; set; }
        public virtual DbSet<AuthorRequest> AuthorRequests { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }

        public async Task<int> SaveChangesAsync()
        {

            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Classification>().HasMany(x => x.Terms).WithOne(x => x.Classification)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<SoilObject>().HasMany(x => x.Translations).WithOne(x => x.SoilObject).OnDelete(DeleteBehavior.SetNull).HasForeignKey(x => x.SoilId);

            builder.Entity<SoilObject>().HasMany(x => x.Authors).WithMany(x => x.SoilObjects).UsingEntity<AuthorSoilObject>();
            builder.Entity<SoilObject>().HasMany(x => x.Terms).WithMany(x => x.SoilObjects).UsingEntity<SoilObjectTerm>();
            builder.Entity<SoilObject>().HasMany(x => x.Publications).WithMany(x => x.SoilObjects);
            builder.Entity<SoilObject>().HasMany(x => x.EcoSystems).WithMany(x => x.SoilObjects);
            builder.Entity<EcoSystem>().HasMany(x => x.Publications).WithMany(x => x.EcoSystems);
            builder.Entity<EcoSystem>().HasMany(x => x.Authors).WithMany(x => x.EcoSystems);
            builder.Entity<EcoSystem>().HasMany(x => x.ObjectPhoto).WithMany(x => x.EcoSystems);


            builder.Entity<EcoSystem>().HasMany(x => x.Translations).WithOne(x => x.EcoSystem).OnDelete(DeleteBehavior.SetNull).HasForeignKey(x => x.EcoSystemId);
            builder.Entity<EcoSystem>().HasOne(x => x.Photo).WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Article>().HasOne(x=>x.Photo).WithOne().HasForeignKey<Article>(x => x.PhotoId);
            builder.Entity<Author>().HasOne(x => x.Photo).WithOne().HasForeignKey<Author>(x => x.PhotoId);

            builder.Entity<SoilObject>().HasOne(x => x.Photo).WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<SoilObject>().HasMany(x => x.ObjectPhoto).WithMany(x => x.SoilObjects).UsingEntity<FileSoilObject>();


            builder.Entity<Author>().HasOne(x => x.User).WithMany(x => x.Authors).OnDelete(DeleteBehavior.SetNull).HasForeignKey(x => x.UserId);

            builder.Entity<ApplicationUser>().HasMany(x => x.EcoSystems).WithOne(x => x.User).OnDelete(DeleteBehavior.SetNull).HasForeignKey(x => x.UserId);
            builder.Entity<ApplicationUser>().HasMany(x => x.Publications).WithOne(x => x.User).OnDelete(DeleteBehavior.SetNull).HasForeignKey(x => x.UserId);
            builder.Entity<ApplicationUser>().HasMany(x => x.SoilObjects).WithOne(x => x.User).OnDelete(DeleteBehavior.SetNull).HasForeignKey(x => x.UserId);
            builder.Entity<ApplicationUser>().HasMany(x => x.News).WithOne(x => x.User).OnDelete(DeleteBehavior.SetNull).HasForeignKey(x => x.UserId);
            builder.Entity<ApplicationUser>().HasMany(x => x.Authors).WithOne(x => x.User).OnDelete(DeleteBehavior.SetNull).HasForeignKey(x => x.UserId);

            builder.Entity<Publication>().HasMany(x => x.Translations).WithOne(x => x.Publication).OnDelete(DeleteBehavior.SetNull).HasForeignKey(x => x.PublicationId);

            builder.Entity<Rules>().HasMany(x => x.Files).WithMany(x => x.Rules);

            builder.Entity<News>().HasMany(x => x.Files).WithMany(x => x.NewsFiles);
            builder.Entity<News>().HasMany(x => x.Tags).WithMany(x => x.News);
            builder.Entity<News>().HasMany(x => x.ObjectPhoto).WithMany(x => x.NewsPhoto);
            builder.Entity<News>().HasMany(x => x.Translations).WithOne(x => x.News).OnDelete(DeleteBehavior.SetNull).HasForeignKey(x => x.NewsId); ;

            SetData(builder);
            base.OnModelCreating(builder);
        }

        private void SetData(ModelBuilder builder)
        {
            var jsonTerms = System.IO.File.ReadAllText("wwwroot/SeedData/terms.json");
            var terms = JsonConvert.DeserializeObject<Term[]>(jsonTerms);

            var jsonTranslation= System.IO.File.ReadAllText("wwwroot/SeedData/authorTranslation.json");
            var translations = JsonConvert.DeserializeObject<Translation[]>(jsonTranslation);

            var jsonClassification = System.IO.File.ReadAllText("wwwroot/SeedData/classification.json");
            var classification = JsonConvert.DeserializeObject<Classification[]>(jsonClassification);

            var jsonAuthor = System.IO.File.ReadAllText("wwwroot/SeedData/authors.json");
            var authors = JsonConvert.DeserializeObject<Author[]>(jsonAuthor);

            var jsonPictures = System.IO.File.ReadAllText("wwwroot/SeedData/Pictures.json");
            var pictures = JsonConvert.DeserializeObject<Core.Models.File[]>(jsonPictures);

            var jsonSoils = System.IO.File.ReadAllText("wwwroot/SeedData/soils.json");
            var soils = JsonConvert.DeserializeObject<SoilObject[]>(jsonSoils);

            var jsonSoilstranslations = System.IO.File.ReadAllText("wwwroot/SeedData/soilTranslation.json");
            var soilstranslations = JsonConvert.DeserializeObject<SoilTranslation[]>(jsonSoilstranslations);

            var jsonSoilObjectTerm = System.IO.File.ReadAllText("wwwroot/SeedData/Link/soilObjectTerm.json");
            var soilObjectTerm = JsonConvert.DeserializeObject<SoilObjectTerm[]>(jsonSoilObjectTerm);

            var jsonFileSoilObject = System.IO.File.ReadAllText("wwwroot/SeedData/Link/fileSoilObject.json");
            var fileSoilObject = JsonConvert.DeserializeObject<FileSoilObject[]>(jsonFileSoilObject);

            var jsonAuthorSoilObject = System.IO.File.ReadAllText("wwwroot/SeedData/Link/authorSoilObject.json");
            var authorSoilObject = JsonConvert.DeserializeObject<AuthorSoilObject[]>(jsonAuthorSoilObject);

            builder.Entity<Translation>().HasData(translations);
            builder.Entity<Term>().HasData(terms);
            builder.Entity<Classification>().HasData(classification);
            builder.Entity<Author>().HasData(authors);
            builder.Entity<Core.Models.File>().HasData(pictures);
            builder.Entity<SoilObject>().HasData(soils);
            builder.Entity<SoilTranslation>().HasData(soilstranslations);
            builder.Entity<SoilObjectTerm>().HasData(soilObjectTerm);
            builder.Entity<FileSoilObject>().HasData(fileSoilObject);
            builder.Entity<AuthorSoilObject>().HasData(authorSoilObject);
        }

    }
}
