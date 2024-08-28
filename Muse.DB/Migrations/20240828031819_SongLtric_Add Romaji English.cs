using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Muse.DB.Migrations
{
    /// <inheritdoc />
    public partial class SongLtric_AddRomajiEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Japanese",
                table: "SongLyrics",
                newName: "RoMaJi");

            migrationBuilder.AddColumn<string>(
                name: "English",
                table: "SongLyrics",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kanji",
                table: "SongLyrics",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "English",
                table: "SongLyrics");

            migrationBuilder.DropColumn(
                name: "Kanji",
                table: "SongLyrics");

            migrationBuilder.RenameColumn(
                name: "RoMaJi",
                table: "SongLyrics",
                newName: "Japanese");
        }
    }
}
