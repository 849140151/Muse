using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Muse.DB.Migrations
{
    /// <inheritdoc />
    public partial class SongBasic_delete_hiragana : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hiragana",
                table: "SongBasic");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hiragana",
                table: "SongBasic",
                type: "nvarchar(20)",
                nullable: true);
        }
    }
}
