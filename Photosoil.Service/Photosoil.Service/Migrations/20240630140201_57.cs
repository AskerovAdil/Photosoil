using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Photosoil.Service.Migrations
{
    public partial class _57 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "EcoSystem");

            migrationBuilder.DropColumn(
                name: "IsEnglish",
                table: "EcoSystem");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "EcoSystem");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "EcoSystem");

            migrationBuilder.DropColumn(
                name: "OtherLangId",
                table: "EcoSystem");

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

            migrationBuilder.CreateIndex(
                name: "IX_EcoTranslations_EcoSystemId",
                table: "EcoTranslations",
                column: "EcoSystemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EcoTranslations");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "EcoSystem",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnglish",
                table: "EcoSystem",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "EcoSystem",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EcoSystem",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OtherLangId",
                table: "EcoSystem",
                type: "integer",
                nullable: true);
        }
    }
}
