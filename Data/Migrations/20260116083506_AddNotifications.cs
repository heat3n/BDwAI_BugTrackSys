using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDwAI_BugTrackSys.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Powiadomienia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tresc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CzyPrzeczytane = table.Column<bool>(type: "bit", nullable: false),
                    UzytkownikId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ZgloszenieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Powiadomienia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Powiadomienia_AspNetUsers_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Powiadomienia_Zgloszenia_ZgloszenieId",
                        column: x => x.ZgloszenieId,
                        principalTable: "Zgloszenia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Powiadomienia_UzytkownikId",
                table: "Powiadomienia",
                column: "UzytkownikId");

            migrationBuilder.CreateIndex(
                name: "IX_Powiadomienia_ZgloszenieId",
                table: "Powiadomienia",
                column: "ZgloszenieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Powiadomienia");
        }
    }
}
