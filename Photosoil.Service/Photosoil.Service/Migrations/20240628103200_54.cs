using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photosoil.Service.Migrations
{
    public partial class _54 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OtherLangId",
                table: "Publication",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherLangId",
                table: "Publication");
        }
    }
}
