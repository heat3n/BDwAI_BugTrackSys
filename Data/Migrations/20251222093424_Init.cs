using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDwAI_BugTrackSys.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Priorytety",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorytety", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projekty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statusy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statusy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zgloszenia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Temat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataUtworzenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjektId = table.Column<int>(type: "int", nullable: false),
                    PriorytetId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zgloszenia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zgloszenia_Priorytety_PriorytetId",
                        column: x => x.PriorytetId,
                        principalTable: "Priorytety",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zgloszenia_Projekty_ProjektId",
                        column: x => x.ProjektId,
                        principalTable: "Projekty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zgloszenia_Statusy_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statusy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zgloszenia_PriorytetId",
                table: "Zgloszenia",
                column: "PriorytetId");

            migrationBuilder.CreateIndex(
                name: "IX_Zgloszenia_ProjektId",
                table: "Zgloszenia",
                column: "ProjektId");

            migrationBuilder.CreateIndex(
                name: "IX_Zgloszenia_StatusId",
                table: "Zgloszenia",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zgloszenia");

            migrationBuilder.DropTable(
                name: "Priorytety");

            migrationBuilder.DropTable(
                name: "Projekty");

            migrationBuilder.DropTable(
                name: "Statusy");
        }
    }
}
