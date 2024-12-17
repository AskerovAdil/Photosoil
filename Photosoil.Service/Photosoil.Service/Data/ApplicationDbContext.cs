
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Photosoil.Core.Enum;
using Photosoil.Core.Models;
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

            builder.Entity<SoilObject>().HasMany(x => x.Authors).WithMany(x => x.SoilObjects);
            builder.Entity<SoilObject>().HasMany(x => x.Terms).WithMany(x => x.SoilObjects);
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

            builder.Entity<SoilObject>().HasMany(x => x.ObjectPhoto).WithMany(x => x.SoilObjects);


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
            //var termQulifier = new List<string>()
            //{
            //    "Cambric","Chronic","Titanic","Gleyic","Gleyic", 
            //    "Orthofluvent","Argillic","Reductic_glycemic", 
            //    "Rudic","Somerc","Polic", 
            //    "Paralithic", // Паралитический (ближайшее соответствие)
            //    "Ironpan",
            //    "Stagnic",
            //    "Steinic", // Стеинцевый (замена на более распространённое слово)
            //    "Natic",
            //    "Petrocalcic", // Лептокальциевый (ближайшее соответствие)
            //    "Limnetic",
            //    "Alluvial",
            //    "Mollisol",
            //    "Turfic",
            //    "Umbraept", // Умбриоватый (комбинация умбры и апт)
            //    "Umbraept", // повторное использование по двум вариантам
            //    "Vertic",
            //    "Vitrande", // Витриндозный (комбинация витри и андезита)
            //    "Laminar", // Ламинарный
            //    "Histosols",
            //    "Chernozem",
            //    "Circalitoral_Cryozem", // Криоземовый (комбинация circalitoral, cryo и zeim)
            //    "Distropic",
            //    "Calciorthid", // Эндокальцитровидный (комбинация endore, calcitic и orthic)
            //    "Calcic", // Кальциевый (совпадает с карбовым)
            //    "Aluminous", // Алюминиевый (по отношению к аллювию)
            //    "Andosol",
            //    "Brunic",
            //    "Entectic",
            //    "Eutric",
            //    "Gleyic", // повторное использование по разным источникам
            //    "Glossic",
            //    "Gypseous", // Гипсовый
            //    "Hydrosols", // Геологические водно-органические почвы
            //    "Glaciomoraine", // Моренные породы
            //    "Fragmental", // Фрагментарный
            //    "Fibrist", // Фибристый
            //    "Fluvisols",
            //    "Foliaceous", // Листоватый
            //    "Albic"
            //};
            var ReferenceSoilGroups = new Dictionary<string, string>()
            {
                { "Acrisols", "Acrisols" },
                { "Alisols", "Alisols" },
                { "Andosols", "Andosols" },
                { "Anthrosols", "Anthrosols" },
                { "Arenosols", "Arenosols" },
                { "Calcisols", "Calcisols" },
                { "Cambisols", "Cambisols" },
                { "Chernozems", "Chernozems" },
                { "Cryosols", "Cryosols" },
                { "Durisols", "Durisols" },
                { "Ferralsols", "Ferralsols" },
                { "Fluvisols", "Fluvisols" },
                { "Gleysols", "Gleysols" },
                { "Gypsisols", "Gypsisols" },
                { "Histosols", "Histosols" },
                { "Kastanozems", "Kastanozems" },
                { "Leptosols", "Leptosols" },
                { "Lixisols", "Lixisols" },
                { "Luvisols", "Luvisols" },
                { "Nitisols", "Nitisols" },
                { "Phaeozems", "Phaeozems" },
                { "Planosols", "Planosols" },
                { "Plinthosols", "Plinthosols" },
                { "Podzols", "Podzols" },
                { "Regosols", "Regosols" },
                { "Retisols", "Retisols" },
                { "Solonchaks", "Solonchaks" },
                { "Solonetz", "Solonetz" },
                { "Stagnosols", "Stagnosols" },
                { "Technosols", "Technosols" },
                { "Umbrisols", "Umbrisols" },
                { "Vertisols", "Vertisols" }
            };
            var termQulifier = new Dictionary<string, string>()
            {
                { "Abruptic", "Abruptic" },
                { "Calcaric", "Calcaric" },
                { "Cambic", "Cambic" },
                { "Chromic", "Chromic" },
                { "Cutanic", "Cutanic" },
                { "Greyic", "Greyic" },
                { "Greyzemic", "Greyzemic" },
                { "Orthofluvic", "Orthofluvic" },
                { "Protoargic", "Protoargic" },
                { "Reductigleyic", "Reductigleyic" },
                { "Rustic", "Rustic" },
                { "Someric", "Someric" },
                { "Spolic", "Spolic" },
                { "Sapric", "Sapric" },
                { "Spodic", "Spodic" },
                { "Stagnic", "Stagnic" },
                { "Ortsteinic", "Ortsteinic" },
                { "Natric", "Natric" },
                { "Leptic", "Leptic" },
                { "Limnic", "Limnic" },
                { "Luvic", "Luvic" },
                { "Mollic", "Mollic" },
                { "Turbic", "Turbic" },
                { "Umbric", "Umbric" },
                { "Vertic", "Vertic" },
                { "Vitric", "Vitric" },
                { "Lamellic", "Lamellic" },
                { "Histic", "Histic" },
                { "Chernic", "Chernic" },
                { "Cryic", "Cryic" },
                { "Dystric", "Dystric" },
                { "Endocalcic", "Endocalcic" },
                { "Carbic", "Carbic" },
                { "Calcic", "Calcic" },
                { "Alic", "Alic" },
                { "Andic", "Andic" },
                { "Brunic", "Brunic" },
                { "Entic", "Entic" },
                { "Eutric", "Eutric" },
                { "Gleyic", "Gleyic" },
                { "Glossic", "Glossic" },
                { "Gypsic", "Gypsic" },
                { "Hemic", "Hemic" },
                { "Glacic", "Glacic" },
                { "Fragic", "Fragic" },
                { "Fibric", "Fibric" },
                { "Fluvic", "Fluvic" },
                { "Folic", "Folic" },
                { "Albic", "Albic" }
            };




            var nameType = new List<string>()
            {
                "Абразёмы глинисто-иллювиированные",
                "Агроабразёмы",
                "Агродерново-подзолисто-глеевые",
                "Агродерново-подзолистые",
                "Агрозёмы",
                "Агроземы текстурно-карбонатные",
                "Агросолоди",
                "Агростратозем",
                "Агростратозёмы темногумусовые",
                "Агротёмно-серые",
                "Агротёмно-серые глеевые",
                "Агротёмно-серые метаморфические",
                "Агрочернозем текстурно-карбонатный",
                "Агрочерноземы",
                "Аллювиальные",
                "Аллювиальные гумусовые (дерновые)",
                "Аллювиальные гумусовые глеевые",
                "Аллювиальные перегнойно-глеевые",
                "Аллювиальные тёмногумусовые",
                "Аллювиальные тёмногумусовые глеевые",
                "Аллювиальные торфянно-глеевые",
                "Бурая (аридная)",
                "Бурозёмы",
                "Бурозёмы грубогумусовые",
                "Бурозёмы тёмногумусовые",
                "Глеезёмы",
                "Глееземы криометаморфические",
                "Гумусово-гидрометаморфические",
                "Дерново подзолы",
                "Дерново-брусно-подзолистые",
                "Дерново-криометаморфические",
                "Дерново-подбуры",
                "Дерново-подбуры глеевые",
                "Дерново-подбуры элювоземы",
                "Дерново-подзол-глеевые",
                "Дерново-подзолистые",
                "Дерново-подзолы глеевые",
                "Дерново-солоди",
                "Дерново-элювиально-метаморфические",
                "Дерново-элювозем",
                "Карбо-петрозём",
                "Каштановые",
                "Криогумусовые",
                "Криозёмы",
                "Криозёмы грубогумусовые",
                "Криометаморфические",
                "Криометаморфические грубогумусовые",
                "Литозем грубогумусовый",
                "Органо-ржавозёмы",
                "Охристые",
                "Палевые",
                "Пелозем",
                "Пелоземы гумусовые",
                "Перегнойно-глеевые",
                "Перегnoonно-криометаморфические",
                "Перегнойно-охристая",
                "Перегнойно-темногумусовые",
                "Перегнойные",
                "Петроземы",
                "Подбуры",
                "Подбуры глеевые",
                "Подзол-элювозёмы",
                "Подзолисто-глеевые",
                "Подзолистые",
                "Подзолы",
                "Подзолы глеевые",
                "Псаммоземы",
                "Ржавозёмы",
                "Ржавозёмы грубогумусовые",
                "Светлогумусовые",
                "Серая метаморфическая",
                "Серогумусовые (дерновые)",
                "Серые",
                "Слоисто-пепловые",
                "Солоди темногумусовые",
                "Солонцы светлые",
                "Солонцы темные",
                "Солончаки",
                "Солончаки глеевые",
                "Стратозёмы серогумусовые",
                "Сухо-торфяные",
                "Сухоторфяно-подбуры",
                "Сухоторфяно-подзолы",
                "Сухоторфяные",
                "Темно-серые",
                "Темно-серые глеевые",
                "Темно-serые метаморфические",
                "Темногумусово-глеевые",
                "Темногумусовые",
                "Темногумусовые подбелы",
                "Торфозёмы",
                "Торфяно-глеезёмы",
                "Торфяно-криозёмы",
                "Торфяно-подзолисто-глеевые",
                "Торфяно-подзолы",
                "Торфяно-подзолы глеевые",
                "Торфяные олиготрофные",
                "Торфяные олиготрофные глеевые",
                "Торфяные эутрофные",
                "Торфяные эутрофные глеевые"
            };

            var nameSubType = new List<string>()
            {
                "Артииндустратный",
                "Глее-подзолистые",
                "Глееватые",
                "Глеевые",
                "Глинисто-иллювиированные",
                "Глинофибровые",
                "Грубо-гумусированные",
                "Гумусово-слаборазвитый",
                "Дисперсно-карбонатный",
                "Железисто-гранулированные (гранузёмы)",
                "Засоленные",
                "Иллювиально-гумусовые",
                "Иллювиально-железистые",
                "Иллювиально-ожелезненные",
                "Иллювиально-гумусированные",
                "Иловато-перегнойные",
                "Иловато-торфяные",
                "Квазиглееватые",
                "Конкреционные",
                "Контактно-осветленные",
                "Крио-гомогенные",
                "Криогенно-ожелезненные",
                "Криометаморфические",
                "Криотурбированные",
                "Мерзлотные",
                "Миграционно-мицелярный",
                "Миграционно-сегрегационные",
                "Минерально-торфяные",
                "Омергеленные",
                "Оподзоленные",
                "Оруденелые",
                "Осолоделые",
                "Остаточно-карбонатные",
                "Остаточно-эутрофные",
                "Палево-метаморфизованные",
                "Палево-подзолистые",
                "Палевые",
                "Перегнойно-грубогумусовые",
                "Перегнойно-торфяные",
                "Перегнойные",
                "Перегнойные (глеевые)",
                "Потечно-гумусовые",
                "Псевдофибровые",
                "С микропрофилем подзола",
                "Сегрегационные",
                "Со вторым гумусовым горизонтом",
                "Солонцеватые",
                "Языковатые"
            };
            //var biomes = new List<string>()
            //{
            //    "Влажные экваториальные леса",
            //    "Горные территории с высотной поясностью",
            //    "Субтропические вечно-зеленые леса и кустарники",
            //    "Типичная тундра (подзона)",
            //    "Южная тундра (подзона)",
            //    "Лесотундра",
            //    "Северная тайга (подзона)",
            //    "Средняя тайга (подзона)",
            //    "Южная тайга",
            //    "Хвойно-широколиственные леса",
            //    "Широколиственные леса",
            //    "Подтайга (мелколиственно-светлохвойная или хвойно-широколиственная)",
            //    "Лесостепь",
            //    "Степь (подзоны луговых и настоящих степей)",
            //    "Сухая и опустыненная степь",
            //    "Полупустыни и пустыни",
            //    "Субтропических саванн и редколесий"
            //};
            var biomes = new Dictionary<string, string>()
            {
                { "Влажные экваториальные леса", "Humid equatorial forests" },
                { "Горные территории с высотной поясностью", "Mountains with altitudinal zonations" },
                { "Субтропические вечно-зеленые леса и кустарники", "Subtropical evergreen forests and shrubs" },
                { "Типичная тундра (подзона)", "Typical tundra (subzone)" },
                { "Южная тундра (подзона)", "South tundra (subzone)" },
                { "Лесотундра", "Forest Tundra" },
                { "Северная тайга (подзона)", "Northern taiga (subzone)" },
                { "Средняя тайга (подзона)", "Middle taiga (subzone)" },
                { "Южная тайга", "Southern taiga (subzone)" },
                { "Хвойно-широколиственные леса", "Coniferous-deciduous forests" },
                { "Широколиственные леса", "Broad-leaved forests (deciduous forests)" },
                { "Подтайга (мелколиственно-светлохвойная или хвойно-широколиственная)", "Continental subboreal forests (hemiboreal)" },
                { "Лесостепь", "Forest steppe" },
                { "Степь (подзоны луговых и настоящих степей)", "Steppe (subzones meadow and true steppes)" },
                { "Сухая и опустыненная степь", "Dry and deserted steppe" },
                { "Полупустыни и пустыни", "Deserts and semi-deserts" },
                { "Субтропических саванн и редколесий", "Subtropical savannahs and woodlands" }
            };

            var nameDep= new List<string>()
            {
                "Торфяные почвы",
                "Турбозёмы",
                "Хемозёмы",
                "Торфозёмы",
                "Текстурно-дифференцированные почвы",
                "Стратозёмы",
                "Структурно-метаморфические почвы",
                "Химически-преобразованные",
                "Щелочно-глинисто-дифференцированные почвы",
                "Элювиальные почвы",
                "Слаборазвитые почвы",
                "Светлогумусовые аккумулятивно-карбонатные почвы",
                "Аллювиальные почвы",
                "Альфегумусовые почвы",
                "Вулканические почвы",
                "Аккумулятивно-гумусовые почвы",
                "Аквазёмы",
                "Агроабразёмы",
                "Агрозёмы",
                "Галоморфные почвы",
                "Гидрометаморфические почвы",
                "Литозёмы",
                "Органо-аккумулятивные почвы",
                "Палево-метаморфические почвы",
                "Криометаморфические почвы",
                "Криогенные почвы (Криозёмы)",
                "Глеевые почвы",
                "Железисто-метаморфические почвы",
                "Абразёмы"
            };
            var natureZone = new Classification() { Id = 4, TranslationMode = TranslationMode.Neutral,  NameRu = "Природная зона", NameEng = "Natural zone" };
            var soilDep = new Classification(){Id = 1,TranslationMode = TranslationMode.OnlyRu,NameRu = "Отделы почв по Классификации почв России 2004/2008"};
            var soilType = new Classification(){Id = 3, TranslationMode = TranslationMode.OnlyRu, NameRu = "Типы почв по Классификации почв России 2004/2008"};
            var soilSubType = new Classification() { Id = 2, TranslationMode = TranslationMode.OnlyRu, NameRu = "Подтипы почв по Классификации почв России 2004/2008" };
            var qualifier = new Classification() { Id = 5, TranslationMode = TranslationMode.Neutral, NameRu = "Основные квалификаторы (WRB 2014)", NameEng = "Principal qualifiers (WRB 2014)" };
            var Refqualifier = new Classification() { Id = 6, TranslationMode = TranslationMode.Neutral, NameRu = "Реферативные почвенные группы (WRB 2014)", NameEng = "Reference Soil Groups (WRB 2014)" };

            //var qualifier = new Classification() { Id = 6, NameRu = "Основные квалификаторы (WRB 2014)" };

            builder.Entity<Classification>().HasData(soilDep, natureZone, soilSubType, soilType, qualifier, Refqualifier);

            var listTerm = new List<Term>();
            foreach (var name in termQulifier)
            {
                listTerm.Add(new Term()
                {
                    NameRu = name.Key,
                    NameEng = name.Value,
                    ClassificationId = qualifier.Id
                });
            }
            foreach (var name in ReferenceSoilGroups)
            {
                listTerm.Add(new Term()
                {
                    NameRu = name.Key,
                    NameEng = name.Value,
                    ClassificationId = Refqualifier.Id
                });
            }
            foreach (var name in nameType)
            {
                listTerm.Add(new Term()
                {
                    NameRu = name,
                    ClassificationId = soilType.Id
                });
            }
            foreach (var name in nameSubType)
            {
                listTerm.Add(new Term() { NameRu = name, ClassificationId = soilSubType.Id });
            }
            
            foreach (var name in biomes)
            {
                listTerm.Add(new Term() { NameRu = name.Key,NameEng = name.Value, ClassificationId = natureZone.Id });
            }
            foreach (var name in nameDep)
            {
                listTerm.Add(new Term() { NameRu = name, ClassificationId = soilDep.Id });
            }

            for (int i = 1; i <listTerm.Count+1; i++)
            {
                listTerm[i-1].Id = i;
            }

            var rand = new Random();




            builder.Entity<Term>().HasData(listTerm);

        }
       
    }
}
