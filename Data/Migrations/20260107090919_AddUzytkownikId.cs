using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDwAI_BugTrackSys.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUzytkownikId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UzytkownikId",
                table: "Zgloszenia",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UzytkownikId",
                table: "Zgloszenia");
        }
    }
}
