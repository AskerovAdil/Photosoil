using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photosoil.Service.Migrations
{
    public partial class int2222255 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalSource",
                table: "SoilObjects");

            migrationBuilder.DropColumn(
                name: "ExternalSource",
                table: "EcoSystem");

            migrationBuilder.AddColumn<string>(
                name: "ExternalSource",
                table: "SoilTranslations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalSource",
                table: "EcoTranslations",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalSource",
                table: "SoilTranslations");

            migrationBuilder.DropColumn(
                name: "ExternalSource",
                table: "EcoTranslations");

            migrationBuilder.AddColumn<string>(
                name: "ExternalSource",
                table: "SoilObjects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalSource",
                table: "EcoSystem",
                type: "text",
                nullable: true);
        }
    }
}
