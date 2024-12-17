using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photosoil.Service.Migrations
{
    public partial class int22222 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "SoilObjects",
                newName: "ExternalSource");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "SoilTranslations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsExternal",
                table: "SoilObjects",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "EcoTranslations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "EcoTranslations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalSource",
                table: "EcoSystem",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsExternal",
                table: "EcoSystem",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "SoilTranslations");

            migrationBuilder.DropColumn(
                name: "IsExternal",
                table: "SoilObjects");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "EcoTranslations");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "EcoTranslations");

            migrationBuilder.DropColumn(
                name: "ExternalSource",
                table: "EcoSystem");

            migrationBuilder.DropColumn(
                name: "IsExternal",
                table: "EcoSystem");

            migrationBuilder.RenameColumn(
                name: "ExternalSource",
                table: "SoilObjects",
                newName: "Code");
        }
    }
}
