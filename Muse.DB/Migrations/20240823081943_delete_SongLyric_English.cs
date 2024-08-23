using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Muse.DB.Migrations
{
    /// <inheritdoc />
    public partial class delete_SongLyric_English : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "English",
                table: "SongLyrics");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "English",
                table: "SongLyrics",
                type: "TEXT",
                nullable: true);
        }
    }
}
