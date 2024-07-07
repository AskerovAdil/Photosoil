using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photosoil.Service.Migrations
{
    public partial class _59 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnglish",
                table: "PublicationTranslations",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnglish",
                table: "PublicationTranslations");
        }
    }
}
