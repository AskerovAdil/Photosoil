using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photosoil.Service.Migrations
{
    public partial class annotation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Annotation",
                table: "News");

            migrationBuilder.AddColumn<string>(
                name: "Annotation",
                table: "NewsTranslations",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Annotation",
                table: "NewsTranslations");

            migrationBuilder.AddColumn<string>(
                name: "Annotation",
                table: "News",
                type: "text",
                nullable: true);
        }
    }
}
