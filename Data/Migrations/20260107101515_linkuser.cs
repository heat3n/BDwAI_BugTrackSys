using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDwAI_BugTrackSys.Data.Migrations
{
    /// <inheritdoc />
    public partial class linkuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UzytkownikId",
                table: "Zgloszenia",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Zgloszenia_UzytkownikId",
                table: "Zgloszenia",
                column: "UzytkownikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Zgloszenia_AspNetUsers_UzytkownikId",
                table: "Zgloszenia",
                column: "UzytkownikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zgloszenia_AspNetUsers_UzytkownikId",
                table: "Zgloszenia");

            migrationBuilder.DropIndex(
                name: "IX_Zgloszenia_UzytkownikId",
                table: "Zgloszenia");

            migrationBuilder.AlterColumn<string>(
                name: "UzytkownikId",
                table: "Zgloszenia",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
