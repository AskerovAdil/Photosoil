using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Photosoil.Service.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsMulti = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NatureZone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NatureZone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Qualifier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoilDepartment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilDepartment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoilGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoilSubType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilSubType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoilType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Term",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClassificationId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Term", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Term_Classification_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "Classification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Summary = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    PhotoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FIO = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    PhotoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    SoilObjectId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoilObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    GeographicLocation = table.Column<string>(type: "text", nullable: true),
                    ReliefLocation = table.Column<string>(type: "text", nullable: true),
                    PlantCommunity = table.Column<string>(type: "text", nullable: true),
                    SoilFeatures = table.Column<string>(type: "text", nullable: true),
                    PhotoId = table.Column<int>(type: "integer", nullable: false),
                    AssociatedSoilComponents = table.Column<string>(type: "text", nullable: true),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    ObjectType = table.Column<int>(type: "integer", nullable: true),
                    AuthorId = table.Column<int>(type: "integer", nullable: true),
                    SoilGroupId = table.Column<int>(type: "integer", nullable: true),
                    DepartmentId = table.Column<int>(type: "integer", nullable: true),
                    NatureZoneId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoilObjects_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SoilObjects_NatureZone_NatureZoneId",
                        column: x => x.NatureZoneId,
                        principalTable: "NatureZone",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SoilObjects_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoilObjects_SoilDepartment_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "SoilDepartment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SoilObjects_SoilGroup_SoilGroupId",
                        column: x => x.SoilGroupId,
                        principalTable: "SoilGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QualifierSoilObject",
                columns: table => new
                {
                    QualifiersId = table.Column<int>(type: "integer", nullable: false),
                    SoilObjectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualifierSoilObject", x => new { x.QualifiersId, x.SoilObjectsId });
                    table.ForeignKey(
                        name: "FK_QualifierSoilObject_Qualifier_QualifiersId",
                        column: x => x.QualifiersId,
                        principalTable: "Qualifier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QualifierSoilObject_SoilObjects_SoilObjectsId",
                        column: x => x.SoilObjectsId,
                        principalTable: "SoilObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoilObjectSoilSubType",
                columns: table => new
                {
                    SoilObjectsId = table.Column<int>(type: "integer", nullable: false),
                    SoilSubTypesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilObjectSoilSubType", x => new { x.SoilObjectsId, x.SoilSubTypesId });
                    table.ForeignKey(
                        name: "FK_SoilObjectSoilSubType_SoilObjects_SoilObjectsId",
                        column: x => x.SoilObjectsId,
                        principalTable: "SoilObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoilObjectSoilSubType_SoilSubType_SoilSubTypesId",
                        column: x => x.SoilSubTypesId,
                        principalTable: "SoilSubType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoilObjectSoilType",
                columns: table => new
                {
                    SoilObjectsId = table.Column<int>(type: "integer", nullable: false),
                    SoilTypesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilObjectSoilType", x => new { x.SoilObjectsId, x.SoilTypesId });
                    table.ForeignKey(
                        name: "FK_SoilObjectSoilType_SoilObjects_SoilObjectsId",
                        column: x => x.SoilObjectsId,
                        principalTable: "SoilObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoilObjectSoilType_SoilType_SoilTypesId",
                        column: x => x.SoilTypesId,
                        principalTable: "SoilType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoilObjectTerm",
                columns: table => new
                {
                    SoilObjectsId = table.Column<int>(type: "integer", nullable: false),
                    TermsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilObjectTerm", x => new { x.SoilObjectsId, x.TermsId });
                    table.ForeignKey(
                        name: "FK_SoilObjectTerm_SoilObjects_SoilObjectsId",
                        column: x => x.SoilObjectsId,
                        principalTable: "SoilObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoilObjectTerm_Term_TermsId",
                        column: x => x.TermsId,
                        principalTable: "Term",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Classification",
                columns: new[] { "Id", "IsMulti", "Name" },
                values: new object[,]
                {
                    { 1, true, "Отделы почв по Классификации почв России 2004/2008" },
                    { 2, true, "Подтипы почв по Классификации почв России 2004/2008" },
                    { 3, true, "Типы почв по Классификации почв России 2004/2008" },
                    { 4, true, "Природная зона" },
                    { 5, true, "Основные квалификаторы (WRB 2014)" }
                });

            migrationBuilder.InsertData(
                table: "Term",
                columns: new[] { "Id", "ClassificationId", "Name" },
                values: new object[,]
                {
                    { 1, 5, "Cambric" },
                    { 2, 5, "Chronic" },
                    { 3, 5, "Titanic" },
                    { 4, 5, "Gleyic" },
                    { 5, 5, "Gleyic" },
                    { 6, 5, "Orthofluvent" },
                    { 7, 5, "Argillic" },
                    { 8, 5, "Reductic_glycemic" },
                    { 9, 5, "Rudic" },
                    { 10, 5, "Somerc" },
                    { 11, 5, "Polic" },
                    { 12, 5, "Paralithic" },
                    { 13, 5, "Ironpan" },
                    { 14, 5, "Stagnic" },
                    { 15, 5, "Steinic" },
                    { 16, 5, "Natic" },
                    { 17, 5, "Petrocalcic" },
                    { 18, 5, "Limnetic" },
                    { 19, 5, "Alluvial" },
                    { 20, 5, "Mollisol" },
                    { 21, 5, "Turfic" },
                    { 22, 5, "Umbraept" },
                    { 23, 5, "Umbraept" },
                    { 24, 5, "Vertic" },
                    { 25, 5, "Vitrande" },
                    { 26, 5, "Laminar" },
                    { 27, 5, "Histosols" },
                    { 28, 5, "Chernozem" },
                    { 29, 5, "Circalitoral_Cryozem" },
                    { 30, 5, "Distropic" },
                    { 31, 5, "Calciorthid" },
                    { 32, 5, "Calcic" },
                    { 33, 5, "Aluminous" },
                    { 34, 5, "Andosol" },
                    { 35, 5, "Brunic" },
                    { 36, 5, "Entectic" },
                    { 37, 5, "Eutric" },
                    { 38, 5, "Gleyic" },
                    { 39, 5, "Glossic" },
                    { 40, 5, "Gypseous" },
                    { 41, 5, "Hydrosols" },
                    { 42, 5, "Glaciomoraine" },
                    { 43, 5, "Fragmental" },
                    { 44, 5, "Fibrist" },
                    { 45, 5, "Fluvisols" },
                    { 46, 5, "Foliaceous" },
                    { 47, 5, "Albic" },
                    { 48, 3, "Абразёмы глинисто-иллювиированные" },
                    { 49, 3, "Агроабразёмы" },
                    { 50, 3, "Агродерново-подзолисто-глеевые" },
                    { 51, 3, "Агродерново-подзолистые" },
                    { 52, 3, "Агрозёмы" },
                    { 53, 3, "Агроземы текстурно-карбонатные" },
                    { 54, 3, "Агросолоди" },
                    { 55, 3, "Агростратозем" },
                    { 56, 3, "Агростратозёмы темногумусовые" },
                    { 57, 3, "Агротёмно-серые" },
                    { 58, 3, "Агротёмно-серые глеевые" },
                    { 59, 3, "Агротёмно-серые метаморфические" },
                    { 60, 3, "Агрочернозем текстурно-карбонатный" },
                    { 61, 3, "Агрочерноземы" },
                    { 62, 3, "Аллювиальные" },
                    { 63, 3, "Аллювиальные гумусовые (дерновые)" },
                    { 64, 3, "Аллювиальные гумусовые глеевые" },
                    { 65, 3, "Аллювиальные перегнойно-глеевые" },
                    { 66, 3, "Аллювиальные тёмногумусовые" },
                    { 67, 3, "Аллювиальные тёмногумусовые глеевые" },
                    { 68, 3, "Аллювиальные торфянно-глеевые" },
                    { 69, 3, "Бурая (аридная)" },
                    { 70, 3, "Бурозёмы" },
                    { 71, 3, "Бурозёмы грубогумусовые" },
                    { 72, 3, "Бурозёмы тёмногумусовые" },
                    { 73, 3, "Глеезёмы" },
                    { 74, 3, "Глееземы криометаморфические" },
                    { 75, 3, "Гумусово-гидрометаморфические" },
                    { 76, 3, "Дерново подзолы" },
                    { 77, 3, "Дерново-брусно-подзолистые" },
                    { 78, 3, "Дерново-криометаморфические" },
                    { 79, 3, "Дерново-подбуры" },
                    { 80, 3, "Дерново-подбуры глеевые" },
                    { 81, 3, "Дерново-подбуры элювоземы" },
                    { 82, 3, "Дерново-подзол-глеевые" },
                    { 83, 3, "Дерново-подзолистые" },
                    { 84, 3, "Дерново-подзолы глеевые" },
                    { 85, 3, "Дерново-солоди" },
                    { 86, 3, "Дерново-элювиально-метаморфические" },
                    { 87, 3, "Дерново-элювозем" },
                    { 88, 3, "Карбо-петрозём" },
                    { 89, 3, "Каштановые" },
                    { 90, 3, "Криогумусовые" },
                    { 91, 3, "Криозёмы" },
                    { 92, 3, "Криозёмы грубогумусовые" },
                    { 93, 3, "Криометаморфические" },
                    { 94, 3, "Криометаморфические грубогумусовые" },
                    { 95, 3, "Литозем грубогумусовый" },
                    { 96, 3, "Органо-ржавозёмы" },
                    { 97, 3, "Охристые" },
                    { 98, 3, "Палевые" },
                    { 99, 3, "Пелозем" },
                    { 100, 3, "Пелоземы гумусовые" },
                    { 101, 3, "Перегнойно-глеевые" },
                    { 102, 3, "Перегnoonно-криометаморфические" },
                    { 103, 3, "Перегнойно-охристая" },
                    { 104, 3, "Перегнойно-темногумусовые" },
                    { 105, 3, "Перегнойные" },
                    { 106, 3, "Петроземы" },
                    { 107, 3, "Подбуры" },
                    { 108, 3, "Подбуры глеевые" },
                    { 109, 3, "Подзол-элювозёмы" },
                    { 110, 3, "Подзолисто-глеевые" },
                    { 111, 3, "Подзолистые" },
                    { 112, 3, "Подзолы" },
                    { 113, 3, "Подзолы глеевые" },
                    { 114, 3, "Псаммоземы" },
                    { 115, 3, "Ржавозёмы" },
                    { 116, 3, "Ржавозёмы грубогумусовые" },
                    { 117, 3, "Светлогумусовые" },
                    { 118, 3, "Серая метаморфическая" },
                    { 119, 3, "Серогумусовые (дерновые)" },
                    { 120, 3, "Серые" },
                    { 121, 3, "Слоисто-пепловые" },
                    { 122, 3, "Солоди темногумусовые" },
                    { 123, 3, "Солонцы светлые" },
                    { 124, 3, "Солонцы темные" },
                    { 125, 3, "Солончаки" },
                    { 126, 3, "Солончаки глеевые" },
                    { 127, 3, "Стратозёмы серогумусовые" },
                    { 128, 3, "Сухо-торфяные" },
                    { 129, 3, "Сухоторфяно-подбуры" },
                    { 130, 3, "Сухоторфяно-подзолы" },
                    { 131, 3, "Сухоторфяные" },
                    { 132, 3, "Темно-серые" },
                    { 133, 3, "Темно-серые глеевые" },
                    { 134, 3, "Темно-serые метаморфические" },
                    { 135, 3, "Темногумусово-глеевые" },
                    { 136, 3, "Темногумусовые" },
                    { 137, 3, "Темногумусовые подбелы" },
                    { 138, 3, "Торфозёмы" },
                    { 139, 3, "Торфяно-глеезёмы" },
                    { 140, 3, "Торфяно-криозёмы" },
                    { 141, 3, "Торфяно-подзолисто-глеевые" },
                    { 142, 3, "Торфяно-подзолы" },
                    { 143, 3, "Торфяно-подзолы глеевые" },
                    { 144, 3, "Торфяные олиготрофные" },
                    { 145, 3, "Торфяные олиготрофные глеевые" },
                    { 146, 3, "Торфяные эутрофные" },
                    { 147, 3, "Торфяные эутрофные глеевые" },
                    { 148, 2, "Артииндустратный" },
                    { 149, 2, "Глее-подзолистые" },
                    { 150, 2, "Глееватые" },
                    { 151, 2, "Глеевые" },
                    { 152, 2, "Глинисто-иллювиированные" },
                    { 153, 2, "Глинофибровые" },
                    { 154, 2, "Грубо-гумусированные" },
                    { 155, 2, "Гумусово-слаборазвитый" },
                    { 156, 2, "Дисперсно-карбонатный" },
                    { 157, 2, "Железисто-гранулированные (гранузёмы)" },
                    { 158, 2, "Засоленные" },
                    { 159, 2, "Иллювиально-гумусовые" },
                    { 160, 2, "Иллювиально-железистые" },
                    { 161, 2, "Иллювиально-ожелезненные" },
                    { 162, 2, "Иллювиально-гумусированные" },
                    { 163, 2, "Иловато-перегнойные" },
                    { 164, 2, "Иловато-торфяные" },
                    { 165, 2, "Квазиглееватые" },
                    { 166, 2, "Конкреционные" },
                    { 167, 2, "Контактно-осветленные" },
                    { 168, 2, "Крио-гомогенные" },
                    { 169, 2, "Криогенно-ожелезненные" },
                    { 170, 2, "Криометаморфические" },
                    { 171, 2, "Криотурбированные" },
                    { 172, 2, "Мерзлотные" },
                    { 173, 2, "Миграционно-мицелярный" },
                    { 174, 2, "Миграционно-сегрегационные" },
                    { 175, 2, "Минерально-торфяные" },
                    { 176, 2, "Омергеленные" },
                    { 177, 2, "Оподзоленные" },
                    { 178, 2, "Оруденелые" },
                    { 179, 2, "Осолоделые" },
                    { 180, 2, "Остаточно-карбонатные" },
                    { 181, 2, "Остаточно-эутрофные" },
                    { 182, 2, "Палево-метаморфизованные" },
                    { 183, 2, "Палево-подзолистые" },
                    { 184, 2, "Палевые" },
                    { 185, 2, "Перегнойно-грубогумусовые" },
                    { 186, 2, "Перегнойно-торфяные" },
                    { 187, 2, "Перегнойные" },
                    { 188, 2, "Перегнойные (глеевые)" },
                    { 189, 2, "Потечно-гумусовые" },
                    { 190, 2, "Псевдофибровые" },
                    { 191, 2, "С микропрофилем подзола" },
                    { 192, 2, "Сегрегационные" },
                    { 193, 2, "Со вторым гумусовым горизонтом" },
                    { 194, 2, "Солонцеватые" },
                    { 195, 2, "Языковатые" },
                    { 196, 4, "Влажные экваториальные леса" },
                    { 197, 4, "Горные территории с высотной поясностью" },
                    { 198, 4, "Субтропические вечно-зеленые леса и кустарники" },
                    { 199, 4, "Типичная тундра (подзона)" },
                    { 200, 4, "Южная тундра (подзона)" },
                    { 201, 4, "Лесотундра" },
                    { 202, 4, "Северная тайга (подзона)" },
                    { 203, 4, "Средняя тайга (подзона)" },
                    { 204, 4, "Южная тайга" },
                    { 205, 4, "Хвойно-широколиственные леса" },
                    { 206, 4, "Широколиственные леса" },
                    { 207, 4, "Подтайга (мелколиственно-светлохвойная или хвойно-широколиственная)" },
                    { 208, 4, "Лесостепь" },
                    { 209, 4, "Степь (подзоны луговых и настоящих степей)" },
                    { 210, 4, "Сухая и опустыненная степь" },
                    { 211, 4, "Полупустыни и пустыни" },
                    { 212, 4, "Субтропических саванн и редколесий" },
                    { 213, 1, "Торфяные почвы" },
                    { 214, 1, "Турбозёмы" },
                    { 215, 1, "Хемозёмы" },
                    { 216, 1, "Торфозёмы" },
                    { 217, 1, "Текстурно-дифференцированные почвы" },
                    { 218, 1, "Стратозёмы" },
                    { 219, 1, "Структурно-метаморфические почвы" },
                    { 220, 1, "Химически-преобразованные" },
                    { 221, 1, "Щелочно-глинисто-дифференцированные почвы" },
                    { 222, 1, "Элювиальные почвы" },
                    { 223, 1, "Слаборазвитые почвы" },
                    { 224, 1, "Светлогумусовые аккумулятивно-карбонатные почвы" },
                    { 225, 1, "Аллювиальные почвы" },
                    { 226, 1, "Альфегумусовые почвы" },
                    { 227, 1, "Вулканические почвы" },
                    { 228, 1, "Аккумулятивно-гумусовые почвы" },
                    { 229, 1, "Аквазёмы" },
                    { 230, 1, "Агроабразёмы" },
                    { 231, 1, "Агрозёмы" },
                    { 232, 1, "Галоморфные почвы" },
                    { 233, 1, "Гидрометаморфические почвы" },
                    { 234, 1, "Литозёмы" },
                    { 235, 1, "Органо-аккумулятивные почвы" },
                    { 236, 1, "Палево-метаморфические почвы" },
                    { 237, 1, "Криометаморфические почвы" },
                    { 238, 1, "Криогенные почвы (Криозёмы)" },
                    { 239, 1, "Глеевые почвы" },
                    { 240, 1, "Железисто-метаморфические почвы" },
                    { 241, 1, "Абразёмы" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_PhotoId",
                table: "Article",
                column: "PhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Author_PhotoId",
                table: "Author",
                column: "PhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_SoilObjectId",
                table: "Photo",
                column: "SoilObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_QualifierSoilObject_SoilObjectsId",
                table: "QualifierSoilObject",
                column: "SoilObjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_SoilObjects_AuthorId",
                table: "SoilObjects",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_SoilObjects_DepartmentId",
                table: "SoilObjects",
                column: "DepartmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoilObjects_NatureZoneId",
                table: "SoilObjects",
                column: "NatureZoneId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoilObjects_PhotoId",
                table: "SoilObjects",
                column: "PhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoilObjects_SoilGroupId",
                table: "SoilObjects",
                column: "SoilGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoilObjectSoilSubType_SoilSubTypesId",
                table: "SoilObjectSoilSubType",
                column: "SoilSubTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_SoilObjectSoilType_SoilTypesId",
                table: "SoilObjectSoilType",
                column: "SoilTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_SoilObjectTerm_TermsId",
                table: "SoilObjectTerm",
                column: "TermsId");

            migrationBuilder.CreateIndex(
                name: "IX_Term_ClassificationId",
                table: "Term",
                column: "ClassificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Photo_PhotoId",
                table: "Article",
                column: "PhotoId",
                principalTable: "Photo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Photo_PhotoId",
                table: "Author",
                column: "PhotoId",
                principalTable: "Photo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_SoilObjects_SoilObjectId",
                table: "Photo",
                column: "SoilObjectId",
                principalTable: "SoilObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_Photo_PhotoId",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_SoilObjects_Photo_PhotoId",
                table: "SoilObjects");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "QualifierSoilObject");

            migrationBuilder.DropTable(
                name: "SoilObjectSoilSubType");

            migrationBuilder.DropTable(
                name: "SoilObjectSoilType");

            migrationBuilder.DropTable(
                name: "SoilObjectTerm");

            migrationBuilder.DropTable(
                name: "Qualifier");

            migrationBuilder.DropTable(
                name: "SoilSubType");

            migrationBuilder.DropTable(
                name: "SoilType");

            migrationBuilder.DropTable(
                name: "Term");

            migrationBuilder.DropTable(
                name: "Classification");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "SoilObjects");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "NatureZone");

            migrationBuilder.DropTable(
                name: "SoilDepartment");

            migrationBuilder.DropTable(
                name: "SoilGroup");
        }
    }
}
