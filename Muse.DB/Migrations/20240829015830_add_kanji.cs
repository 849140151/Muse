using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Muse.DB.Migrations
{
    /// <inheritdoc />
    public partial class add_kanji : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Japanese",
                table: "SongLyrics",
                newName: "Romaji");

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
                name: "Romaji",
                table: "SongLyrics",
                newName: "Japanese");
        }
    }
}
