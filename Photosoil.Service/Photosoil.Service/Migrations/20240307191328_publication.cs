using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Photosoil.Service.Migrations
{
    public partial class publication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_SoilObjects_SoilObjectId",
                table: "Photo");

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "SoilObjects",
                type: "boolean",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EcoSystem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    PhotoId = table.Column<int>(type: "integer", nullable: false),
                    Latitude = table.Column<string>(type: "text", nullable: false),
                    Longtitude = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcoSystem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcoSystem_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Publication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    FileId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publication_Photo_FileId",
                        column: x => x.FileId,
                        principalTable: "Photo",
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

            migrationBuilder.CreateIndex(
                name: "IX_EcoSystem_PhotoId",
                table: "EcoSystem",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_EcoSystemPublication_PublicationsId",
                table: "EcoSystemPublication",
                column: "PublicationsId");

            migrationBuilder.CreateIndex(
                name: "IX_EcoSystemSoilObject_SoilObjectsId",
                table: "EcoSystemSoilObject",
                column: "SoilObjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_FileId",
                table: "Publication",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicationSoilObject_SoilObjectsId",
                table: "PublicationSoilObject",
                column: "SoilObjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_SoilObjects_SoilObjectId",
                table: "Photo",
                column: "SoilObjectId",
                principalTable: "SoilObjects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_SoilObjects_SoilObjectId",
                table: "Photo");

            migrationBuilder.DropTable(
                name: "EcoSystemPublication");

            migrationBuilder.DropTable(
                name: "EcoSystemSoilObject");

            migrationBuilder.DropTable(
                name: "PublicationSoilObject");

            migrationBuilder.DropTable(
                name: "EcoSystem");

            migrationBuilder.DropTable(
                name: "Publication");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "SoilObjects");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_SoilObjects_SoilObjectId",
                table: "Photo",
                column: "SoilObjectId",
                principalTable: "SoilObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
