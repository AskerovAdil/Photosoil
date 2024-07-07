using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Photosoil.Service.Migrations
{
    public partial class _55 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssociatedSoilComponents",
                table: "SoilObjects");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "SoilObjects");

            migrationBuilder.DropColumn(
                name: "GeographicLocation",
                table: "SoilObjects");

            migrationBuilder.DropColumn(
                name: "IsEnglish",
                table: "SoilObjects");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "SoilObjects");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SoilObjects");

            migrationBuilder.DropColumn(
                name: "OtherLangId",
                table: "SoilObjects");

            migrationBuilder.DropColumn(
                name: "PlantCommunity",
                table: "SoilObjects");

            migrationBuilder.DropColumn(
                name: "ReliefLocation",
                table: "SoilObjects");

            migrationBuilder.DropColumn(
                name: "SoilFeatures",
                table: "SoilObjects");

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
                    SoilId = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_SoilTranslations_SoilId",
                table: "SoilTranslations",
                column: "SoilId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoilTranslations");

            migrationBuilder.AddColumn<string>(
                name: "AssociatedSoilComponents",
                table: "SoilObjects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "SoilObjects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeographicLocation",
                table: "SoilObjects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnglish",
                table: "SoilObjects",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "SoilObjects",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SoilObjects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OtherLangId",
                table: "SoilObjects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlantCommunity",
                table: "SoilObjects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReliefLocation",
                table: "SoilObjects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoilFeatures",
                table: "SoilObjects",
                type: "text",
                nullable: true);
        }
    }
}
