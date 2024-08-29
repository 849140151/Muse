using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Muse.DB.Migrations
{
    /// <inheritdoc />
    public partial class SongLyric_add_lyricOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LyricOrder",
                table: "SongLyrics",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LyricOrder",
                table: "SongLyrics");
        }
    }
}
