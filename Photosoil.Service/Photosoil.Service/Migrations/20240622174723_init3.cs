using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Photosoil.Service.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Path = table.Column<string>(type: "text", nullable: false),
                    TitleEng = table.Column<string>(type: "text", nullable: true),
                    TitleRu = table.Column<string>(type: "text", nullable: true),
                    LastUpdated = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Organization = table.Column<string>(type: "text", nullable: true),
                    Specialization = table.Column<string>(type: "text", nullable: true),
                    Degree = table.Column<string>(type: "text", nullable: true),
                    Position = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_Article_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EcoSystem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PhotoId = table.Column<int>(type: "integer", nullable: false),
                    LastUpdated = table.Column<string>(type: "text", nullable: true),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: true),
                    IsEnglish = table.Column<bool>(type: "boolean", nullable: true),
                    OtherLangId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Latitude = table.Column<string>(type: "text", nullable: true),
                    Longtitude = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcoSystem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcoSystem_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_EcoSystem_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Publication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Edition = table.Column<string>(type: "text", nullable: true),
                    Authors = table.Column<string>(type: "text", nullable: true),
                    Doi = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: true),
                    LastUpdated = table.Column<string>(type: "text", nullable: true),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: true),
                    Coordinates = table.Column<string>(type: "text", nullable: true),
                    FileId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publication_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Publication_Photo_FileId",
                        column: x => x.FileId,
                        principalTable: "Photo",
                        principalColumn: "Id");
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
                    Code = table.Column<string>(type: "text", nullable: true),
                    LastUpdated = table.Column<string>(type: "text", nullable: true),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: true),
                    IsEnglish = table.Column<bool>(type: "boolean", nullable: true),
                    OtherLangId = table.Column<int>(type: "integer", nullable: true),
                    ObjectType = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Latitude = table.Column<string>(type: "text", nullable: true),
                    Longtitude = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoilObjects_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SoilObjects_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataEngId = table.Column<int>(type: "integer", nullable: false),
                    DataRuId = table.Column<int>(type: "integer", nullable: false),
                    OtherProfiles = table.Column<string>(type: "text", nullable: true),
                    Contacts = table.Column<string>(type: "text", nullable: true),
                    PhotoId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Author_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Author_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Author_Translation_DataEngId",
                        column: x => x.DataEngId,
                        principalTable: "Translation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Author_Translation_DataRuId",
                        column: x => x.DataRuId,
                        principalTable: "Translation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcoSystemFile",
                columns: table => new
                {
                    EcoSystemsId = table.Column<int>(type: "integer", nullable: false),
                    ObjectPhotoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcoSystemFile", x => new { x.EcoSystemsId, x.ObjectPhotoId });
                    table.ForeignKey(
                        name: "FK_EcoSystemFile_EcoSystem_EcoSystemsId",
                        column: x => x.EcoSystemsId,
                        principalTable: "EcoSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EcoSystemFile_Photo_ObjectPhotoId",
                        column: x => x.ObjectPhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcoSystemPublication",
                columns: table => new
                {
                    EcoSystemsId = table.Column<int>(type: "integer", nullable: false),
                    PublicationsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcoSystemPublication", x => new { x.EcoSystemsId, x.PublicationsId });
                    table.ForeignKey(
                        name: "FK_EcoSystemPublication_EcoSystem_EcoSystemsId",
                        column: x => x.EcoSystemsId,
                        principalTable: "EcoSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EcoSystemPublication_Publication_PublicationsId",
                        column: x => x.PublicationsId,
                        principalTable: "Publication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcoSystemSoilObject",
                columns: table => new
                {
                    EcoSystemsId = table.Column<int>(type: "integer", nullable: false),
                    SoilObjectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcoSystemSoilObject", x => new { x.EcoSystemsId, x.SoilObjectsId });
                    table.ForeignKey(
                        name: "FK_EcoSystemSoilObject_EcoSystem_EcoSystemsId",
                        column: x => x.EcoSystemsId,
                        principalTable: "EcoSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EcoSystemSoilObject_SoilObjects_SoilObjectsId",
                        column: x => x.SoilObjectsId,
                        principalTable: "SoilObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileSoilObject",
                columns: table => new
                {
                    ObjectPhotoId = table.Column<int>(type: "integer", nullable: false),
                    SoilObjectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileSoilObject", x => new { x.ObjectPhotoId, x.SoilObjectsId });
                    table.ForeignKey(
                        name: "FK_FileSoilObject_Photo_ObjectPhotoId",
                        column: x => x.ObjectPhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileSoilObject_SoilObjects_SoilObjectsId",
                        column: x => x.SoilObjectsId,
                        principalTable: "SoilObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicationSoilObject",
                columns: table => new
                {
                    PublicationsId = table.Column<int>(type: "integer", nullable: false),
                    SoilObjectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationSoilObject", x => new { x.PublicationsId, x.SoilObjectsId });
                    table.ForeignKey(
                        name: "FK_PublicationSoilObject_Publication_PublicationsId",
                        column: x => x.PublicationsId,
                        principalTable: "Publication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicationSoilObject_SoilObjects_SoilObjectsId",
                        column: x => x.SoilObjectsId,
                        principalTable: "SoilObjects",
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

            migrationBuilder.CreateTable(
                name: "AuthorEcoSystem",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "integer", nullable: false),
                    EcoSystemsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorEcoSystem", x => new { x.AuthorsId, x.EcoSystemsId });
                    table.ForeignKey(
                        name: "FK_AuthorEcoSystem_Author_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorEcoSystem_EcoSystem_EcoSystemsId",
                        column: x => x.EcoSystemsId,
                        principalTable: "EcoSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorSoilObject",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "integer", nullable: false),
                    SoilObjectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorSoilObject", x => new { x.AuthorsId, x.SoilObjectsId });
                    table.ForeignKey(
                        name: "FK_AuthorSoilObject_Author_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorSoilObject_SoilObjects_SoilObjectsId",
                        column: x => x.SoilObjectsId,
                        principalTable: "SoilObjects",
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Author_DataEngId",
                table: "Author",
                column: "DataEngId");

            migrationBuilder.CreateIndex(
                name: "IX_Author_DataRuId",
                table: "Author",
                column: "DataRuId");

            migrationBuilder.CreateIndex(
                name: "IX_Author_PhotoId",
                table: "Author",
                column: "PhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Author_UserId",
                table: "Author",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorEcoSystem_EcoSystemsId",
                table: "AuthorEcoSystem",
                column: "EcoSystemsId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorSoilObject_SoilObjectsId",
                table: "AuthorSoilObject",
                column: "SoilObjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_EcoSystem_PhotoId",
                table: "EcoSystem",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_EcoSystem_UserId",
                table: "EcoSystem",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EcoSystemFile_ObjectPhotoId",
                table: "EcoSystemFile",
                column: "ObjectPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_EcoSystemPublication_PublicationsId",
                table: "EcoSystemPublication",
                column: "PublicationsId");

            migrationBuilder.CreateIndex(
                name: "IX_EcoSystemSoilObject_SoilObjectsId",
                table: "EcoSystemSoilObject",
                column: "SoilObjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_FileSoilObject_SoilObjectsId",
                table: "FileSoilObject",
                column: "SoilObjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_FileId",
                table: "Publication",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_UserId",
                table: "Publication",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationSoilObject_SoilObjectsId",
                table: "PublicationSoilObject",
                column: "SoilObjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_SoilObjects_PhotoId",
                table: "SoilObjects",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_SoilObjects_UserId",
                table: "SoilObjects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SoilObjectTerm_TermsId",
                table: "SoilObjectTerm",
                column: "TermsId");

            migrationBuilder.CreateIndex(
                name: "IX_Term_ClassificationId",
                table: "Term",
                column: "ClassificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuthorEcoSystem");

            migrationBuilder.DropTable(
                name: "AuthorSoilObject");

            migrationBuilder.DropTable(
                name: "EcoSystemFile");

            migrationBuilder.DropTable(
                name: "EcoSystemPublication");

            migrationBuilder.DropTable(
                name: "EcoSystemSoilObject");

            migrationBuilder.DropTable(
                name: "FileSoilObject");

            migrationBuilder.DropTable(
                name: "PublicationSoilObject");

            migrationBuilder.DropTable(
                name: "SoilObjectTerm");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "EcoSystem");

            migrationBuilder.DropTable(
                name: "Publication");

            migrationBuilder.DropTable(
                name: "SoilObjects");

            migrationBuilder.DropTable(
                name: "Term");

            migrationBuilder.DropTable(
                name: "Translation");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "Classification");
        }
    }
}
