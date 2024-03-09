using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Photosoil.Service.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoilObjects_NatureZone_NatureZoneId",
                table: "SoilObjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SoilObjects_SoilDepartment_DepartmentId",
                table: "SoilObjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SoilObjects_SoilGroup_SoilGroupId",
                table: "SoilObjects");

            migrationBuilder.DropTable(
                name: "NatureZone");

            migrationBuilder.DropTable(
                name: "QualifierSoilObject");

            migrationBuilder.DropTable(
                name: "SoilDepartment");

            migrationBuilder.DropTable(
                name: "SoilGroup");

            migrationBuilder.DropTable(
                name: "SoilObjectSoilSubType");

            migrationBuilder.DropTable(
                name: "SoilObjectSoilType");

            migrationBuilder.DropTable(
                name: "Qualifier");

            migrationBuilder.DropTable(
                name: "SoilSubType");

            migrationBuilder.DropTable(
                name: "SoilType");

            migrationBuilder.DropIndex(
                name: "IX_SoilObjects_DepartmentId",
                table: "SoilObjects");

            migrationBuilder.DropIndex(
                name: "IX_SoilObjects_NatureZoneId",
                table: "SoilObjects");

            migrationBuilder.DropIndex(
                name: "IX_SoilObjects_SoilGroupId",
                table: "SoilObjects");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "SoilObjects");

            migrationBuilder.DropColumn(
                name: "NatureZoneId",
                table: "SoilObjects");

            migrationBuilder.DropColumn(
                name: "SoilGroupId",
                table: "SoilObjects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "SoilObjects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NatureZoneId",
                table: "SoilObjects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoilGroupId",
                table: "SoilObjects",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NatureZone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NatureZone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Qualifier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoilDepartment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilDepartment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoilGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoilSubType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilSubType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoilType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QualifierSoilObject",
                columns: table => new
                {
                    QualifiersId = table.Column<int>(type: "integer", nullable: false),
                    SoilObjectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualifierSoilObject", x => new { x.QualifiersId, x.SoilObjectsId });
                    table.ForeignKey(
                        name: "FK_QualifierSoilObject_Qualifier_QualifiersId",
                        column: x => x.QualifiersId,
                        principalTable: "Qualifier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QualifierSoilObject_SoilObjects_SoilObjectsId",
                        column: x => x.SoilObjectsId,
                        principalTable: "SoilObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoilObjectSoilSubType",
                columns: table => new
                {
                    SoilObjectsId = table.Column<int>(type: "integer", nullable: false),
                    SoilSubTypesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilObjectSoilSubType", x => new { x.SoilObjectsId, x.SoilSubTypesId });
                    table.ForeignKey(
                        name: "FK_SoilObjectSoilSubType_SoilObjects_SoilObjectsId",
                        column: x => x.SoilObjectsId,
                        principalTable: "SoilObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoilObjectSoilSubType_SoilSubType_SoilSubTypesId",
                        column: x => x.SoilSubTypesId,
                        principalTable: "SoilSubType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoilObjectSoilType",
                columns: table => new
                {
                    SoilObjectsId = table.Column<int>(type: "integer", nullable: false),
                    SoilTypesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilObjectSoilType", x => new { x.SoilObjectsId, x.SoilTypesId });
                    table.ForeignKey(
                        name: "FK_SoilObjectSoilType_SoilObjects_SoilObjectsId",
                        column: x => x.SoilObjectsId,
                        principalTable: "SoilObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoilObjectSoilType_SoilType_SoilTypesId",
                        column: x => x.SoilTypesId,
                        principalTable: "SoilType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SoilObjects_DepartmentId",
                table: "SoilObjects",
                column: "DepartmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoilObjects_NatureZoneId",
                table: "SoilObjects",
                column: "NatureZoneId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoilObjects_SoilGroupId",
                table: "SoilObjects",
                column: "SoilGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QualifierSoilObject_SoilObjectsId",
                table: "QualifierSoilObject",
                column: "SoilObjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_SoilObjectSoilSubType_SoilSubTypesId",
                table: "SoilObjectSoilSubType",
                column: "SoilSubTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_SoilObjectSoilType_SoilTypesId",
                table: "SoilObjectSoilType",
                column: "SoilTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_SoilObjects_NatureZone_NatureZoneId",
                table: "SoilObjects",
                column: "NatureZoneId",
                principalTable: "NatureZone",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SoilObjects_SoilDepartment_DepartmentId",
                table: "SoilObjects",
                column: "DepartmentId",
                principalTable: "SoilDepartment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SoilObjects_SoilGroup_SoilGroupId",
                table: "SoilObjects",
                column: "SoilGroupId",
                principalTable: "SoilGroup",
                principalColumn: "Id");
        }
    }
}
