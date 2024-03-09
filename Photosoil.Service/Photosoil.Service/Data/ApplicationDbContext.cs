using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Photosoil.Core.Enum;
using Photosoil.Core.Models;
using Photosoil.Core.Models.Second;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Photosoil.Service.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<SoilObject> SoilObjects { get; set; }
        public virtual DbSet<Core.Models.File> Photo { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<EcoSystem> EcoSystem { get; set; }
        public virtual DbSet<Publication> Publication { get; set; }


        public virtual DbSet<Term> Term { get; set; }
        public virtual DbSet<Classification> Classification { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public async Task<int> SaveChangesAsync()
        {

            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Classification>().HasMany(x => x.Terms).WithOne(x => x.Classification)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<SoilObject>().HasMany(x => x.Terms).WithMany(x => x.SoilObjects);
            builder.Entity<SoilObject>().HasMany(x => x.Publications).WithMany(x => x.SoilObjects);
            builder.Entity<SoilObject>().HasMany(x => x.EcoSystems).WithMany(x => x.SoilObjects);
            builder.Entity<EcoSystem>().HasMany(x => x.Publications).WithMany(x => x.EcoSystems);

            builder.Entity<Article>().HasOne(x=>x.Photo).WithOne().HasForeignKey<Article>(x => x.PhotoId);
            builder.Entity<Author>().HasOne(x=>x.Photo).WithOne().HasForeignKey<Author>(x => x.PhotoId);
            builder.Entity<SoilObject>().HasOne(x=>x.Photo).WithOne().HasForeignKey<SoilObject>(x => x.PhotoId);




            SetData(builder);



            base.OnModelCreating(builder);
        }

        private void SetData(ModelBuilder builder)
        {
            var termQulifier = new List<string>()
            {
                "Cambric","Chronic","Titanic","Gleyic","Gleyic", 
                "Orthofluvent","Argillic","Reductic_glycemic", 
                "Rudic","Somerc","Polic", 
                "Paralithic", // Паралитический (ближайшее соответствие)
                "Ironpan",
                "Stagnic",
                "Steinic", // Стеинцевый (замена на более распространённое слово)
                "Natic",
                "Petrocalcic", // Лептокальциевый (ближайшее соответствие)
                "Limnetic",
                "Alluvial",
                "Mollisol",
                "Turfic",
                "Umbraept", // Умбриоватый (комбинация умбры и апт)
                "Umbraept", // повторное использование по двум вариантам
                "Vertic",
                "Vitrande", // Витриндозный (комбинация витри и андезита)
                "Laminar", // Ламинарный
                "Histosols",
                "Chernozem",
                "Circalitoral_Cryozem", // Криоземовый (комбинация circalitoral, cryo и zeim)
                "Distropic",
                "Calciorthid", // Эндокальцитровидный (комбинация endore, calcitic и orthic)
                "Calcic", // Кальциевый (совпадает с карбовым)
                "Aluminous", // Алюминиевый (по отношению к аллювию)
                "Andosol",
                "Brunic",
                "Entectic",
                "Eutric",
                "Gleyic", // повторное использование по разным источникам
                "Glossic",
                "Gypseous", // Гипсовый
                "Hydrosols", // Геологические водно-органические почвы
                "Glaciomoraine", // Моренные породы
                "Fragmental", // Фрагментарный
                "Fibrist", // Фибристый
                "Fluvisols",
                "Foliaceous", // Листоватый
                "Albic"
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
            var biomes = new List<string>()
            {
                "Влажные экваториальные леса",
                "Горные территории с высотной поясностью",
                "Субтропические вечно-зеленые леса и кустарники",
                "Типичная тундра (подзона)",
                "Южная тундра (подзона)",
                "Лесотундра",
                "Северная тайга (подзона)",
                "Средняя тайга (подзона)",
                "Южная тайга",
                "Хвойно-широколиственные леса",
                "Широколиственные леса",
                "Подтайга (мелколиственно-светлохвойная или хвойно-широколиственная)",
                "Лесостепь",
                "Степь (подзоны луговых и настоящих степей)",
                "Сухая и опустыненная степь",
                "Полупустыни и пустыни",
                "Субтропических саванн и редколесий"
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

            var soilDep = new Classification(){Id = 1,Name = "Отделы почв по Классификации почв России 2004/2008"};
            var soilSubType= new Classification(){Id = 2, Name = "Подтипы почв по Классификации почв России 2004/2008"};
            var soilType = new Classification(){Id = 3,Name = "Типы почв по Классификации почв России 2004/2008"};
            var natureZone = new Classification(){Id = 4,Name = "Природная зона"};
            var qualifier= new Classification(){Id = 5,Name = "Основные квалификаторы (WRB 2014)"};
            
            builder.Entity<Classification>().HasData(soilDep, natureZone, soilSubType, soilType, qualifier);

            var listTerm = new List<Term>();
            foreach (var name in termQulifier)
            {
                listTerm.Add(new Term()
                {
                    Name = name,
                    ClassificationId = qualifier.Id
                });
            }
            foreach (var name in nameType)
            {
                listTerm.Add(new Term()
                {
                    Name = name,
                    ClassificationId = soilType.Id
                });
            }
            foreach (var name in nameSubType)
            {
                listTerm.Add(new Term() { Name = name, ClassificationId = soilSubType.Id });
            }
            
            foreach (var name in biomes)
            {
                listTerm.Add(new Term() { Name = name, ClassificationId = natureZone.Id });
            }
            foreach (var name in nameDep)
            {
                listTerm.Add(new Term() { Name = name, ClassificationId = soilDep.Id });
            }

            for (int i = 1; i <listTerm.Count+1; i++)
            {
                listTerm[i-1].Id = i;
            }

            var rand = new Random();

            var soilObject = new SoilObject()
            {
                Id = 1,
                ObjectType = SoilObjectType.SoilProfiles,
                GeographicLocation = "Турция, северо-западные предместья Стамбула, окрестности местечка Фатих",
                ReliefLocation = "Выположенная поверхность в средней части мезосклона в юго-восточной оконечности хребта Истранджа",

                Latitude = "41.234193",
                Longtitude = "28.687272",

                PlantCommunity = "Кустарниково-древесное сообщество",
                SoilFeatures = "Почва вскипает с поверхности. Почвообразующей породой является известняк.",
                Terms = listTerm.GetRange(0,5),

                    Name = "Бурозем остаточно-карбонатный"
            };


            builder.Entity<Term>().HasData(listTerm);

        }
        private void SetSoilData(ModelBuilder builder)
        {
            var soilObject = new SoilObject()
            {
                Id = 1, 
                ObjectType = SoilObjectType.SoilProfiles,
                GeographicLocation = "Турция, северо-западные предместья Стамбула, окрестности местечка Фатих",
                ReliefLocation = "Выположенная поверхность в средней части мезосклона в юго-восточной оконечности хребта Истранджа",

                Latitude = "41.234193",
                Longtitude = "28.687272",

                PlantCommunity = "Кустарниково-древесное сообщество",
                SoilFeatures = "Почва вскипает с поверхности. Почвообразующей породой является известняк.",

                Name = "Бурозем остаточно-карбонатный"
            };
            var soilObject2 = new SoilObject()
            {
                Id = 1,
                ObjectType = SoilObjectType.SoilProfiles,
                GeographicLocation = "Турция, северо-западные предместья Стамбула, окрестности местечка Фатих",
                ReliefLocation = "Выположенная поверхность в средней части мезосклона в юго-восточной оконечности хребта Истранджа",

                Latitude = "69.088061",
                Longtitude = "49.804225",

                PlantCommunity = "Кустарниково-древесное сообщество",
                SoilFeatures = "Почва вскипает с поверхности. Почвообразующей породой является известняк.",

                Name = "Перегнойно-криометаморфическая глееватая мерзлотная"
            };



            builder.Entity<SoilObject>().HasData(soilObject);
            builder.Entity<SoilObject>().HasData(soilObject2);

        }
    }
}
