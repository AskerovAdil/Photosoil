    using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photosoil.Service.Migrations
{
    public partial class _60 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "SoilObjects");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Publication");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "EcoSystem");

            migrationBuilder.AddColumn<string>(
                name: "LastUpdated",
                table: "SoilTranslations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdated",
                table: "PublicationTranslations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdated",
                table: "EcoTranslations",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "SoilTranslations");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "PublicationTranslations");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "EcoTranslations");

            migrationBuilder.AddColumn<string>(
                name: "LastUpdated",
                table: "SoilObjects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdated",
                table: "Publication",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdated",
                table: "EcoSystem",
                type: "text",
                nullable: true);
        }
    }
}
