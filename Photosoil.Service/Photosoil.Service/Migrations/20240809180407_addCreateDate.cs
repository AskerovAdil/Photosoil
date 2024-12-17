using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photosoil.Service.Migrations
{
    public partial class addCreateDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedDate",
                table: "SoilObjects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedDate",
                table: "Publication",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedDate",
                table: "News",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedDate",
                table: "EcoSystem",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedDate",
                table: "Author",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SoilObjects");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Publication");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "News");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "EcoSystem");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Author");
        }
    }
}
