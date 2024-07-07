using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Photosoil.Service.Migrations
{
    public partial class _58 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Authors",
                table: "Publication");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Publication");

            migrationBuilder.DropColumn(
                name: "Edition",
                table: "Publication");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Publication");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Publication");

            migrationBuilder.DropColumn(
                name: "OtherLangId",
                table: "Publication");

            migrationBuilder.CreateTable(
                name: "PublicationTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Edition = table.Column<string>(type: "text", nullable: true),
                    Authors = table.Column<string>(type: "text", nullable: true),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: true),
                    PublicationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicationTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicationTranslations_Publication_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicationTranslations_PublicationId",
                table: "PublicationTranslations",
                column: "PublicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicationTranslations");

            migrationBuilder.AddColumn<string>(
                name: "Authors",
                table: "Publication",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Publication",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Edition",
                table: "Publication",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Publication",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Publication",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OtherLangId",
                table: "Publication",
                type: "integer",
                nullable: true);
        }
    }
}
