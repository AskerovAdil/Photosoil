using System;
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
                    TranslationMode = table.Column<int>(type: "integer", nullable: false),
                    NameRu = table.Column<string>(type: "text", nullable: true),
                    NameEng = table.Column<string>(type: "text", nullable: true)
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
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameRu = table.Column<string>(type: "text", nullable: true),
                    NameEng = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
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
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Annotation = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Term",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClassificationId = table.Column<int>(type: "integer", nullable: false),
                    NameRu = table.Column<string>(type: "text", nullable: true),
                    NameEng = table.Column<string>(type: "text", nullable: true)
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
                    PhotoId = table.Column<int>(type: "integer", nullable: false),
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
                    Doi = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: true),
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
                    PhotoId = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true),
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
                    AuthorType = table.Column<int>(type: "integer", nullable: false),
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
                name: "FileNews",
                columns: table => new
                {
                    FilesId = table.Column<int>(type: "integer", nullable: false),
                    NewsFilesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileNews", x => new { x.FilesId, x.NewsFilesId });
                    table.ForeignKey(
                        name: "FK_FileNews_News_NewsFilesId",
                        column: x => x.NewsFilesId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileNews_Photo_FilesId",
                        column: x => x.FilesId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileNews1",
                columns: table => new
                {
                    NewsPhotoId = table.Column<int>(type: "integer", nullable: false),
                    ObjectPhotoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileNews1", x => new { x.NewsPhotoId, x.ObjectPhotoId });
                    table.ForeignKey(
                        name: "FK_FileNews1_News_NewsPhotoId",
                        column: x => x.NewsPhotoId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileNews1_Photo_ObjectPhotoId",
                        column: x => x.ObjectPhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsTag",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "integer", nullable: false),
                    TagsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsTag", x => new { x.NewsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_NewsTag_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: true),
                    IsEnglish = table.Column<bool>(type: "boolean", nullable: true),
                    LastUpdated = table.Column<string>(type: "text", nullable: true),
                    NewsId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsTranslations_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                name: "EcoTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: true),
                    IsEnglish = table.Column<bool>(type: "boolean", nullable: true),
                    LastUpdated = table.Column<string>(type: "text", nullable: true),
                    EcoSystemId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcoTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcoTranslations_EcoSystem_EcoSystemId",
                        column: x => x.EcoSystemId,
                        principalTable: "EcoSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                name: "PublicationTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Edition = table.Column<string>(type: "text", nullable: true),
                    Authors = table.Column<string>(type: "text", nullable: true),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: true),
                    IsEnglish = table.Column<bool>(type: "boolean", nullable: true),
                    LastUpdated = table.Column<string>(type: "text", nullable: true),
                    PublicationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicationTranslations_Publication_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                name: "SoilTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    GeographicLocation = table.Column<string>(type: "text", nullable: true),
                    ReliefLocation = table.Column<string>(type: "text", nullable: true),
                    PlantCommunity = table.Column<string>(type: "text", nullable: true),
                    SoilFeatures = table.Column<string>(type: "text", nullable: true),
                    AssociatedSoilComponents = table.Column<string>(type: "text", nullable: true),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: true),
                    IsEnglish = table.Column<bool>(type: "boolean", nullable: true),
                    LastUpdated = table.Column<string>(type: "text", nullable: true),
                    SoilId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoilTranslations_SoilObjects_SoilId",
                        column: x => x.SoilId,
                        principalTable: "SoilObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                columns: new[] { "Id", "IsMulti", "NameEng", "NameRu", "TranslationMode" },
                values: new object[,]
                {
                    { 1, true, null, "Отделы почв по Классификации почв России 2004/2008", 2 },
                    { 2, true, null, "Подтипы почв по Классификации почв России 2004/2008", 2 },
                    { 3, true, null, "Типы почв по Классификации почв России 2004/2008", 2 },
                    { 4, true, "Natural zone", "Природная зона", 0 },
                    { 5, true, "Principal qualifiers (WRB 2014)", "Основные квалификаторы (WRB 2014)", 0 },
                    { 6, true, "Principal qualifiers (WRB 2014)", "Основные квалификаторы (WRB 2014)", 0 }
                });

            migrationBuilder.InsertData(
                table: "Term",
                columns: new[] { "Id", "ClassificationId", "NameEng", "NameRu" },
                values: new object[,]
                {
                    { 1, 5, "Abruptic", "Abruptic" },
                    { 2, 5, "Calcaric", "Calcaric" },
                    { 3, 5, "Cambic", "Cambic" },
                    { 4, 5, "Chromic", "Chromic" },
                    { 5, 5, "Cutanic", "Cutanic" },
                    { 6, 5, "Greyic", "Greyic" },
                    { 7, 5, "Greyzemic", "Greyzemic" },
                    { 8, 5, "Orthofluvic", "Orthofluvic" },
                    { 9, 5, "Protoargic", "Protoargic" },
                    { 10, 5, "Reductigleyic", "Reductigleyic" },
                    { 11, 5, "Rustic", "Rustic" },
                    { 12, 5, "Someric", "Someric" },
                    { 13, 5, "Spolic", "Spolic" },
                    { 14, 5, "Sapric", "Sapric" },
                    { 15, 5, "Spodic", "Spodic" },
                    { 16, 5, "Stagnic", "Stagnic" },
                    { 17, 5, "Ortsteinic", "Ortsteinic" },
                    { 18, 5, "Natric", "Natric" },
                    { 19, 5, "Leptic", "Leptic" },
                    { 20, 5, "Limnic", "Limnic" },
                    { 21, 5, "Luvic", "Luvic" },
                    { 22, 5, "Mollic", "Mollic" },
                    { 23, 5, "Turbic", "Turbic" },
                    { 24, 5, "Umbric", "Umbric" },
                    { 25, 5, "Vertic", "Vertic" },
                    { 26, 5, "Vitric", "Vitric" },
                    { 27, 5, "Lamellic", "Lamellic" },
                    { 28, 5, "Histic", "Histic" },
                    { 29, 5, "Chernic", "Chernic" },
                    { 30, 5, "Cryic", "Cryic" },
                    { 31, 5, "Dystric", "Dystric" },
                    { 32, 5, "Endocalcic", "Endocalcic" },
                    { 33, 5, "Carbic", "Carbic" },
                    { 34, 5, "Calcic", "Calcic" },
                    { 35, 5, "Alic", "Alic" },
                    { 36, 5, "Andic", "Andic" },
                    { 37, 5, "Brunic", "Brunic" },
                    { 38, 5, "Entic", "Entic" },
                    { 39, 5, "Eutric", "Eutric" },
                    { 40, 5, "Gleyic", "Gleyic" },
                    { 41, 5, "Glossic", "Glossic" },
                    { 42, 5, "Gypsic", "Gypsic" },
                    { 43, 5, "Hemic", "Hemic" },
                    { 44, 5, "Glacic", "Glacic" },
                    { 45, 5, "Fragic", "Fragic" },
                    { 46, 5, "Fibric", "Fibric" },
                    { 47, 5, "Fluvic", "Fluvic" },
                    { 48, 5, "Folic", "Folic" },
                    { 49, 5, "Albic", "Albic" },
                    { 50, 6, "Acrisols", "Acrisols" },
                    { 51, 6, "Alisols", "Alisols" },
                    { 52, 6, "Andosols", "Andosols" },
                    { 53, 6, "Anthrosols", "Anthrosols" },
                    { 54, 6, "Arenosols", "Arenosols" },
                    { 55, 6, "Calcisols", "Calcisols" },
                    { 56, 6, "Cambisols", "Cambisols" },
                    { 57, 6, "Chernozems", "Chernozems" },
                    { 58, 6, "Cryosols", "Cryosols" },
                    { 59, 6, "Durisols", "Durisols" },
                    { 60, 6, "Ferralsols", "Ferralsols" },
                    { 61, 6, "Fluvisols", "Fluvisols" },
                    { 62, 6, "Gleysols", "Gleysols" },
                    { 63, 6, "Gypsisols", "Gypsisols" },
                    { 64, 6, "Histosols", "Histosols" },
                    { 65, 6, "Kastanozems", "Kastanozems" },
                    { 66, 6, "Leptosols", "Leptosols" },
                    { 67, 6, "Lixisols", "Lixisols" },
                    { 68, 6, "Luvisols", "Luvisols" },
                    { 69, 6, "Nitisols", "Nitisols" },
                    { 70, 6, "Phaeozems", "Phaeozems" },
                    { 71, 6, "Planosols", "Planosols" },
                    { 72, 6, "Plinthosols", "Plinthosols" },
                    { 73, 6, "Podzols", "Podzols" },
                    { 74, 6, "Regosols", "Regosols" },
                    { 75, 6, "Retisols", "Retisols" },
                    { 76, 6, "Solonchaks", "Solonchaks" },
                    { 77, 6, "Solonetz", "Solonetz" },
                    { 78, 6, "Stagnosols", "Stagnosols" },
                    { 79, 6, "Technosols", "Technosols" },
                    { 80, 6, "Umbrisols", "Umbrisols" },
                    { 81, 6, "Vertisols", "Vertisols" },
                    { 82, 3, null, "Абразёмы глинисто-иллювиированные" },
                    { 83, 3, null, "Агроабразёмы" },
                    { 84, 3, null, "Агродерново-подзолисто-глеевые" },
                    { 85, 3, null, "Агродерново-подзолистые" },
                    { 86, 3, null, "Агрозёмы" },
                    { 87, 3, null, "Агроземы текстурно-карбонатные" },
                    { 88, 3, null, "Агросолоди" },
                    { 89, 3, null, "Агростратозем" },
                    { 90, 3, null, "Агростратозёмы темногумусовые" },
                    { 91, 3, null, "Агротёмно-серые" },
                    { 92, 3, null, "Агротёмно-серые глеевые" },
                    { 93, 3, null, "Агротёмно-серые метаморфические" },
                    { 94, 3, null, "Агрочернозем текстурно-карбонатный" },
                    { 95, 3, null, "Агрочерноземы" },
                    { 96, 3, null, "Аллювиальные" },
                    { 97, 3, null, "Аллювиальные гумусовые (дерновые)" },
                    { 98, 3, null, "Аллювиальные гумусовые глеевые" },
                    { 99, 3, null, "Аллювиальные перегнойно-глеевые" },
                    { 100, 3, null, "Аллювиальные тёмногумусовые" },
                    { 101, 3, null, "Аллювиальные тёмногумусовые глеевые" },
                    { 102, 3, null, "Аллювиальные торфянно-глеевые" },
                    { 103, 3, null, "Бурая (аридная)" },
                    { 104, 3, null, "Бурозёмы" },
                    { 105, 3, null, "Бурозёмы грубогумусовые" },
                    { 106, 3, null, "Бурозёмы тёмногумусовые" },
                    { 107, 3, null, "Глеезёмы" },
                    { 108, 3, null, "Глееземы криометаморфические" },
                    { 109, 3, null, "Гумусово-гидрометаморфические" },
                    { 110, 3, null, "Дерново подзолы" },
                    { 111, 3, null, "Дерново-брусно-подзолистые" },
                    { 112, 3, null, "Дерново-криометаморфические" },
                    { 113, 3, null, "Дерново-подбуры" },
                    { 114, 3, null, "Дерново-подбуры глеевые" },
                    { 115, 3, null, "Дерново-подбуры элювоземы" },
                    { 116, 3, null, "Дерново-подзол-глеевые" },
                    { 117, 3, null, "Дерново-подзолистые" },
                    { 118, 3, null, "Дерново-подзолы глеевые" },
                    { 119, 3, null, "Дерново-солоди" },
                    { 120, 3, null, "Дерново-элювиально-метаморфические" },
                    { 121, 3, null, "Дерново-элювозем" },
                    { 122, 3, null, "Карбо-петрозём" },
                    { 123, 3, null, "Каштановые" },
                    { 124, 3, null, "Криогумусовые" },
                    { 125, 3, null, "Криозёмы" },
                    { 126, 3, null, "Криозёмы грубогумусовые" },
                    { 127, 3, null, "Криометаморфические" },
                    { 128, 3, null, "Криометаморфические грубогумусовые" },
                    { 129, 3, null, "Литозем грубогумусовый" },
                    { 130, 3, null, "Органо-ржавозёмы" },
                    { 131, 3, null, "Охристые" },
                    { 132, 3, null, "Палевые" },
                    { 133, 3, null, "Пелозем" },
                    { 134, 3, null, "Пелоземы гумусовые" },
                    { 135, 3, null, "Перегнойно-глеевые" },
                    { 136, 3, null, "Перегnoonно-криометаморфические" },
                    { 137, 3, null, "Перегнойно-охристая" },
                    { 138, 3, null, "Перегнойно-темногумусовые" },
                    { 139, 3, null, "Перегнойные" },
                    { 140, 3, null, "Петроземы" },
                    { 141, 3, null, "Подбуры" },
                    { 142, 3, null, "Подбуры глеевые" },
                    { 143, 3, null, "Подзол-элювозёмы" },
                    { 144, 3, null, "Подзолисто-глеевые" },
                    { 145, 3, null, "Подзолистые" },
                    { 146, 3, null, "Подзолы" },
                    { 147, 3, null, "Подзолы глеевые" },
                    { 148, 3, null, "Псаммоземы" },
                    { 149, 3, null, "Ржавозёмы" },
                    { 150, 3, null, "Ржавозёмы грубогумусовые" },
                    { 151, 3, null, "Светлогумусовые" },
                    { 152, 3, null, "Серая метаморфическая" },
                    { 153, 3, null, "Серогумусовые (дерновые)" },
                    { 154, 3, null, "Серые" },
                    { 155, 3, null, "Слоисто-пепловые" },
                    { 156, 3, null, "Солоди темногумусовые" },
                    { 157, 3, null, "Солонцы светлые" },
                    { 158, 3, null, "Солонцы темные" },
                    { 159, 3, null, "Солончаки" },
                    { 160, 3, null, "Солончаки глеевые" },
                    { 161, 3, null, "Стратозёмы серогумусовые" },
                    { 162, 3, null, "Сухо-торфяные" },
                    { 163, 3, null, "Сухоторфяно-подбуры" },
                    { 164, 3, null, "Сухоторфяно-подзолы" },
                    { 165, 3, null, "Сухоторфяные" },
                    { 166, 3, null, "Темно-серые" },
                    { 167, 3, null, "Темно-серые глеевые" },
                    { 168, 3, null, "Темно-serые метаморфические" },
                    { 169, 3, null, "Темногумусово-глеевые" },
                    { 170, 3, null, "Темногумусовые" },
                    { 171, 3, null, "Темногумусовые подбелы" },
                    { 172, 3, null, "Торфозёмы" },
                    { 173, 3, null, "Торфяно-глеезёмы" },
                    { 174, 3, null, "Торфяно-криозёмы" },
                    { 175, 3, null, "Торфяно-подзолисто-глеевые" },
                    { 176, 3, null, "Торфяно-подзолы" },
                    { 177, 3, null, "Торфяно-подзолы глеевые" },
                    { 178, 3, null, "Торфяные олиготрофные" },
                    { 179, 3, null, "Торфяные олиготрофные глеевые" },
                    { 180, 3, null, "Торфяные эутрофные" },
                    { 181, 3, null, "Торфяные эутрофные глеевые" },
                    { 182, 2, null, "Артииндустратный" },
                    { 183, 2, null, "Глее-подзолистые" },
                    { 184, 2, null, "Глееватые" },
                    { 185, 2, null, "Глеевые" },
                    { 186, 2, null, "Глинисто-иллювиированные" },
                    { 187, 2, null, "Глинофибровые" },
                    { 188, 2, null, "Грубо-гумусированные" },
                    { 189, 2, null, "Гумусово-слаборазвитый" },
                    { 190, 2, null, "Дисперсно-карбонатный" },
                    { 191, 2, null, "Железисто-гранулированные (гранузёмы)" },
                    { 192, 2, null, "Засоленные" },
                    { 193, 2, null, "Иллювиально-гумусовые" },
                    { 194, 2, null, "Иллювиально-железистые" },
                    { 195, 2, null, "Иллювиально-ожелезненные" },
                    { 196, 2, null, "Иллювиально-гумусированные" },
                    { 197, 2, null, "Иловато-перегнойные" },
                    { 198, 2, null, "Иловато-торфяные" },
                    { 199, 2, null, "Квазиглееватые" },
                    { 200, 2, null, "Конкреционные" },
                    { 201, 2, null, "Контактно-осветленные" },
                    { 202, 2, null, "Крио-гомогенные" },
                    { 203, 2, null, "Криогенно-ожелезненные" },
                    { 204, 2, null, "Криометаморфические" },
                    { 205, 2, null, "Криотурбированные" },
                    { 206, 2, null, "Мерзлотные" },
                    { 207, 2, null, "Миграционно-мицелярный" },
                    { 208, 2, null, "Миграционно-сегрегационные" },
                    { 209, 2, null, "Минерально-торфяные" },
                    { 210, 2, null, "Омергеленные" },
                    { 211, 2, null, "Оподзоленные" },
                    { 212, 2, null, "Оруденелые" },
                    { 213, 2, null, "Осолоделые" },
                    { 214, 2, null, "Остаточно-карбонатные" },
                    { 215, 2, null, "Остаточно-эутрофные" },
                    { 216, 2, null, "Палево-метаморфизованные" },
                    { 217, 2, null, "Палево-подзолистые" },
                    { 218, 2, null, "Палевые" },
                    { 219, 2, null, "Перегнойно-грубогумусовые" },
                    { 220, 2, null, "Перегнойно-торфяные" },
                    { 221, 2, null, "Перегнойные" },
                    { 222, 2, null, "Перегнойные (глеевые)" },
                    { 223, 2, null, "Потечно-гумусовые" },
                    { 224, 2, null, "Псевдофибровые" },
                    { 225, 2, null, "С микропрофилем подзола" },
                    { 226, 2, null, "Сегрегационные" },
                    { 227, 2, null, "Со вторым гумусовым горизонтом" },
                    { 228, 2, null, "Солонцеватые" },
                    { 229, 2, null, "Языковатые" },
                    { 230, 4, "Humid equatorial forests", "Влажные экваториальные леса" },
                    { 231, 4, "Mountains with altitudinal zonations", "Горные территории с высотной поясностью" },
                    { 232, 4, "Subtropical evergreen forests and shrubs", "Субтропические вечно-зеленые леса и кустарники" },
                    { 233, 4, "Typical tundra (subzone)", "Типичная тундра (подзона)" },
                    { 234, 4, "South tundra (subzone)", "Южная тундра (подзона)" },
                    { 235, 4, "Forest Tundra", "Лесотундра" },
                    { 236, 4, "Northern taiga (subzone)", "Северная тайга (подзона)" },
                    { 237, 4, "Middle taiga (subzone)", "Средняя тайга (подзона)" },
                    { 238, 4, "Southern taiga (subzone)", "Южная тайга" },
                    { 239, 4, "Coniferous-deciduous forests", "Хвойно-широколиственные леса" },
                    { 240, 4, "Broad-leaved forests (deciduous forests)", "Широколиственные леса" },
                    { 241, 4, "Continental subboreal forests (hemiboreal)", "Подтайга (мелколиственно-светлохвойная или хвойно-широколиственная)" },
                    { 242, 4, "Forest steppe", "Лесостепь" },
                    { 243, 4, "Steppe (subzones meadow and true steppes)", "Степь (подзоны луговых и настоящих степей)" },
                    { 244, 4, "Dry and deserted steppe", "Сухая и опустыненная степь" },
                    { 245, 4, "Deserts and semi-deserts", "Полупустыни и пустыни" },
                    { 246, 4, "Subtropical savannahs and woodlands", "Субтропических саванн и редколесий" },
                    { 247, 1, null, "Торфяные почвы" },
                    { 248, 1, null, "Турбозёмы" },
                    { 249, 1, null, "Хемозёмы" },
                    { 250, 1, null, "Торфозёмы" },
                    { 251, 1, null, "Текстурно-дифференцированные почвы" },
                    { 252, 1, null, "Стратозёмы" },
                    { 253, 1, null, "Структурно-метаморфические почвы" },
                    { 254, 1, null, "Химически-преобразованные" },
                    { 255, 1, null, "Щелочно-глинисто-дифференцированные почвы" },
                    { 256, 1, null, "Элювиальные почвы" },
                    { 257, 1, null, "Слаборазвитые почвы" },
                    { 258, 1, null, "Светлогумусовые аккумулятивно-карбонатные почвы" },
                    { 259, 1, null, "Аллювиальные почвы" },
                    { 260, 1, null, "Альфегумусовые почвы" },
                    { 261, 1, null, "Вулканические почвы" },
                    { 262, 1, null, "Аккумулятивно-гумусовые почвы" },
                    { 263, 1, null, "Аквазёмы" },
                    { 264, 1, null, "Агроабразёмы" },
                    { 265, 1, null, "Агрозёмы" },
                    { 266, 1, null, "Галоморфные почвы" },
                    { 267, 1, null, "Гидрометаморфические почвы" },
                    { 268, 1, null, "Литозёмы" },
                    { 269, 1, null, "Органо-аккумулятивные почвы" },
                    { 270, 1, null, "Палево-метаморфические почвы" },
                    { 271, 1, null, "Криометаморфические почвы" },
                    { 272, 1, null, "Криогенные почвы (Криозёмы)" },
                    { 273, 1, null, "Глеевые почвы" },
                    { 274, 1, null, "Железисто-метаморфические почвы" },
                    { 275, 1, null, "Абразёмы" }
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
                name: "IX_EcoTranslations_EcoSystemId",
                table: "EcoTranslations",
                column: "EcoSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_FileNews_NewsFilesId",
                table: "FileNews",
                column: "NewsFilesId");

            migrationBuilder.CreateIndex(
                name: "IX_FileNews1_ObjectPhotoId",
                table: "FileNews1",
                column: "ObjectPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_FileSoilObject_SoilObjectsId",
                table: "FileSoilObject",
                column: "SoilObjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_News_UserId",
                table: "News",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsTag_TagsId",
                table: "NewsTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsTranslations_NewsId",
                table: "NewsTranslations",
                column: "NewsId");

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
                name: "IX_PublicationTranslations_PublicationId",
                table: "PublicationTranslations",
                column: "PublicationId");

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
                name: "IX_SoilTranslations_SoilId",
                table: "SoilTranslations",
                column: "SoilId");

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
                name: "EcoTranslations");

            migrationBuilder.DropTable(
                name: "FileNews");

            migrationBuilder.DropTable(
                name: "FileNews1");

            migrationBuilder.DropTable(
                name: "FileSoilObject");

            migrationBuilder.DropTable(
                name: "NewsTag");

            migrationBuilder.DropTable(
                name: "NewsTranslations");

            migrationBuilder.DropTable(
                name: "PublicationSoilObject");

            migrationBuilder.DropTable(
                name: "PublicationTranslations");

            migrationBuilder.DropTable(
                name: "SoilObjectTerm");

            migrationBuilder.DropTable(
                name: "SoilTranslations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "EcoSystem");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Publication");

            migrationBuilder.DropTable(
                name: "Term");

            migrationBuilder.DropTable(
                name: "SoilObjects");

            migrationBuilder.DropTable(
                name: "Translation");

            migrationBuilder.DropTable(
                name: "Classification");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Photo");
        }
    }
}
